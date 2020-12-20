﻿using Robbi.FSM;
using Robbi.Levels;
using Robbi.Levels.Elements;
using RobbiEditor.Iterators;
using CelesteEditor.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using CelesteEditor;

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

            oldLevelIndex = CelesteEditorGUILayout.UIntField("Old Level Index", oldLevelIndex);
            newLevelIndex = CelesteEditorGUILayout.UIntField("New Level Index", newLevelIndex);

            return propertiesChanged || EditorGUI.EndChangeCheck();
        }

        private void OnWizardCreate()
        {
            if (oldLevelIndex == newLevelIndex)
            {
                EditorUtility.DisplayDialog("Error", "New level index is the same as the old level index", "OK");
                return;
            }

            LogUtility.Clear();

            RenameDirectory();
            RenameFSM();
            RenamePrefab();
            RenameLevelData();
            RenameTest();
            RenameDoors();
            RenameInteractables();

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
            string errorMessage = AssetDatabase.RenameAsset(oldLevelDataPath, string.Format("Level{0}Data.asset", newLevelIndex));

            if (!string.IsNullOrEmpty(errorMessage))
            {
                errorString = errorMessage;
                Debug.LogError(errorString);
            }
        }

        private void RenameTest()
        {
            string oldTestPath = string.Format("{0}/{1}Level{2}IntegrationTest.asset", NewLevelFolderFullPath, LevelDirectories.TESTS_NAME, oldLevelIndex);
            string errorMessage = AssetDatabase.RenameAsset(oldTestPath, string.Format("Level{0}IntegrationTest.asset", newLevelIndex));

            if (!string.IsNullOrEmpty(errorMessage))
            {
                errorString = errorMessage;
                Debug.LogError(errorString);
            }
        }

        private void RenameDoors()
        {
            foreach (string doorPath in new FindAssets<Door>(NewLevelFolderFullPath))
            {
                Door door = AssetDatabase.LoadAssetAtPath<Door>(doorPath);
                string errorMessage = AssetDatabase.RenameAsset(doorPath, string.Format("{0}.asset", door.name.Replace(oldLevelIndex.ToString(), newLevelIndex.ToString())));

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    errorString = errorMessage;
                    Debug.LogError(errorString);
                }
            }
        }

        private void RenameInteractables()
        {
            foreach (string interactablePath in new FindAssets<ScriptableObject>(string.Format("{0}/{1}", NewLevelFolderFullPath, LevelDirectories.INTERACTABLES_NAME)))
            {
                ScriptableObject interactable = AssetDatabase.LoadAssetAtPath<ScriptableObject>(interactablePath);
                string errorMessage = AssetDatabase.RenameAsset(interactablePath, string.Format("{0}.asset", interactable.name.Replace(oldLevelIndex.ToString(), newLevelIndex.ToString())));

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    errorString = errorMessage;
                    Debug.LogError(errorString);
                }
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
