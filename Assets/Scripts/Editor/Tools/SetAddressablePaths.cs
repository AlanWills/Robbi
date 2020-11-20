﻿using Robbi.Levels.Elements;
using RobbiEditor.Constants;
using RobbiEditor.Iterators;
using RobbiEditor.Levels;
using RobbiEditor.Utils;
using System;
using System.IO;
using UnityEditor;
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
                        AddressablesUtility.SetAddressableInfo(levelFolder.TutorialsPath, AddressablesConstants.LEVELS_GROUP);
                        AddressablesUtility.SetAddressableInfo(levelFolder.TutorialsFSMPath, AddressablesConstants.LEVELS_GROUP);
                        AddressablesUtility.SetAddressableInfo(levelFolder.TutorialsDGPath, AddressablesConstants.LEVELS_GROUP);
                    }
                }

                // Tests
                {
                    if (Directory.Exists(levelFolder.TestsFolderPath))
                    {
                        AddressablesUtility.SetAddressableInfo(levelFolder.TestFSMPath, AddressablesConstants.TESTS_GROUP, string.Format("Level{0}IntegrationTest", levelFolder.Index));
                    }
                }
                
                // Interactables
                {
                    foreach (string interactablePath in levelFolder.Interactables)
                    {
                        AddressablesUtility.SetAddressableInfo(interactablePath, AddressablesConstants.LEVELS_GROUP);
                    }
                }

                // Doors
                {
                    foreach (string doorPath in levelFolder.Doors)
                    {
                        Door door = AssetDatabase.LoadAssetAtPath<Door>(doorPath);
                        AddressablesUtility.SetAddressableInfo(doorPath, AddressablesConstants.LEVELS_GROUP);
                    }
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}