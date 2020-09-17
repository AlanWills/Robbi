using Robbi.Doors;
using Robbi.Events;
using Robbi.FSM;
using Robbi.Interactables;
using Robbi.Levels;
using Robbi.Parameters;
using RobbiEditor.Utils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.Events;
using Event = Robbi.Events.Event;

namespace RobbiEditor.Tools
{
    public class CreateLevelWizard : ScriptableWizard
    {
        #region Properties and Fields

        private string LevelFolderName
        {
            get { return string.Format("Level{0}", levelIndex); }
        }

        private string LevelFolderFullPath
        {
            get { return Path.Combine(destinationFolder, LevelFolderName); }
        }

        private string destinationFolder = LevelDirectories.FULL_PATH;
        private uint levelIndex = 0;
        private uint numDoors = 1;
        private uint numInteractables = 1;
        private GameObject levelPrefabToCopy;

        #endregion

        #region GUI

        protected override bool DrawWizardGUI()
        {
            bool propertiesChanged = base.DrawWizardGUI();
            EditorGUI.BeginChangeCheck();

            destinationFolder = EditorGUILayout.TextField(destinationFolder);
            levelIndex = RobbiEditorGUILayout.UIntField("Level Index", levelIndex);
            numDoors = RobbiEditorGUILayout.UIntField("Num Doors", numDoors);
            numInteractables = RobbiEditorGUILayout.UIntField("Num Interactables", numInteractables);
            levelPrefabToCopy = EditorGUILayout.ObjectField("Level Prefab To Copy", levelPrefabToCopy, typeof(GameObject), false) as GameObject;

            EditorGUILayout.Space();

            if (GUILayout.Button("Create Directories"))
            {
                CreateDirectories();
            }

            if (GUILayout.Button("Create Doors"))
            {
                CreateDoors();
            }

            if (GUILayout.Button("Create Interactables"))
            {
                CreateInteractables();
            }

            if (GUILayout.Button("Create FSM"))
            {
                CreateFSM();
            }

            if (GUILayout.Button("Create Prefab"))
            {
                CreatePrefab();
            }

            if (GUILayout.Button("Create Level Data"))
            {
                CreateLevelData();
            }

            return propertiesChanged || EditorGUI.EndChangeCheck();
        }

