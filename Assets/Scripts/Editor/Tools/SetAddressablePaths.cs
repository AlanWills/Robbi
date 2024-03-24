using CelesteEditor.Tools;
using Robbi.Levels.Elements;
using RobbiEditor.Constants;
using RobbiEditor.Iterators;
using RobbiEditor.Levels;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using static RobbiEditor.LevelDirectories;

namespace RobbiEditor.Tools
{
    public static class SetAddressablePaths
    {
        [MenuItem("Robbi/Tools/Set Addressable Paths")]
        public static void MenuItem()
        {
            foreach (LevelFolder levelFolder in new LevelFolders())
            {
                uint levelIndex = levelFolder.Index;
                string levelFolderPath = levelFolder.Path;

                // Level Data
                {
                    AddressablesUtility.SetAddressableInfo(levelFolder.LevelDataPath, AddressablesConstants.LEVELS_GROUP);
                }

                // Level Prefab
                {
                    AddressablesUtility.SetAddressableInfo(levelFolder.PrefabPath, AddressablesConstants.LEVELS_GROUP);
                }

                // Tutorials
                {
                    if (Directory.Exists(levelFolder.TutorialsFolderPath))
                    {
                        AddressablesUtility.SetAddressableInfo(levelFolder.TutorialsPrefabPath, AddressablesConstants.LEVELS_GROUP);
                        AddressablesUtility.SetAddressableInfo(levelFolder.TutorialsFSMPath, AddressablesConstants.LEVELS_GROUP);
                        AddressablesUtility.SetAddressableInfo(levelFolder.TutorialsDGPath, AddressablesConstants.LEVELS_GROUP);
                    }
                }

                // Tests
                {
                    if (Directory.Exists(levelFolder.TestsFolderPath))
                    {
                        AddressablesUtility.SetAddressableInfo(levelFolder.TestFSMPath, AddressablesConstants.TESTS_GROUP);
                    }
                }

                // ILevelElements
                foreach (string scriptableObjectPath in new FindAssets<ScriptableObject>(levelFolder.Path))
                {
                    ScriptableObject scriptableObject = AssetDatabase.LoadAssetAtPath<ScriptableObject>(scriptableObjectPath);
                    if (scriptableObject is ILevelElement)
                    {
                        AddressablesUtility.SetAddressableInfo(scriptableObject, AddressablesConstants.LEVELS_GROUP);
                    }
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
