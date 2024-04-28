using CelesteEditor.Tools;
using Robbi.Levels.Elements;
using RobbiEditor.Constants;
using RobbiEditor.Iterators;
using RobbiEditor.Levels;
using System.IO;
using UnityEditor;
using UnityEngine;

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
                    AddressablesExtensions.SetAddressableInfo(levelFolder.LevelDataPath, AddressablesConstants.LEVELS_GROUP);
                }

                // Level Prefab
                {
                    AddressablesExtensions.SetAddressableInfo(levelFolder.PrefabPath, AddressablesConstants.LEVELS_GROUP);
                }

                // Tutorials
                {
                    if (Directory.Exists(levelFolder.TutorialsFolderPath))
                    {
                        AddressablesExtensions.SetAddressableInfo(levelFolder.TutorialsPrefabPath, AddressablesConstants.LEVELS_GROUP);
                        AddressablesExtensions.SetAddressableInfo(levelFolder.TutorialsFSMPath, AddressablesConstants.LEVELS_GROUP);
                        AddressablesExtensions.SetAddressableInfo(levelFolder.TutorialsDGPath, AddressablesConstants.LEVELS_GROUP);
                    }
                }

                // Tests
                {
                    if (Directory.Exists(levelFolder.TestsFolderPath))
                    {
                        AddressablesExtensions.SetAddressableInfo(levelFolder.TestFSMPath, AddressablesConstants.TESTS_GROUP);
                    }
                }

                // ILevelElements
                foreach (string scriptableObjectPath in new FindAssets<ScriptableObject>(levelFolder.Path))
                {
                    ScriptableObject scriptableObject = AssetDatabase.LoadAssetAtPath<ScriptableObject>(scriptableObjectPath);
                    if (scriptableObject is ILevelElement)
                    {
                        AddressablesExtensions.SetAddressableInfo(scriptableObject, AddressablesConstants.LEVELS_GROUP);
                    }
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
