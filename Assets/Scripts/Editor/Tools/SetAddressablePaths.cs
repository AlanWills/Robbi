using CelesteEditor.Tools;
using Robbi.Levels.Elements;
using RobbiEditor.Constants;
using RobbiEditor.Iterators;
using RobbiEditor.Levels;
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
                        AddressablesUtility.SetAddressableInfo(levelFolder.TestFSMPath, AddressablesConstants.TESTS_GROUP);
                    }
                }
                
                // Interactables
                {
                    foreach (string interactablePath in levelFolder.Interactables)
                    {
                        AddressablesUtility.SetAddressableInfo(interactablePath, AddressablesConstants.LEVELS_GROUP);
                    }
                }

                // Collectables
                {
                    foreach (string collectablePath in levelFolder.Collectables)
                    {
                        AddressablesUtility.SetAddressableInfo(collectablePath, AddressablesConstants.LEVELS_GROUP);
                    }
                }

                // Portals
                {
                    foreach (string portalPath in levelFolder.Portals)
                    {
                        AddressablesUtility.SetAddressableInfo(portalPath, AddressablesConstants.LEVELS_GROUP);
                    }
                }

                // Doors
                {
                    foreach (string doorPath in levelFolder.Doors)
                    {
                        AddressablesUtility.SetAddressableInfo(doorPath, AddressablesConstants.LEVELS_GROUP);
                    }
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
