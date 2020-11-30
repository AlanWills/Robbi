using Robbi.DataSystem;
using Robbi.FSM;
using Robbi.Levels;
using Robbi.Levels.Elements;
using RobbiEditor.Constants;
using RobbiEditor.Levels;
using RobbiEditor.Levels.Elements;
using RobbiEditor.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using static RobbiEditor.LevelDirectories;

namespace RobbiEditor.Tools
{
    public enum DoorState
    {
        Open,
        Close
    }

    [Serializable]
    public struct DoorMarker
    {
        public DoorColour doorColour;
        public DoorState doorState;
    }

    [Serializable]
    public class LevelInfo : ScriptableObject
    {
        public string destinationFolder = LevelDirectories.LEVELS_PATH;
        public uint levelIndex = 0;
        public bool increaseMaxLevel = true;
        public GameObject levelPrefabToCopy;
        public bool clearLevel = true;
        public int maxWaypointsPlaceable = 3; 
        public List<DoorColour> horizontalDoors = new List<DoorColour>();
        public List<DoorColour> verticalDoors = new List<DoorColour>();
        public List<DoorMarker> interactableMarkers = new List<DoorMarker>();
        public int numInteractables;
        public bool hasTutorial = false;
        public GameObject tutorialPrefabToCopy;
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

        private static Dictionary<DoorColour, Tuple<string, string, string>> horizontalTiles = new Dictionary<DoorColour, Tuple<string, string, string>>()
        {
            { DoorColour.Green, TileFiles.HORIZONTAL_GREEN_DOOR },
            { DoorColour.Red, TileFiles.HORIZONTAL_RED_DOOR },
            { DoorColour.Blue, TileFiles.HORIZONTAL_BLUE_DOOR },
            { DoorColour.Grey, TileFiles.HORIZONTAL_GREY_DOOR },
        };

        private static Dictionary<DoorColour, Tuple<string, string, string>> verticalTiles = new Dictionary<DoorColour, Tuple<string, string, string>>()
        {
            { DoorColour.Green, TileFiles.VERTICAL_GREEN_DOOR },
            { DoorColour.Red, TileFiles.VERTICAL_RED_DOOR },
            { DoorColour.Blue, TileFiles.VERTICAL_BLUE_DOOR },
            { DoorColour.Grey, TileFiles.VERTICAL_GREY_DOOR },
        };

        private static Dictionary<DoorState, string> markerPrefabs = new Dictionary<DoorState, string>()
        {
            { DoorState.Open, PrefabFiles.DOOR_OPEN_MARKER_PREFAB },
            { DoorState.Close, PrefabFiles.DOOR_CLOSE_MARKER_PREFAB },
        };

        #endregion

        #region GUI

        private void OnEnable()
        {
            LevelManager levelManager = LevelManager.EditorOnly_Load();

            levelInfo = ScriptableObject.CreateInstance<LevelInfo>();
            levelInfo.levelIndex = levelManager.LatestLevelIndex_DefaultValue + 1;

            levelInfoObject = new SerializedObject(levelInfo);
        }