        private void OnWizardCreate()
        {
            if (Directory.Exists(Path.Combine(Application.dataPath, LevelFolderFullPath)))
            {
                EditorUtility.DisplayDialog("Error", string.Format("Folder for Level Index {0} already exists.  Abandoning creation.", levelIndex), "OK");
                return;
            }

            Log.Clear();

            CreateDirectories();
            CreateDoors();
            CreateInteractables();
            CreateFSM();
            CreatePrefab();
            CreateLevelData();  // Must happen last

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        #endregion

        #region Creation Methods

        private void CreateDirectories()
        {
            string levelFolderFullPath = LevelFolderFullPath;

            AssetDatabase.CreateFolder(destinationFolder, LevelFolderName);
            AssetDatabase.CreateFolder(levelFolderFullPath, LevelDirectories.DOORS_NAME);
            AssetDatabase.CreateFolder(levelFolderFullPath, LevelDirectories.EVENTS_NAME);
            AssetDatabase.CreateFolder(levelFolderFullPath, LevelDirectories.FSMS_NAME);
            AssetDatabase.CreateFolder(levelFolderFullPath, LevelDirectories.PREFABS_NAME);
            AssetDatabase.CreateFolder(levelFolderFullPath, LevelDirectories.INTERACTABLES_NAME);
        }

        private void CreateDoors()
        {
            string levelFolderFullPath = LevelFolderFullPath;
            
            for (uint i = 0; i < numDoors; ++i)
            {
                Door door = ScriptableObject.CreateInstance<Door>();
                door.name = string.Format("Door{0}", i);
                
                Vector3IntEvent doorOpenedEvent = AssetDatabase.LoadAssetAtPath<Vector3IntEvent>(EventFiles.DOOR_OPENED_EVENT);
                Debug.Assert(doorOpenedEvent, "On Door Opened event could not be found automatically");
                door.onDoorOpened = doorOpenedEvent;

                AssetDatabase.CreateAsset(door, Path.Combine(levelFolderFullPath, LevelDirectories.DOORS_NAME, door.name + ".asset"));
            }
        }

        private void CreateInteractables()
        {
            string levelFolderFullPath = LevelFolderFullPath;

            for (uint i = 0; i < numInteractables; ++i)
            {
                Interactable interactable = ScriptableObject.CreateInstance<Interactable>();
                interactable.name = string.Format("Interactable{0}", i);
                interactable.onInteract = CreateInteractionEvent(string.Format("OnInteractable{0}Activated", levelIndex));

                Debug.Assert(interactable.onInteract != null, "On Interact event could not be created successfully");

                AssetDatabase.CreateAsset(interactable, Path.Combine(levelFolderFullPath, LevelDirectories.INTERACTABLES_NAME, interactable.name + ".asset"));
            }
        }

        private Event CreateInteractionEvent(string name)
        {
            Event interactionEvent = ScriptableObject.CreateInstance<Event>();
            interactionEvent.name = name;

            AssetDatabase.CreateAsset(interactionEvent, Path.Combine(LevelFolderFullPath, LevelDirectories.EVENTS_NAME, interactionEvent.name + ".asset"));
            return interactionEvent;
        }

        private void CreateFSM()
        {
            FSMGraph fsm = ScriptableObject.CreateInstance<FSMGraph>();
            fsm.name = string.Format("Level{0}FSM", levelIndex);

            AssetDatabase.CreateAsset(fsm, Path.Combine(LevelFolderFullPath, LevelDirectories.FSMS_NAME, fsm.name + ".asset"));
        }

        private void CreatePrefab()
        {
            string levelFolderFullPath = LevelFolderFullPath;
            string prefabPath = Path.Combine(levelFolderFullPath, LevelDirectories.PREFABS_NAME, string.Format("Level{0}.prefab", levelIndex));

            GameObject levelGameObject = GameObject.Instantiate(levelPrefabToCopy);
            GameObject interactableMarkerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabFiles.INTERACTABLE_MARKER_PREFAB);

            Transform doors = levelGameObject.transform.Find("Doors");
            Transform exits = levelGameObject.transform.Find("Exits");
            Transform interactables = levelGameObject.transform.Find("Interactables");

            DeleteAllChildren(doors);
            DeleteAllChildren(exits);
            DeleteAllChildren(interactables);

            for (uint i = 0; i < numDoors; ++i)
            {
                Door door = AssetDatabase.LoadAssetAtPath<Door>(Path.Combine(levelFolderFullPath, LevelDirectories.DOORS_NAME, string.Format("Door{0}.asset", i)));

                GameObject doorGameObject = new GameObject(string.Format("Door{0}", i), typeof(EventListener));
                doorGameObject.transform.parent = doors;

                EventListener eventListener = doorGameObject.GetComponent<EventListener>();
                UnityEventTools.AddVoidPersistentListener(eventListener.response, door.Open);
            }

            for (uint i = 0; i < numInteractables; ++i)
            {
                Interactable interactable = AssetDatabase.LoadAssetAtPath<Interactable>(Path.Combine(levelFolderFullPath, LevelDirectories.INTERACTABLES_NAME, string.Format("Interactable{0}.asset", i)));
                Debug.Assert(interactable != null, string.Format("Interactable{0} could not be loaded", i));

                GameObject interactableMarker = PrefabUtility.InstantiatePrefab(interactableMarkerPrefab, interactables) as GameObject;
                interactableMarker.name = string.Format("Interactable{0}", i);
                interactableMarker.GetComponent<InteractableMarker>().interactable = interactable;
            }

            FSMRuntime runtime = levelGameObject.GetComponent<FSMRuntime>();
            if (runtime == null)
            {
                runtime = levelGameObject.AddComponent<FSMRuntime>();
            }
            runtime.graph = AssetDatabase.LoadAssetAtPath<FSMGraph>(Path.Combine(levelFolderFullPath, LevelDirectories.FSMS_NAME, string.Format("Level{0}FSM.asset", levelIndex)));

            PrefabUtility.SaveAsPrefabAsset(levelGameObject, prefabPath);
            GameObject.DestroyImmediate(levelGameObject);
        }

        private void CreateLevelData()
        {
            string levelFolderFullPath = LevelFolderFullPath;

            Level level = ScriptableObject.CreateInstance<Level>();
            level.levelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(Path.Combine(levelFolderFullPath, LevelDirectories.PREFABS_NAME, string.Format("Level{0}.prefab", levelIndex)));

            Debug.Assert(level.levelPrefab != null, "Level Prefab could not be found automatically");

            AssetDatabase.CreateAsset(level, Path.Combine(levelFolderFullPath, string.Format("Level{0}Data", levelIndex) + ".asset"));
        }

        #endregion

        #region Utility Methods

        private void DeleteAllChildren(Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; --i)
            {
                GameObject.DestroyImmediate(transform.GetChild(i).gameObject, true);
            }
        }

        #endregion

        #region Menu Item

        [MenuItem("Window/Robbi/Tools/Create Level Wizard")]
        public static void ShowCreateLevelWizard()
        {
            ScriptableWizard.DisplayWizard<CreateLevelWizard>("Create Level Wizard", "Create All", "Close");
        }

        #endregion
    }
}
