using Celeste.DS;
using Celeste.FSM;
using Celeste.Tools.Attributes.GUI;
using CelesteEditor.Tools;
using Robbi.Levels;
using Robbi.Levels.Elements;
using RobbiEditor.Constants;
using RobbiEditor.Levels;
using RobbiEditor.Levels.Elements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using static RobbiEditor.LevelDirectories;

namespace RobbiEditor.Tools
{
    public enum InteractableAction
    {
        OpenDoor,
        CloseDoor,
        ToggleDoor
    }

    [Serializable]
    public struct InteractableMarker
    {
        public DoorColour doorColour;
        public InteractableAction interactableAction;
    }

    [Serializable]
    public struct DoorInfo
    {
        public DoorColour doorColour;
        public DoorState doorState;
    }

    [Serializable]
    public class LevelInfo : ScriptableObject
    {
        public string destinationFolder = LevelDirectories.LEVELS_PATH;
        public uint levelIndex = 0;
        public GameObject levelPrefabToCopy;
        public bool clearLevel = true;

        [Header("Parameters")]
        public int maxWaypointsPlaceable = 10;
        public uint softCurrencyPrize = 50;
        public bool requiresFuel = false;
        [ShowIf("requiresFuel")]
        public uint startingFuel = 0;

        [Header("Doors")]
        public List<DoorInfo> doors = new List<DoorInfo>();

        [Header("Interactables")]
        public List<InteractableMarker> interactableMarkers = new List<InteractableMarker>();
        public int numInteractables;
        public int numInteractableStateMachines;

        [Header("Collectables")]
        public int numCollectables;
        public List<LevelCollectionTarget> collectionTargets = new List<LevelCollectionTarget>();

        [Header("Portals")]
        public int numPortals;

        [Header("Lasers")]
        public List<LaserDefinition> lasers = new List<LaserDefinition>();

        [Header("Tutorials")]
        public bool hasTutorial = false;
        [ShowIf("hasTutorial")]
        public GameObject tutorialPrefabToCopy;

        [Header("FSM")]
        public bool hasFsm = false;
    }

    public class CreateLevel : ScriptableWizard
    {
        #region Properties and Fields

        private string LevelFolderName
        {
            get { return string.Format("Level{0}/", levelInfo.levelIndex); }
        }

        private string LevelFolderFullPath
        {
            get { return levelInfo.destinationFolder + LevelFolderName; }
        }

        private LevelInfo levelInfo;
        private SerializedObject levelInfoObject;

        private static Dictionary<InteractableAction, string> greenDoorMarkerPrefabs = new Dictionary<InteractableAction, string>()
        {
            { InteractableAction.OpenDoor, PrefabFiles.GREEN_DOOR_OPEN_MARKER_PREFAB },
            { InteractableAction.CloseDoor, PrefabFiles.GREEN_DOOR_CLOSE_MARKER_PREFAB },
            { InteractableAction.ToggleDoor, PrefabFiles.GREEN_DOOR_TOGGLE_MARKER_PREFAB },
        };

        private static Dictionary<InteractableAction, string> redDoorMarkerPrefabs = new Dictionary<InteractableAction, string>()
        {
            { InteractableAction.OpenDoor, PrefabFiles.RED_DOOR_OPEN_MARKER_PREFAB },
            { InteractableAction.CloseDoor, PrefabFiles.RED_DOOR_CLOSE_MARKER_PREFAB },
            { InteractableAction.ToggleDoor, PrefabFiles.RED_DOOR_TOGGLE_MARKER_PREFAB },
        };

        private static Dictionary<InteractableAction, string> blueDoorMarkerPrefabs = new Dictionary<InteractableAction, string>()
        {
            { InteractableAction.OpenDoor, PrefabFiles.BLUE_DOOR_OPEN_MARKER_PREFAB },
            { InteractableAction.CloseDoor, PrefabFiles.BLUE_DOOR_CLOSE_MARKER_PREFAB },
            { InteractableAction.ToggleDoor, PrefabFiles.BLUE_DOOR_TOGGLE_MARKER_PREFAB },
        };

        private static Dictionary<InteractableAction, string> brownDoorMarkerPrefabs = new Dictionary<InteractableAction, string>()
        {
            { InteractableAction.OpenDoor, PrefabFiles.BROWN_DOOR_OPEN_MARKER_PREFAB },
            { InteractableAction.CloseDoor, PrefabFiles.BROWN_DOOR_CLOSE_MARKER_PREFAB },
            { InteractableAction.ToggleDoor, PrefabFiles.BROWN_DOOR_TOGGLE_MARKER_PREFAB },
        };