        protected override bool DrawWizardGUI()
        {
            bool propertiesChanged = base.DrawWizardGUI();
            EditorGUI.BeginChangeCheck();

            levelInfoObject.Update();

            levelInfo.destinationFolder = EditorGUILayout.TextField(levelInfo.destinationFolder);
            levelInfo.levelIndex = RobbiEditorGUILayout.UIntField("Level Index", levelInfo.levelIndex);
            levelInfo.increaseMaxLevel = EditorGUILayout.Toggle("Increase Max Level", levelInfo.increaseMaxLevel);
            levelInfo.levelPrefabToCopy = EditorGUILayout.ObjectField("Level Prefab To Copy", levelInfo.levelPrefabToCopy, typeof(GameObject), false) as GameObject;
            levelInfo.clearLevel = EditorGUILayout.Toggle("Clear Level", levelInfo.clearLevel);
            levelInfo.maxWaypointsPlaceable = EditorGUILayout.IntField("Max Waypoints Placeable", levelInfo.maxWaypointsPlaceable);
            propertiesChanged |= EditorGUILayout.PropertyField(levelInfoObject.FindProperty(nameof(levelInfo.horizontalDoors)));
            propertiesChanged |= EditorGUILayout.PropertyField(levelInfoObject.FindProperty(nameof(levelInfo.verticalDoors)));
            propertiesChanged |= EditorGUILayout.PropertyField(levelInfoObject.FindProperty(nameof(levelInfo.interactableMarkers)));

            levelInfo.numInteractables = EditorGUILayout.IntField("Num Interactables", levelInfo.numInteractables);

            levelInfo.hasTutorial = EditorGUILayout.Toggle("Has Tutorial", levelInfo.hasTutorial);
            if (levelInfo.hasTutorial)
            {
                levelInfo.tutorialPrefabToCopy = EditorGUILayout.ObjectField("Tutorial Prefab To Copy", levelInfo.tutorialPrefabToCopy, typeof(GameObject), false) as GameObject;
            }

            levelInfo.hasFsm = EditorGUILayout.Toggle("Has FSM", levelInfo.hasFsm);

            EditorGUILayout.Space();

            if (GUILayout.Button("Create Directories"))
            {
                CreateDirectories();
            }

            if (levelInfo.hasFsm && GUILayout.Button("Create FSM"))
            {
                CreateFSM();
            }

            if (GUILayout.Button("Create Prefab"))
            {
                CreatePrefab();
            }

            if (GUILayout.Button("Create Doors"))
            {
                CreateDoors();
            }

            if (GUILayout.Button("Create Interactables"))
            {
                CreateInteractables();
            }

            if (levelInfo.hasTutorial && GUILayout.Button("Create Tutorial"))
            {
                CreateTutorial();
            }

            if (GUILayout.Button("Create Level Data"))
            {
                CreateLevelData();
            }

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

            LogUtils.Clear();

            CreateDirectories();
            CreateFSM();
            CreatePrefab();
            CreateDoors();
            CreateInteractables();
            CreateTutorial();
            CreateLevelData();

            if (levelInfo.increaseMaxLevel)
            {
                LevelManager.EditorOnly_Load().LatestLevelIndex_DefaultValue = levelInfo.levelIndex;
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
            
            if (levelInfo.horizontalDoors.Count > 0 || levelInfo.verticalDoors.Count > 0)
            {
                AssetUtility.CreateFolder(levelFolderPath, DOORS_NAME);
            }

            if (levelInfo.hasTutorial)
            {
                AssetUtility.CreateFolder(levelFolderPath, TUTORIALS_NAME);
            }

            if (levelInfo.numInteractables > 0)
            {
                AssetUtility.CreateFolder(levelFolderPath, INTERACTABLES_NAME);
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
                    DoorMarker doorMarker = levelInfo.interactableMarkers[(int)i];
                    GameObject interactableMarkerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(markerPrefabs[doorMarker.doorState]);
                    GameObject interactableMarker = PrefabUtility.InstantiatePrefab(interactableMarkerPrefab, instantiatedLevelRoot.interactablesTilemap.transform) as GameObject;
                    interactableMarker.name = string.Format("{0}Switch{1}", doorMarker.doorColour, doorMarker.doorState);

                    DoorColourHelper doorColourHelper = interactableMarker.GetComponentInChildren<DoorColourHelper>();
                    doorColourHelper.icon.color = DoorColours.COLOURS[(int)doorMarker.doorColour];

                    PrefabUtility.ApplyAddedGameObject(interactableMarker, prefabPath, InteractionMode.AutomatedAction);
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

            foreach (DoorColour doorColour in levelInfo.horizontalDoors)
            {
                Tuple<string, string, string> tiles = horizontalTiles[doorColour];
                DoorEditor.CreateDoor(string.Format("Level{0}Horizontal{1}Door", levelInfo.levelIndex, doorColour), doorsPath, Direction.Horizontal, tiles.Item1, tiles.Item2, tiles.Item3);
            }

            foreach (DoorColour doorColour in levelInfo.verticalDoors)
            {
                Tuple<string, string, string> tiles = verticalTiles[doorColour];
                DoorEditor.CreateDoor(string.Format("Level{0}Vertical{1}Door", levelInfo.levelIndex, doorColour), doorsPath, Direction.Vertical, tiles.Item1, tiles.Item2, tiles.Item3);
            }
        }

        private void CreateInteractables()
        {
            string interactablesPath = string.Format("{0}{1}", LevelFolderFullPath, INTERACTABLES_NAME);

            for (int i = 0; i < levelInfo.numInteractables; ++i)
            {
                Interactable interactable = ScriptableObject.CreateInstance<Interactable>();
                interactable.name = string.Format("Interactable{0}", i);
                AssetDatabase.CreateAsset(interactable, string.Format("{0}{1}.asset", interactablesPath, interactable.name));
                interactable.SetAddressableInfo(AddressablesConstants.LEVELS_GROUP);
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
            tutorialDataGraph.name = string.Format("Level{0}TutorialsDataGraph", levelIndex);

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
            
            Debug.Assert(level.levelPrefab != null, "Level Prefab could not be found automatically");
            Debug.Assert(!levelInfo.hasTutorial || level.levelTutorial != null, "Level Tutorial could not be found automatically");

            AssetDatabase.CreateAsset(level, Path.Combine(levelFolderFullPath, string.Format("Level{0}Data", levelInfo.levelIndex) + ".asset"));
            level.SetAddressableInfo(AddressablesConstants.LEVELS_GROUP);

            // Must do this after the asset is actually created
            LevelEditor.FindInteractables(level);
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
