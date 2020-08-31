using Robbi.Doors;
using Robbi.Events;
using Robbi.Exit;
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

        private string destinationFolder = Path.Combine("Assets", "Resources", "Levels");
        private uint levelIndex = 0;
        private uint numDoors = 1;
        private uint numExits = 1;
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
            numExits = RobbiEditorGUILayout.UIntField("Num Exits", numExits);
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

            if (GUILayout.Button("Create Exits"))
            {
                CreateExits();
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
            CreateExits();
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
            AssetDatabase.CreateFolder(levelFolderFullPath, "Doors");
            AssetDatabase.CreateFolder(levelFolderFullPath, "Events");
            AssetDatabase.CreateFolder(levelFolderFullPath, "Exits");
            AssetDatabase.CreateFolder(levelFolderFullPath, "FSMs");
            AssetDatabase.CreateFolder(levelFolderFullPath, "Prefabs");
            AssetDatabase.CreateFolder(levelFolderFullPath, "Interactables");
        }

        private void CreateDoors()
        {
            string levelFolderFullPath = LevelFolderFullPath;
            
            for (uint i = 0; i < numDoors; ++i)
            {
                Door door = ScriptableObject.CreateInstance<Door>();
                door.name = string.Format("Door{0}", levelIndex);
                
                Vector3IntEvent doorOpenedEvent = AssetDatabase.LoadAssetAtPath<Vector3IntEvent>(Path.Combine("Assets", "Events", "Level", "DoorOpened.asset"));
                Debug.Assert(doorOpenedEvent, "On Door Opened event could not be found automatically");
                door.onDoorOpened = doorOpenedEvent;

                AssetDatabase.CreateAsset(door, Path.Combine(levelFolderFullPath, "Doors", door.name + ".asset"));
            }
        }

        private void CreateExits()
        {
            string levelFolderFullPath = LevelFolderFullPath;

            for (uint i = 0; i < numExits; ++i)
            {
                Exit exit = ScriptableObject.CreateInstance<Exit>();
                exit.name = string.Format("Exit{0}", levelIndex);
                
                Event exitReachedEvent = AssetDatabase.LoadAssetAtPath<Event>(Path.Combine("Assets", "Events", "Level", "ExitReached.asset"));
                Debug.Assert(exitReachedEvent != null, "On Exit Reached event could not be found automatically");
                exit.onExitReached = exitReachedEvent;

                AssetDatabase.CreateAsset(exit, Path.Combine(levelFolderFullPath, "Exits", exit.name + ".asset"));
            }
        }

        private void CreateInteractables()
        {
            string levelFolderFullPath = LevelFolderFullPath;

            for (uint i = 0; i < numInteractables; ++i)
            {
                Interactable interactable = ScriptableObject.CreateInstance<Interactable>();
                interactable.name = string.Format("Interactable{0}", levelIndex);
                interactable.onInteract = CreateInteractionEvent(string.Format("OnInteractable{0}Activated", levelIndex));

                Debug.Assert(interactable.onInteract != null, "On Interact event could not be created successfully");

                AssetDatabase.CreateAsset(interactable, Path.Combine(levelFolderFullPath, "Interactables", interactable.name + ".asset"));
            }
        }

        private Event CreateInteractionEvent(string name)
        {
            Event interactionEvent = ScriptableObject.CreateInstance<Event>();
            interactionEvent.name = name;

            AssetDatabase.CreateAsset(interactionEvent, Path.Combine(LevelFolderFullPath, "Events", interactionEvent.name + ".asset"));
            return interactionEvent;
        }

        private void CreateFSM()
        {
            FSMGraph fsm = ScriptableObject.CreateInstance<FSMGraph>();
            fsm.name = string.Format("Level{0}FSM", levelIndex);

            AssetDatabase.CreateAsset(fsm, Path.Combine(LevelFolderFullPath, "FSMs", fsm.name + ".asset"));
        }

        private void CreatePrefab()
        {
            string levelFolderFullPath = LevelFolderFullPath;
            string prefabPath = Path.Combine(levelFolderFullPath, "Prefabs", string.Format("Level{0}.prefab", levelIndex));

            GameObject levelGameObject = GameObject.Instantiate(levelPrefabToCopy);
            GameObject interactableMarkerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(Path.Combine("Assets", "Prefabs", "Level", "InteractableMarker.prefab"));

            Transform doors = levelGameObject.transform.Find("Doors");
            Transform exits = levelGameObject.transform.Find("Exits");
            Transform interactables = levelGameObject.transform.Find("Interactables");

            DeleteAllChildren(doors);
            DeleteAllChildren(exits);
            DeleteAllChildren(interactables);

            for (uint i = 0; i < numDoors; ++i)
            {
                Door door = AssetDatabase.LoadAssetAtPath<Door>(Path.Combine(levelFolderFullPath, "Doors", string.Format("Door{0}.asset", i)));

                GameObject doorGameObject = new GameObject(string.Format("Door{0}", i), typeof(EventListener));
                doorGameObject.transform.parent = doors;
            }

            for (uint i = 0; i < numExits; ++i)
            {
                Exit exit = AssetDatabase.LoadAssetAtPath<Exit>(Path.Combine(levelFolderFullPath, "Exits", string.Format("Exit{0}.asset", i)));
                Debug.Assert(exit != null, string.Format("Exit{0} could not be found automatically", i));

                GameObject exitGameObject = new GameObject(string.Format("Exit{0}", i));
                exitGameObject.transform.parent = exits;

                Vector3IntEventListener movedToListener = exitGameObject.AddComponent<Vector3IntEventListener>();
                Vector3IntEvent movedToEvent = AssetDatabase.LoadAssetAtPath<Vector3IntEvent>(Path.Combine("Assets", "Events", "Level", "MovedTo.asset"));
                Debug.Assert(movedToEvent != null, "Moved To event could not be found automatically");

                if (movedToEvent != null)
                {
                    movedToListener.gameEvent = movedToEvent;
                    movedToListener.response = new Vector3IntUnityEvent();
                    UnityEventTools.AddPersistentListener(movedToListener.response, exit.TryExit);
                }
            }

            for (uint i = 0; i < numInteractables; ++i)
            {
                Interactable interactable = AssetDatabase.LoadAssetAtPath<Interactable>(Path.Combine(levelFolderFullPath, "Interactables", string.Format("Interactable{0}.asset", i)));
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
            runtime.graph = AssetDatabase.LoadAssetAtPath<FSMGraph>(Path.Combine(levelFolderFullPath, "FSMs", string.Format("Level{0}FSM.asset", levelIndex)));

            PrefabUtility.SaveAsPrefabAsset(levelGameObject, prefabPath);
            GameObject.DestroyImmediate(levelGameObject);
        }

        private void CreateLevelData()
        {
            string levelFolderFullPath = LevelFolderFullPath;

            Level level = ScriptableObject.CreateInstance<Level>();
            level.playerLocalPosition = AssetDatabase.LoadAssetAtPath<Vector3Value>(Path.Combine("Assets", "Data", "Player", "PlayerLocalPosition.asset"));
            level.levelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(Path.Combine(levelFolderFullPath, "Prefabs", string.Format("Level{0}.prefab", levelIndex)));

            Debug.Assert(level.playerLocalPosition != null, "Player Local Position value could not be found automatically");
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