        #endregion

        #region GUI

        private void OnEnable()
        {
            LevelFolder previousLevelFolder = new LevelFolder(levelInfo.levelIndex - 1);
            levelInfo.levelPrefabToCopy = AssetDatabase.LoadAssetAtPath<GameObject>(previousLevelFolder.PrefabPath);

            levelInfoObject = new SerializedObject(levelInfo);
        }

        protected override bool DrawWizardGUI()
        {
            levelInfoObject.Update();

            EditorGUI.BeginChangeCheck();
            bool propertiesChanged = false;
            string[] excludes = { "m_Script" };

            // Iterate through serialized properties and draw them like the Inspector (But with ports)
            SerializedProperty iterator = levelInfoObject.GetIterator();
            bool enterChildren = true;
            while (iterator.NextVisible(enterChildren))
            {
                enterChildren = false;
                if (excludes.Contains(iterator.name)) continue;
                propertiesChanged |= EditorGUILayout.PropertyField(iterator, true);
            }

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Create Level Directory", GUILayout.ExpandWidth(false)))
            {
                CreateLevelDirectory();
            }

            if (levelInfo.hasFsm && GUILayout.Button("Create FSM", GUILayout.ExpandWidth(false)))
            {
                CreateFSM();
            }

            if (GUILayout.Button("Create Prefab", GUILayout.ExpandWidth(false)))
            {
                CreatePrefab();
            }

            if (levelInfo.hasTutorial && GUILayout.Button("Create Tutorial", GUILayout.ExpandWidth(false)))
            {
                CreateTutorial();
            }

            if (GUILayout.Button("Create Level Data", GUILayout.ExpandWidth(false)))
            {
                CreateLevelData();
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Create Doors", GUILayout.ExpandWidth(false)))
            {
                CreateDoors();
            }

            if (GUILayout.Button("Create Interactables", GUILayout.ExpandWidth(false)))
            {
                CreateInteractables();
            }

            if (GUILayout.Button("Create Interactable State Machines", GUILayout.ExpandWidth(false)))
            {
                CreateInteractableStateMachines();
            }

            if (GUILayout.Button("Create Collectables", GUILayout.ExpandWidth(false)))
            {
                CreateCollectables();
            }

            if (GUILayout.Button("Create Portals", GUILayout.ExpandWidth(false)))
            {
                CreatePortals();
            }

            if (GUILayout.Button("Create Lasers", GUILayout.ExpandWidth(false)))
            {
                CreateLasers();
            }

            EditorGUILayout.EndHorizontal();

            levelInfoObject.ApplyModifiedProperties();

            return propertiesChanged || EditorGUI.EndChangeCheck();
        }

        private void OnWizardCreate()
        {
            if (Directory.Exists(Path.Combine(Application.dataPath, LevelFolderFullPath)))
            {
                EditorUtility.DisplayDialog("Error", string.Format("Folder for Level Index {0} already exists.  Abandoning creation.", levelInfo.levelIndex), "OK");
                return;
            }

            LogUtility.Clear();

            CreateLevelDirectory();
            CreateFSM();
            CreateTutorial();
            CreatePrefab();
            CreateDoors();
            CreateInteractables();
            CreateInteractableStateMachines();
            CreateCollectables();
            CreatePortals();
            CreateLasers();
            CreateLevelData();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private void OnWizardOtherButton()
        {
            Close();
        }

        #endregion

        #region Creation Methods

        private void CreateLevelDirectory()
        {
            AssetUtility.CreateFolder(levelInfo.destinationFolder, LevelFolderName);
        }

        private void CreateFSM()
        {
            if (!levelInfo.hasFsm)
            {
                return;
            }

            FSMGraph fsm = ScriptableObject.CreateInstance<FSMGraph>();
            fsm.name = string.Format("Level{0}FSM", levelInfo.levelIndex);

            AssetDatabase.CreateAsset(fsm, string.Format("{0}{1}.asset", LevelFolderFullPath, fsm.name));
            fsm.SetAddressableInfo(AddressablesConstants.LEVELS_GROUP);
        }

        private void CreatePrefab()
        {
            string levelFolderFullPath = LevelFolderFullPath;
            string prefabPath = string.Format("{0}Level{1}.prefab", levelFolderFullPath, levelInfo.levelIndex);
            AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(levelInfo.levelPrefabToCopy), prefabPath);
            
            GameObject createdPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            {
                LevelRoot createdPrefabLevelRoot = createdPrefab.GetComponent<LevelRoot>();

                if (levelInfo.clearLevel)
                {
                    ClearAllTilemaps(createdPrefab);
                }

                // Have to delete the interactables in the prefab
                DeleteAllChildren(createdPrefabLevelRoot.interactablesTilemap.transform);
            }

            // Then instantiate the prefab so we can add the interactables
            // I kept having a crash in Unity when I tried adding these directly to the prefab
            {
                GameObject instantiatedPrefab = PrefabUtility.InstantiatePrefab(createdPrefab) as GameObject;
                LevelRoot instantiatedLevelRoot = instantiatedPrefab.GetComponent<LevelRoot>();

                for (uint i = 0; i < levelInfo.interactableMarkers.Count; ++i)
                {
                    InteractableMarker interactableMarker = levelInfo.interactableMarkers[(int)i];
                    Dictionary<InteractableAction, string> interactableMarkerPrefabs = GetInteractableMarkerPrefabs(interactableMarker.doorColour);
                    GameObject interactableMarkerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(interactableMarkerPrefabs[interactableMarker.interactableAction]);
                    GameObject interactableMarkerGameObject = PrefabUtility.InstantiatePrefab(interactableMarkerPrefab, instantiatedLevelRoot.interactablesTilemap.transform) as GameObject;
                    interactableMarkerGameObject.name = string.Format("{0}Door{1}Marker", interactableMarker.doorColour, interactableMarker.interactableAction);

                    PrefabUtility.ApplyAddedGameObject(interactableMarkerGameObject, prefabPath, InteractionMode.AutomatedAction);
                }

                GameObject.DestroyImmediate(instantiatedPrefab);

                if (levelInfo.hasFsm)
                {
                    FSMRuntime runtime = createdPrefab.GetComponent<FSMRuntime>();
                    if (runtime == null)
                    {
                        runtime = createdPrefab.AddComponent<FSMRuntime>();
                    }

                    string fsmPath = string.Format("{0}Level{0}FSM.asset", levelFolderFullPath, levelInfo.levelIndex);
                    runtime.graph = AssetDatabase.LoadAssetAtPath<FSMGraph>(fsmPath);
                }
            }

            // Finally, add the tutorial prefab
            if (levelInfo.hasTutorial)
            {
                LevelFolder levelFolder = new LevelFolder(levelInfo.levelIndex);
                GameObject instantiatedLevelPrefab = PrefabUtility.InstantiatePrefab(createdPrefab) as GameObject;
                GameObject tutorialPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(levelFolder.TutorialsPrefabPath);
                GameObject tutorialInstance = PrefabUtility.InstantiatePrefab(tutorialPrefab, instantiatedLevelPrefab.transform) as GameObject;
                tutorialInstance.name = tutorialPrefab.name;

                PrefabUtility.ApplyAddedGameObject(tutorialInstance, prefabPath, InteractionMode.AutomatedAction);
                GameObject.DestroyImmediate(instantiatedLevelPrefab);
            }

            createdPrefab.SetAddressableInfo(AddressablesConstants.LEVELS_GROUP);
            EditorUtility.SetDirty(createdPrefab);
        }

        private void CreateDoors()
        {
            if (levelInfo.doors.Count == 0)
            {
                return;
            }

            string doorsPath = string.Format("{0}{1}", LevelFolderFullPath, DOORS_NAME);
            if (!Directory.Exists(doorsPath))
            {
                AssetUtility.CreateFolder(LevelFolderFullPath, DOORS_NAME);
            }

            foreach (DoorInfo doorInfo in levelInfo.doors)
            {
                DoorEditor.CreateDoor(string.Format("Level{0}{1}Door", levelInfo.levelIndex, doorInfo.doorColour), doorsPath, doorInfo.doorState);
            }
        }

        private void CreateInteractables()
        {
            if (levelInfo.numInteractables == 0)
            {
                return;
            }

            string interactablesPath = string.Format("{0}{1}", LevelFolderFullPath, INTERACTABLES_NAME);
            if (!Directory.Exists(interactablesPath))
            {
                AssetUtility.CreateFolder(LevelFolderFullPath, INTERACTABLES_NAME);
            }

            for (int i = 0; i < levelInfo.numInteractables; ++i)
            {
                Interactable interactable = ScriptableObject.CreateInstance<Interactable>();
                interactable.name = string.Format("Level{0}Interactable{1}", levelInfo.levelIndex, i);
                AssetDatabase.CreateAsset(interactable, string.Format("{0}{1}.asset", interactablesPath, interactable.name));
                interactable.SetAddressableInfo(AddressablesConstants.LEVELS_GROUP);
            }
        }

        private void CreateInteractableStateMachines()
        {
            if (levelInfo.numInteractableStateMachines == 0)
            {
                return;
            }

            string interactablesPath = string.Format("{0}{1}", LevelFolderFullPath, INTERACTABLES_NAME);
            if (!Directory.Exists(interactablesPath))
            {
                AssetUtility.CreateFolder(LevelFolderFullPath, INTERACTABLES_NAME);
            }

            for (int i = 0; i < levelInfo.numInteractableStateMachines; ++i)
            {
                InteractableStateMachine interactableStateMachine = ScriptableObject.CreateInstance<InteractableStateMachine>();
                interactableStateMachine.name = string.Format("Level{0}InteractableStateMachine{1}", levelInfo.levelIndex, i);
                AssetDatabase.CreateAsset(interactableStateMachine, string.Format("{0}{1}.asset", interactablesPath, interactableStateMachine.name));
                interactableStateMachine.SetAddressableInfo(AddressablesConstants.LEVELS_GROUP);
            }
        }

        private void CreateCollectables()
        {
            if (levelInfo.numCollectables == 0)
            {
                return;
            }

            string collectablesPath = string.Format("{0}{1}", LevelFolderFullPath, COLLECTABLES_NAME);
            if (!Directory.Exists(collectablesPath))
            {
                AssetUtility.CreateFolder(LevelFolderFullPath, COLLECTABLES_NAME);
            }

            for (int i = 0; i < levelInfo.numCollectables; ++i)
            {
                Collectable collectable = ScriptableObject.CreateInstance<Collectable>();
                collectable.name = string.Format("Level{0}Collectable{1}", levelInfo.levelIndex, i);
                AssetDatabase.CreateAsset(collectable, string.Format("{0}{1}.asset", collectablesPath, collectable.name));
                collectable.SetAddressableInfo(AddressablesConstants.LEVELS_GROUP);
            }
        }

        private void CreatePortals()
        {
            if (levelInfo.numPortals == 0)
            {
                return;
            }

            string portalsPath = string.Format("{0}{1}", LevelFolderFullPath, PORTALS_NAME);
            if (!Directory.Exists(portalsPath))
            {
                AssetUtility.CreateFolder(LevelFolderFullPath, PORTALS_NAME);
            }

            for (int i = 0; i < levelInfo.numPortals; ++i)
            {
                Portal portal = ScriptableObject.CreateInstance<Portal>();
                portal.name = string.Format("Level{0}Portal{1}", levelInfo.levelIndex, i);
                AssetDatabase.CreateAsset(portal, string.Format("{0}{1}.asset", portalsPath, portal.name));
                portal.SetAddressableInfo(AddressablesConstants.LEVELS_GROUP);
            }
        }

        private void CreateLasers()
        {
            if (levelInfo.lasers.Count == 0)
            {
                return;
            }

            string lasersPath = string.Format("{0}{1}", LevelFolderFullPath, LASERS_NAME);
            if (!Directory.Exists(lasersPath))
            {
                AssetUtility.CreateFolder(LevelFolderFullPath, LASERS_NAME);
            }

            int index = 0;
            foreach (LaserDefinition laserDefinition in levelInfo.lasers)
            {
                Laser laser = ScriptableObject.CreateInstance<Laser>();
                laser.name = string.Format("Level{0}{1}", levelInfo.levelIndex, laserDefinition.name.Replace("Definition", ""));
                laser.laserDefinition = laserDefinition;
                AssetDatabase.CreateAsset(laser, string.Format("{0}{1}{2}.asset", lasersPath, laser.name, index++));
                laser.SetAddressableInfo(AddressablesConstants.LEVELS_GROUP);
            }
        }

        private void CreateTutorial()
        {
            if (!levelInfo.hasTutorial)
            {
                return;
            }

            string tutorialsPath = string.Format("{0}{1}", LevelFolderFullPath, TUTORIALS_NAME);
            if (!Directory.Exists(tutorialsPath))
            {
                AssetUtility.CreateFolder(LevelFolderFullPath, TUTORIALS_NAME);
            }

            uint levelIndex = levelInfo.levelIndex;

            FSMGraph tutorialFsm = ScriptableObject.CreateInstance<FSMGraph>();
            tutorialFsm.name = string.Format("Level{0}TutorialsFSM", levelIndex);

            AssetDatabase.CreateAsset(tutorialFsm, Path.Combine(tutorialsPath, tutorialFsm.name + ".asset"));
            tutorialFsm.SetAddressableInfo(AddressablesConstants.LEVELS_GROUP);

            DataGraph tutorialDataGraph = ScriptableObject.CreateInstance<DataGraph>();
            tutorialDataGraph.name = string.Format("Level{0}TutorialsDG", levelIndex);

            AssetDatabase.CreateAsset(tutorialDataGraph, Path.Combine(tutorialsPath, tutorialDataGraph.name + ".asset"));
            tutorialDataGraph.SetAddressableInfo(AddressablesConstants.LEVELS_GROUP);

            string prefabPath = string.Format("{0}Level{1}Tutorials.prefab", tutorialsPath, levelIndex);
            AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(levelInfo.tutorialPrefabToCopy), prefabPath);

            GameObject createdPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

            FSMRuntime fsmRuntime = createdPrefab.GetComponent<FSMRuntime>();
            if (fsmRuntime == null)
            {
                fsmRuntime = createdPrefab.AddComponent<FSMRuntime>();
            }
            fsmRuntime.graph = tutorialFsm;

            DataRuntime dataRuntime = createdPrefab.GetComponent<DataRuntime>();
            if (dataRuntime == null)
            {
                dataRuntime = createdPrefab.AddComponent<DataRuntime>();
            }
            dataRuntime.graph = tutorialDataGraph;

            createdPrefab.SetAddressableInfo(AddressablesConstants.LEVELS_GROUP);
            EditorUtility.SetDirty(createdPrefab);
        }

