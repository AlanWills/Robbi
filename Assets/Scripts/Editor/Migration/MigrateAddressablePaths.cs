using Robbi.Levels;
using Robbi.Levels.Elements;
using Robbi.Parameters;
using RobbiEditor.Constants;
using RobbiEditor.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using static RobbiEditor.LevelDirectories;

namespace RobbiEditor.Migration
{
    public static class MigrateAddressablePaths
    {
        [MenuItem("Robbi/Migration/Migrate Addressable Paths")]
        public static void MenuItem()
        {
            int i = 0;
            string levelFolderPath = string.Format("{0}Level{1}", LEVELS_PATH, i);

            while (Directory.Exists(levelFolderPath))
            {
                // Tutorials
                {
                    string tutorialsFolderPath = string.Format("{0}/{1}", levelFolderPath, TUTORIALS_NAME);
                    if (Directory.Exists(tutorialsFolderPath))
                    {
                        {
                            string tutorialsPath = string.Format("{0}Level{1}Tutorials.prefab", tutorialsFolderPath, i);
                            AddressablesUtility.SetAddressableAddress(tutorialsPath, tutorialsPath);
                            AddressablesUtility.SetAddressableGroup(tutorialsPath, AddressablesConstants.LEVELS_GROUP);
                        }

                        {
                            string tutorialsFSMPath = string.Format("{0}Level{1}TutorialsFSM.asset", tutorialsFolderPath, i);
                            AddressablesUtility.SetAddressableAddress(tutorialsFSMPath, tutorialsFSMPath);
                            AddressablesUtility.SetAddressableGroup(tutorialsFSMPath, AddressablesConstants.LEVELS_GROUP);
                        }

                        {
                            string tutorialsDGPath = string.Format("{0}Level{1}TutorialsDG.asset", tutorialsFolderPath, i);
                            AddressablesUtility.SetAddressableAddress(tutorialsDGPath, tutorialsDGPath);
                            AddressablesUtility.SetAddressableGroup(tutorialsDGPath, AddressablesConstants.LEVELS_GROUP);
                        }
                    }
                }

                // Tests
                {
                    string testsFolderPath = string.Format("{0}/{1}", levelFolderPath, TESTS_NAME);
                    if (Directory.Exists(testsFolderPath))
                    {
                        string testFSMPath = string.Format("{0}Level{1}IntegrationTestFSM.asset", testsFolderPath, i);
                        AddressablesUtility.SetAddressableAddress(testFSMPath, string.Format("Level{0}IntegrationTest", i));
                        AddressablesUtility.SetAddressableGroup(testFSMPath, AddressablesConstants.TESTS_GROUP);
                    }
                }
                
                // Interactables
                {
                    string interactablesFolderPath = string.Format("{0}/{1}", levelFolderPath, INTERACTABLES_NAME);
                    if (Directory.Exists(interactablesFolderPath))
                    {
                        string[] interactables = AssetDatabase.FindAssets("t:Interactable", new string[] { levelFolderPath });
                        if (interactables.Length > 0)
                        {
                            foreach (string interactableGuid in interactables)
                            {
                                string interactablePath = AssetDatabase.GUIDToAssetPath(interactableGuid);
                                AddressablesUtility.SetAddressableAddress(interactablePath, interactablePath);
                                AddressablesUtility.SetAddressableGroup(interactablePath, AddressablesConstants.LEVELS_GROUP);
                            }
                        }
                    }
                }

                // Doors
                {
                    string doorsFolderPath = string.Format("{0}/{1}", levelFolderPath, DOORS_NAME);
                    if (Directory.Exists(doorsFolderPath))
                    {
                        string[] doors = AssetDatabase.FindAssets("t:Door", new string[] { levelFolderPath });
                        if (doors.Length > 0)
                        {
                            foreach (string doorGuid in doors)
                            {
                                string doorPath = AssetDatabase.GUIDToAssetPath(doorGuid);
                                AddressablesUtility.SetAddressableAddress(doorPath, doorPath);
                                AddressablesUtility.SetAddressableGroup(doorPath, AddressablesConstants.LEVELS_GROUP);
                            }
                        }
                    }
                }

                ++i;
                levelFolderPath = string.Format("{0}Level{1}", LEVELS_PATH, i);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
