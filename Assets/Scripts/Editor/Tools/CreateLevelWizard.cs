﻿using Robbi.DataSystem;
using Robbi.FSM;
using Robbi.Levels;
using Robbi.Levels.Elements;
using RobbiEditor.Constants;
using RobbiEditor.Levels.Elements;
using RobbiEditor.Utils;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

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
            get { return destinationFolder + "/" + LevelFolderName; }
        }

        private string destinationFolder = LevelDirectories.FULL_PATH;
        private uint levelIndex = 0;
        private uint numInteractables = 0;
        private uint numHorizontalDoors = 0;
        private uint numVerticalDoors = 0;
        private bool hasTutorial = false;
        private GameObject levelPrefabToCopy;
        private GameObject tutorialPrefabToCopy;

        #endregion

        #region GUI

        protected override bool DrawWizardGUI()
        {
            bool propertiesChanged = base.DrawWizardGUI();
            EditorGUI.BeginChangeCheck();

            destinationFolder = EditorGUILayout.TextField(destinationFolder);
            levelIndex = RobbiEditorGUILayout.UIntField("Level Index", levelIndex);
            numInteractables = RobbiEditorGUILayout.UIntField("Num Interactables", numInteractables);
            numHorizontalDoors = RobbiEditorGUILayout.UIntField("Num Horizontal Doors", numHorizontalDoors);
            numVerticalDoors = RobbiEditorGUILayout.UIntField("Num Vertical Doors", numVerticalDoors);
            hasTutorial = EditorGUILayout.Toggle("Has Tutorial", hasTutorial);
            levelPrefabToCopy = EditorGUILayout.ObjectField("Level Prefab To Copy", levelPrefabToCopy, typeof(GameObject), false) as GameObject;

            if (hasTutorial)
            {
                tutorialPrefabToCopy = EditorGUILayout.ObjectField("Tutorial Prefab To Copy", tutorialPrefabToCopy, typeof(GameObject), false) as GameObject;
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

            if (hasTutorial && GUILayout.Button("Create Tutorial"))
            {
                CreateTutorial();
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
            AssetDatabase.CreateFolder(destinationFolder, LevelFolderName);
        }

        private void CreateFSM()
        {
            FSMGraph fsm = ScriptableObject.CreateInstance<FSMGraph>();
            fsm.name = string.Format("Level{0}FSM", levelIndex);

            AssetDatabase.CreateAsset(fsm, Path.Combine(LevelFolderFullPath, fsm.name + ".asset"));
            fsm.SetAddressableGroup(AddressablesConstants.LEVELS_GROUP);
        }

        private void CreatePrefab()
        {
            string levelFolderFullPath = LevelFolderFullPath;
            string prefabPath = Path.Combine(levelFolderFullPath, string.Format("Level{0}.prefab", levelIndex));
            AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(levelPrefabToCopy), prefabPath);
            
            GameObject createdPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            GameObject interactableMarkerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabFiles.INTERACTABLE_MARKER_PREFAB);
            
            // Have to delete the interactables in the prefab
            DeleteAllChildren(createdPrefab.transform.Find("Interactables"));
            
            // Then instantiate the prefab so we can add the interactables
            // I kept having a crash in Unity when I tried adding these directly to the prefab
            GameObject instantiatedPrefab = PrefabUtility.InstantiatePrefab(createdPrefab) as GameObject;
            Transform interactables = instantiatedPrefab.transform.Find("Interactables");

            for (uint i = 0; i < numInteractables; ++i)
            {
                GameObject interactableMarker = PrefabUtility.InstantiatePrefab(interactableMarkerPrefab, interactables) as GameObject;
                interactableMarker.name = string.Format("Interactable{0}", i);
                
                PrefabUtility.ApplyAddedGameObject(interactableMarker, prefabPath, InteractionMode.AutomatedAction);
            }

            FSMRuntime runtime = createdPrefab.GetComponent<FSMRuntime>();
            if (runtime == null)
            {
                runtime = createdPrefab.AddComponent<FSMRuntime>();
            }
            runtime.graph = AssetDatabase.LoadAssetAtPath<FSMGraph>(Path.Combine(levelFolderFullPath, string.Format("Level{0}FSM.asset", levelIndex)));

            GameObject.DestroyImmediate(instantiatedPrefab);
            createdPrefab.SetAddressableGroup(AddressablesConstants.LEVELS_GROUP);
            EditorUtility.SetDirty(createdPrefab);
        }

        private void CreateDoors()
        {
            for (int i = 0; i < numHorizontalDoors; ++i)
            {
                DoorEditor.CreateHorizontalDoor(string.Format("Level{0}Door{1}", levelIndex, i), LevelFolderFullPath);
            }

            for (int i = 0; i < numVerticalDoors; ++i)
            {
                DoorEditor.CreateVerticalDoor(string.Format("Level{0}Door{1}", levelIndex, i), LevelFolderFullPath);
            }
        }

        private void CreateLevelData()
        {
            string levelFolderFullPath = LevelFolderFullPath;

            Level level = ScriptableObject.CreateInstance<Level>();
            level.levelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(Path.Combine(levelFolderFullPath, string.Format("Level{0}.prefab", levelIndex)));

            Debug.Assert(level.levelPrefab != null, "Level Prefab could not be found automatically");

            AssetDatabase.CreateAsset(level, Path.Combine(levelFolderFullPath, string.Format("Level{0}Data", levelIndex) + ".asset"));
            level.SetAddressableGroup(AddressablesConstants.LEVELS_GROUP);
        }

        private void CreateTutorial()
        {
            if (!hasTutorial)
            {
                return;
            }

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
            AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(tutorialPrefabToCopy), prefabPath);

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

        private void DeleteAllChildren(Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; --i)
            {
                GameObject.DestroyImmediate(transform.GetChild(i).gameObject, true);
            }
        }

        #endregion

        #region Menu Item

        [MenuItem("Robbi/Tools/Create Level Wizard")]
        public static void ShowCreateLevelWizard()
        {
            ScriptableWizard.DisplayWizard<CreateLevelWizard>("Create Level Wizard", "Create All", "Close");
        }

        #endregion
    }
}