        private void CreateLevelData()
        {
            string levelFolderFullPath = LevelFolderFullPath;

            GameObject levelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(Path.Combine(levelFolderFullPath, $"Level{levelInfo.levelIndex}.prefab"));
            Level level = Level.Create(levelPrefab, levelInfo.maxWaypointsPlaceable, levelInfo.requiresFuel, levelInfo.startingFuel, levelInfo.softCurrencyPrize, levelInfo.collectionTargets);
            AssetDatabase.CreateAsset(level, Path.Combine(levelFolderFullPath, string.Format("Level{0}Data", levelInfo.levelIndex) + ".asset"));
            level.SetAddressableInfo(AddressablesConstants.LEVELS_GROUP);

            // Must do this after the asset is actually created
            LevelEditor.FindAllLevelObjects(level);
        }

        #endregion

        #region Utility Methods

        private void ClearAllTilemaps(GameObject gameObject)
        {
            for (int i = 0; i < gameObject.transform.childCount; ++i)
            {
                Tilemap tilemap = gameObject.transform.GetChild(i).GetComponent<Tilemap>();
                if (tilemap != null)
                {
                    tilemap.ClearAllTiles();
                }
            }
        }

        private void DeleteAllChildren(Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; --i)
            {
                GameObject.DestroyImmediate(transform.GetChild(i).gameObject, true);
            }
        }

        private Dictionary<InteractableAction, string> GetInteractableMarkerPrefabs(DoorColour doorColour)
        {
            switch (doorColour)
            {
                case DoorColour.Green:
                    return greenDoorMarkerPrefabs;

                case DoorColour.Red:
                    return redDoorMarkerPrefabs;

                case DoorColour.Blue:
                    return blueDoorMarkerPrefabs;

                case DoorColour.Brown:
                    return brownDoorMarkerPrefabs;

                default:
                    Debug.LogAssertionFormat("Unhandled DoorColour {0} into GetInteractableMarkerPrefabs", doorColour);
                    return new Dictionary<InteractableAction, string>();
            }
        }

        #endregion

        #region Menu Item

        [MenuItem("Robbi/Tools/Create Level")]
        public static void ShowCreateLevelWizard()
        {
            ScriptableWizard.DisplayWizard<CreateLevel>("Create Level", "Create All", "Close");
        }

        #endregion
    }
}
