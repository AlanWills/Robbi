using Robbi.FSM;
using Robbi.Levels;
using RobbiEditor.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Tools
{
    public class ChangeLevelIndex : ScriptableWizard
    {
        #region Properties and Fields

        private string OldLevelFolderName
        {
            get { return string.Format("Level{0}", oldLevelIndex); }
        }

        private string NewLevelFolderName
        {
            get { return string.Format("Level{0}", newLevelIndex); }
        }

        private string OldLevelFolderFullPath
        {
            get { return LevelDirectories.LEVELS_PATH + OldLevelFolderName; }
        }

        private string NewLevelFolderFullPath
        {
            get { return LevelDirectories.LEVELS_PATH + NewLevelFolderName; }
        }

        private uint oldLevelIndex = 0;
        private uint newLevelIndex = 0;

        #endregion

        #region GUI

        protected override bool DrawWizardGUI()
        {
            bool propertiesChanged = base.DrawWizardGUI();
            EditorGUI.BeginChangeCheck();

            oldLevelIndex = RobbiEditorGUILayout.UIntField("Old Level Index", oldLevelIndex);
            newLevelIndex = RobbiEditorGUILayout.UIntField("New Level Index", newLevelIndex);

            return propertiesChanged || EditorGUI.EndChangeCheck();
        }

        private void OnWizardCreate()
        {
            if (oldLevelIndex == newLevelIndex)
            {
                EditorUtility.DisplayDialog("Error", "New level index is the same as the old level index", "OK");
                return;
            }

            LogUtils.Clear();

            RenameDirectory();
            RenameFSM();
            RenamePrefab();
            RenameLevelData();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private void OnWizardOtherButton()
        {
            Close();
        }

        #endregion

        #region Creation Methods

        private void RenameDirectory()
        {
            string errorMessage = AssetDatabase.RenameAsset(OldLevelFolderFullPath, NewLevelFolderName);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                errorString = errorMessage;
                Debug.LogError(errorString);
            }
        }

        private void RenameFSM()
        {
            string oldFSMPath = Path.Combine(NewLevelFolderFullPath, string.Format("Level{0}FSM.asset", oldLevelIndex));
            string newFSMPath = Path.Combine(NewLevelFolderFullPath, string.Format("Level{0}FSM.asset", newLevelIndex));
            string errorMessage = AssetDatabase.RenameAsset(oldFSMPath, string.Format("Level{0}FSM.asset", newLevelIndex));

            if (!string.IsNullOrEmpty(errorMessage))
            {
                errorString = errorMessage;
                Debug.LogError(errorString);
            }
        }

        private void RenamePrefab()
        {
            string oldPrefabPath = Path.Combine(NewLevelFolderFullPath, string.Format("Level{0}.prefab", oldLevelIndex));
            string newPrefabPath = Path.Combine(NewLevelFolderFullPath, string.Format("Level{0}.prefab", newLevelIndex));
            string errorMessage = AssetDatabase.RenameAsset(oldPrefabPath, string.Format("Level{0}.prefab", newLevelIndex));

            if (!string.IsNullOrEmpty(errorMessage))
            {
                errorString = errorMessage;
                Debug.LogError(errorString);
            }
        }

        private void RenameLevelData()
        {
            string oldLevelDataPath = Path.Combine(NewLevelFolderFullPath, string.Format("Level{0}Data.asset", oldLevelIndex));
            string newLevelDataPath = Path.Combine(NewLevelFolderFullPath, string.Format("Level{0}Data.asset", newLevelIndex));
            string errorMessage = AssetDatabase.RenameAsset(oldLevelDataPath, string.Format("Level{0}Data.asset", newLevelIndex));

            if (!string.IsNullOrEmpty(errorMessage))
            {
                errorString = errorMessage;
                Debug.LogError(errorString);
            }
        }

        #endregion

        #region Menu Item

        [MenuItem("Robbi/Tools/Change Level Index")]
        public static void ShowChangeLevelIndexWizard()
        {
            ScriptableWizard.DisplayWizard<ChangeLevelIndex>("Change Level Index", "Change", "Close");
        }

        #endregion
    }
}
