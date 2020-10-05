using Robbi.FSM;
using Robbi.Levels;
using RobbiEditor.Assets;
using RobbiEditor.Constants;
using RobbiEditor.Utils;
using System.IO;
using UnityEditor;
using UnityEngine;

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
        private uint numInteractables = 0;
        private GameObject levelPrefabToCopy;

        #endregion

        #region GUI

        protected override bool DrawWizardGUI()
        {
            bool propertiesChanged = base.DrawWizardGUI();
            EditorGUI.BeginChangeCheck();

            destinationFolder = EditorGUILayout.TextField(destinationFolder);
            levelIndex = RobbiEditorGUILayout.UIntField("Level Index", levelIndex);
            numInteractables = RobbiEditorGUILayout.UIntField("Num Interactables", numInteractables);
            levelPrefabToCopy = EditorGUILayout.ObjectField("Level Prefab To Copy", levelPrefabToCopy, typeof(GameObject), false) as GameObject;

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
            CreateFSM();
            CreatePrefab();
            CreateLevelData();  // Must happen last

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
            
            Transform interactables = createdPrefab.transform.Find("Interactables");
            DeleteAllChildren(interactables);

            for (uint i = 0; i < numInteractables; ++i)
            {
                GameObject interactableMarker = PrefabUtility.InstantiatePrefab(interactableMarkerPrefab, interactables) as GameObject;
                interactableMarker.name = string.Format("Interactable{0}", i);
            }

            FSMRuntime runtime = createdPrefab.GetComponent<FSMRuntime>();
            if (runtime == null)
            {
                runtime = createdPrefab.AddComponent<FSMRuntime>();
            }
            runtime.graph = AssetDatabase.LoadAssetAtPath<FSMGraph>(Path.Combine(levelFolderFullPath, string.Format("Level{0}FSM.asset", levelIndex)));

            createdPrefab.SetAddressableGroup(AddressablesConstants.LEVELS_GROUP);
            EditorUtility.SetDirty(createdPrefab);
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
