using Robbi.DataSystem;
using Robbi.FSM;
using Robbi.Levels;
using Robbi.Levels.Elements;
using RobbiEditor.Constants;
using RobbiEditor.Levels.Elements;
using RobbiEditor.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace RobbiEditor.Tools
{
    public enum DoorColour
    { 
        Green,
        Red,
        Blue,
        Grey
    }

    public enum InteractableMarker
    {
        DoorOpen,
        DoorClose
    }

    [Serializable]
    public class LevelInfo : ScriptableObject
    {
        public string destinationFolder = LevelDirectories.FULL_PATH;
        public uint levelIndex = 0;
        public GameObject levelPrefabToCopy;
        public bool clearLevel = true;
        public int maxWaypointsPlaceable = 3; 
        public List<DoorColour> horizontalDoors = new List<DoorColour>();
        public List<DoorColour> verticalDoors = new List<DoorColour>();
        public List<InteractableMarker> interactableMarkers = new List<InteractableMarker>();
        public bool hasTutorial = false;
        public GameObject tutorialPrefabToCopy;
    }

    public class CreateLevel : ScriptableWizard
    {
        #region Properties and Fields

        private string LevelFolderName
        {
            get { return string.Format("Level{0}", levelInfo.levelIndex); }
        }

        private string LevelFolderFullPath
        {
            get { return levelInfo.destinationFolder + "/" + LevelFolderName; }
        }

        private LevelInfo levelInfo;
        private SerializedObject levelInfoObject;

        private static Dictionary<DoorColour, string> horizontalDoorPrefabs = new Dictionary<DoorColour, string>()
        {
            { DoorColour.Green, TileFiles.HORIZONTAL_GREEN_CLOSED_DOOR_TILE },
            { DoorColour.Red, TileFiles.HORIZONTAL_RED_CLOSED_DOOR_TILE },
            { DoorColour.Blue, TileFiles.HORIZONTAL_BLUE_CLOSED_DOOR_TILE },
            { DoorColour.Grey, TileFiles.HORIZONTAL_GREY_CLOSED_DOOR_TILE },
        };

        private static Dictionary<DoorColour, string> verticalDoorPrefabs = new Dictionary<DoorColour, string>()
        {
            { DoorColour.Green, TileFiles.VERTICAL_GREEN_CLOSED_DOOR_TILE },
            { DoorColour.Red, TileFiles.VERTICAL_RED_CLOSED_DOOR_TILE },
            { DoorColour.Blue, TileFiles.VERTICAL_BLUE_CLOSED_DOOR_TILE },
            { DoorColour.Grey, TileFiles.VERTICAL_GREY_CLOSED_DOOR_TILE },
        };

        private static Dictionary<InteractableMarker, string> markerPrefabs = new Dictionary<InteractableMarker, string>()
        {
            { InteractableMarker.DoorOpen, PrefabFiles.DOOR_OPEN_MARKER_PREFAB },
            { InteractableMarker.DoorClose, PrefabFiles.DOOR_CLOSE_MARKER_PREFAB },
        };

        #endregion

        #region GUI

        private void OnEnable()
        {
            levelInfo = ScriptableObject.CreateInstance<LevelInfo>();
            levelInfoObject = new SerializedObject(levelInfo);
        }

        protected override bool DrawWizardGUI()
        {
            bool propertiesChanged = base.DrawWizardGUI();
            EditorGUI.BeginChangeCheck();

            levelInfoObject.Update();

            levelInfo.destinationFolder = EditorGUILayout.TextField(levelInfo.destinationFolder);
            levelInfo.levelIndex = RobbiEditorGUILayout.UIntField("Level Index", levelInfo.levelIndex);
            levelInfo.levelPrefabToCopy = EditorGUILayout.ObjectField("Level Prefab To Copy", levelInfo.levelPrefabToCopy, typeof(GameObject), false) as GameObject;
            levelInfo.clearLevel = EditorGUILayout.Toggle("Clear Level", levelInfo.clearLevel);
            levelInfo.maxWaypointsPlaceable = EditorGUILayout.IntField("Max Waypoints Placeable", levelInfo.maxWaypointsPlaceable);
            propertiesChanged |= EditorGUILayout.PropertyField(levelInfoObject.FindProperty(nameof(levelInfo.horizontalDoors)));
            propertiesChanged |= EditorGUILayout.PropertyField(levelInfoObject.FindProperty(nameof(levelInfo.verticalDoors)));
            propertiesChanged |= EditorGUILayout.PropertyField(levelInfoObject.FindProperty(nameof(levelInfo.interactableMarkers)));

            levelInfo.hasTutorial = EditorGUILayout.Toggle("Has Tutorial", levelInfo.hasTutorial);
            if (levelInfo.hasTutorial)
            {
                levelInfo.tutorialPrefabToCopy = EditorGUILayout.ObjectField("Tutorial Prefab To Copy", levelInfo.tutorialPrefabToCopy, typeof(GameObject), false) as GameObject;
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("Create Directories"))
            {
                CreateDirectories();
            }

            if (GUILayout.Button("Create FSM"))
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

            if (GUILayout.Button("Create Level Data"))
            {
                CreateLevelData();
            }

            if (levelInfo.hasTutorial && GUILayout.Button("Create Tutorial"))
            {
                CreateTutorial();
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

            Log.Clear();

            CreateDirectories();
            CreateFSM();
            CreatePrefab();
            CreateDoors();
            CreateLevelData();
            CreateTutorial();

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
            AssetDatabase.CreateFolder(levelInfo.destinationFolder, LevelFolderName);
        }

        private void CreateFSM()
        {
            FSMGraph fsm = ScriptableObject.CreateInstance<FSMGraph>();
            fsm.name = string.Format("Level{0}FSM", levelInfo.levelIndex);

            AssetDatabase.CreateAsset(fsm, Path.Combine(LevelFolderFullPath, fsm.name + ".asset"));
            fsm.SetAddressableGroup(AddressablesConstants.LEVELS_GROUP);
        }

        private void CreatePrefab()
        {
            string levelFolderFullPath = LevelFolderFullPath;
            string prefabPath = Path.Combine(levelFolderFullPath, string.Format("Level{0}.prefab", levelInfo.levelIndex));
            AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(levelInfo.levelPrefabToCopy), prefabPath);
            
            GameObject createdPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

            if (levelInfo.clearLevel)
            {
                ClearAllTilemaps(createdPrefab);
            }
            
            // Have to delete the interactables in the prefab
            DeleteAllChildren(createdPrefab.transform.Find("Interactables"));
            
            // Then instantiate the prefab so we can add the interactables
            // I kept having a crash in Unity when I tried adding these directly to the prefab
            GameObject instantiatedPrefab = PrefabUtility.InstantiatePrefab(createdPrefab) as GameObject;
            Transform interactables = instantiatedPrefab.transform.Find("Interactables");

            for (uint i = 0; i < levelInfo.interactableMarkers.Count; ++i)
            {
                InteractableMarker markerType = levelInfo.interactableMarkers[(int)i];
                GameObject interactableMarkerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(markerPrefabs[markerType]);
                GameObject interactableMarker = PrefabUtility.InstantiatePrefab(interactableMarkerPrefab, interactables) as GameObject;
                interactableMarker.name = string.Format("Interactable{0}{1}", markerType, i);
                
                PrefabUtility.ApplyAddedGameObject(interactableMarker, prefabPath, InteractionMode.AutomatedAction);
            }

            FSMRuntime runtime = createdPrefab.GetComponent<FSMRuntime>();
            if (runtime == null)
            {
                runtime = createdPrefab.AddComponent<FSMRuntime>();
            }
            runtime.graph = AssetDatabase.LoadAssetAtPath<FSMGraph>(Path.Combine(levelFolderFullPath, string.Format("Level{0}FSM.asset", levelInfo.levelIndex)));

            GameObject.DestroyImmediate(instantiatedPrefab);
            createdPrefab.SetAddressableGroup(AddressablesConstants.LEVELS_GROUP);
            EditorUtility.SetDirty(createdPrefab);
        }

        private void CreateDoors()
        {
            foreach (DoorColour doorColour in levelInfo.horizontalDoors)
            {
                DoorEditor.CreateHorizontalDoor(string.Format("Level{0}{1}Door", levelInfo.levelIndex, doorColour), LevelFolderFullPath, horizontalDoorPrefabs[doorColour]);
            }

            foreach (DoorColour doorColour in levelInfo.verticalDoors)
            {
                DoorEditor.CreateVerticalDoor(string.Format("Level{0}{1}Door", levelInfo.levelIndex, doorColour), LevelFolderFullPath, verticalDoorPrefabs[doorColour]);
            }
        }

        private void CreateLevelData()
        {
            string levelFolderFullPath = LevelFolderFullPath;

            Level level = ScriptableObject.CreateInstance<Level>();
            level.levelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(Path.Combine(levelFolderFullPath, string.Format("Level{0}.prefab", levelInfo.levelIndex)));
            level.maxWaypointsPlaceable = levelInfo.maxWaypointsPlaceable;

            Debug.Assert(level.levelPrefab != null, "Level Prefab could not be found automatically");

            AssetDatabase.CreateAsset(level, Path.Combine(levelFolderFullPath, string.Format("Level{0}Data", levelInfo.levelIndex) + ".asset"));
            level.SetAddressableGroup(AddressablesConstants.LEVELS_GROUP);
        }

        private void CreateTutorial()
        {
            if (!levelInfo.hasTutorial)
            {
                return;
            }

            uint levelIndex = levelInfo.levelIndex;

            FSMGraph tutorialFsm = ScriptableObject.CreateInstance<FSMGraph>();
            tutorialFsm.name = string.Format("Level{0}TutorialsFSM", levelIndex);

            AssetDatabase.CreateAsset(tutorialFsm, Path.Combine(LevelFolderFullPath, tutorialFsm.name + ".asset"));
            tutorialFsm.SetAddressableGroup(AddressablesConstants.LEVELS_GROUP);

            DataGraph tutorialDataGraph = ScriptableObject.CreateInstance<DataGraph>();
            tutorialDataGraph.name = string.Format("Level{0}TutorialsDataGraph", levelIndex);

            AssetDatabase.CreateAsset(tutorialDataGraph, Path.Combine(LevelFolderFullPath, tutorialDataGraph.name + ".asset"));
            tutorialDataGraph.SetAddressableGroup(AddressablesConstants.LEVELS_GROUP);

            string levelFolderFullPath = LevelFolderFullPath;
            string prefabPath = Path.Combine(levelFolderFullPath, string.Format("Level{0}Tutorials.prefab", levelIndex));
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

            createdPrefab.SetAddressableGroup(AddressablesConstants.LEVELS_GROUP);
            EditorUtility.SetDirty(createdPrefab);
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
