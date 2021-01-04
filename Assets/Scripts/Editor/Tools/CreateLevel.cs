using Celeste.Attributes.GUI;
using Celeste.DS;
using Celeste.FSM;
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
        ToggleDoor,
    }

    [Serializable]
    public struct InteractableMarker
    {
        public DoorColour doorColour;
        public InteractableAction interactableAction;
    }

    [Serializable]
    public class LevelInfo : ScriptableObject
    {
        public string destinationFolder = LevelDirectories.LEVELS_PATH;
        public uint levelIndex = 0;
        public bool increaseMaxLevel = true;
        public GameObject levelPrefabToCopy;
        public bool clearLevel = true;

        [Header("Parameters")]
        public int maxWaypointsPlaceable = 3;
        public bool requiresFuel = false;
        [ShowIf("requiresFuel")]
        public uint startingFuel = 0;

        [Header("Doors")]
        public List<Tuple<DoorColour, DoorState>> doors = new List<Tuple<DoorColour, DoorState>>();

        [Header("Interactables")]
        public List<InteractableMarker> interactableMarkers = new List<InteractableMarker>();
        public int numInteractables;
        public int numInteractableStateMachines;

        [Header("Collectables")]
        public int numCollectables;

        [Header("Portals")]
        public int numPortals;

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

        private static Dictionary<InteractableAction, string> markerPrefabs = new Dictionary<InteractableAction, string>()
        {
            { InteractableAction.OpenDoor, PrefabFiles.DOOR_OPEN_MARKER_PREFAB },
            { InteractableAction.CloseDoor, PrefabFiles.DOOR_CLOSE_MARKER_PREFAB },
            { InteractableAction.ToggleDoor, PrefabFiles.DOOR_TOGGLE_MARKER_PREFAB },
        };

        #endregion

        #region GUI

        private void OnEnable()
        {
            LevelManager levelManager = LevelManager.EditorOnly_Load();

            levelInfo = ScriptableObject.CreateInstance<LevelInfo>();
            levelInfo.levelIndex = levelManager.LatestAvailableLevel_DefaultValue + 1;
            
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

            if (GUILayout.Button("Create Directories", GUILayout.ExpandWidth(false)))
            {
                CreateDirectories();
            }

            if (levelInfo.hasFsm && GUILayout.Button("Create FSM", GUILayout.ExpandWidth(false)))
            {
                CreateFSM();
            }

            if (GUILayout.Button("Create Prefab", GUILayout.ExpandWidth(false)))
            {
                CreatePrefab();
            }

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

            if (levelInfo.hasTutorial && GUILayout.Button("Create Tutorial", GUILayout.ExpandWidth(false)))
            {
                CreateTutorial();
            }

            if (GUILayout.Button("Create Level Data", GUILayout.ExpandWidth(false)))
            {
                CreateLevelData();
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

            CreateDirectories();
            CreateFSM();
            CreatePrefab();
            CreateDoors();
            CreateInteractables();
            CreateInteractableStateMachines();
            CreateCollectables();
            CreatePortals();
            CreateTutorial();
            CreateLevelData();

            if (levelInfo.increaseMaxLevel)
            {
                LevelManager.EditorOnly_Load().LatestAvailableLevel_DefaultValue = levelInfo.levelIndex;
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private void OnWizardOtherButton()
        {
            Close();
        }

        #endregion

        #region Creation Methods

        private void CreateDirectories()
        {
            AssetUtility.CreateFolder(levelInfo.destinationFolder, LevelFolderName);

            string levelFolderPath = string.Format("{0}{1}", levelInfo.destinationFolder, LevelFolderName);
            
            if (levelInfo.doors.Count > 0)
            {
                AssetUtility.CreateFolder(levelFolderPath, DOORS_NAME);
            }

            if (levelInfo.hasTutorial)
            {
                AssetUtility.CreateFolder(levelFolderPath, TUTORIALS_NAME);
            }

            if (levelInfo.numInteractables > 0 || levelInfo.numInteractableStateMachines > 0)
            {
                AssetUtility.CreateFolder(levelFolderPath, INTERACTABLES_NAME);
            }

            if (levelInfo.numCollectables > 0)
            {
                AssetUtility.CreateFolder(levelFolderPath, COLLECTABLES_NAME);
            }

            if (levelInfo.numPortals > 0)
            {
                AssetUtility.CreateFolder(levelFolderPath, PORTALS_NAME);
            }
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
                    GameObject interactableMarkerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(markerPrefabs[interactableMarker.interactableAction]);
                    GameObject interactableMarkerGameObject = PrefabUtility.InstantiatePrefab(interactableMarkerPrefab, instantiatedLevelRoot.interactablesTilemap.transform) as GameObject;
                    interactableMarkerGameObject.name = string.Format("{0}Switch{1}", interactableMarker.doorColour, interactableMarker.interactableAction);

                    DoorColourHelper doorColourHelper = interactableMarkerGameObject.GetComponentInChildren<DoorColourHelper>();
                    doorColourHelper.icon.color = DoorColours.COLOURS[(int)interactableMarker.doorColour];

                    PrefabUtility.ApplyAddedGameObject(interactableMarkerGameObject, prefabPath, InteractionMode.AutomatedAction);
                }

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

                GameObject.DestroyImmediate(instantiatedPrefab);
            }

            createdPrefab.SetAddressableInfo(AddressablesConstants.LEVELS_GROUP);
            EditorUtility.SetDirty(createdPrefab);
        }

        private void CreateDoors()
        {
            string doorsPath = string.Format("{0}{1}", LevelFolderFullPath, DOORS_NAME);

            foreach (Tuple<DoorColour, DoorState> doorInfo in levelInfo.doors)
            {
                DoorEditor.CreateDoor(string.Format("Level{0}{1}Door", levelInfo.levelIndex, doorInfo.Item1), doorsPath, doorInfo.Item2);
            }
        }

        private void CreateInteractables()
        {
            string interactablesPath = string.Format("{0}{1}", LevelFolderFullPath, INTERACTABLES_NAME);

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
            string interactablesPath = string.Format("{0}{1}", LevelFolderFullPath, INTERACTABLES_NAME);

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
            string collectablesPath = string.Format("{0}{1}", LevelFolderFullPath, COLLECTABLES_NAME);

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
            string portalsPath = string.Format("{0}{1}", LevelFolderFullPath, PORTALS_NAME);
            if (!Directory.Exists(portalsPath))
            {
                AssetDatabase.CreateFolder(LevelFolderFullPath, PORTALS_NAME);
            }

            for (int i = 0; i < levelInfo.numPortals; ++i)
            {
                Portal portal = ScriptableObject.CreateInstance<Portal>();
                portal.name = string.Format("Level{0}Portal{1}", levelInfo.levelIndex, i);
                AssetDatabase.CreateAsset(portal, string.Format("{0}{1}.asset", portalsPath, portal.name));
                portal.SetAddressableInfo(AddressablesConstants.LEVELS_GROUP);
            }
        }

        private void CreateTutorial()
        {
            if (!levelInfo.hasTutorial)
            {
                return;
            }

            string tutorialsPath = string.Format("{0}{1}", LevelFolderFullPath, TUTORIALS_NAME);
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

            Level level = ScriptableObject.CreateInstance<Level>();
            level.levelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(Path.Combine(levelFolderFullPath, string.Format("Level{0}.prefab", levelInfo.levelIndex)));
            level.levelTutorial = levelInfo.hasTutorial ? 
                AssetDatabase.LoadAssetAtPath<GameObject>(string.Format("{0}{1}Level{2}Tutorials.prefab", levelFolderFullPath, TUTORIALS_NAME, levelInfo.levelIndex))
                : null;
            level.maxWaypointsPlaceable = levelInfo.maxWaypointsPlaceable;
            level.requiresFuel = levelInfo.requiresFuel;
            level.startingFuel = levelInfo.startingFuel;
            
            Debug.Assert(level.levelPrefab != null, "Level Prefab could not be found automatically");
            Debug.Assert(!levelInfo.hasTutorial || level.levelTutorial != null, "Level Tutorial could not be found automatically");

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
