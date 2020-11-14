using Robbi.Levels;
using Robbi.Levels.Elements;
using Robbi.Parameters;
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

namespace RobbiEditor.Levels.Migration
{
    public static class MigrateFolders
    {
        [MenuItem("Robbi/Migration/Migrate Folders")]
        public static void MigrateHorizontalDoors()
        {
            int i = 0;
            string levelFolderPath = string.Format("{0}Level{1}/", LEVELS_PATH, i);

            while (Directory.Exists(levelFolderPath))
            {
                // Tutorials
                {
                    string tutorialsFolderPath = string.Format("{0}{1}/", levelFolderPath, TUTORIALS_NAME);
                    if (!Directory.Exists(tutorialsFolderPath))
                    {
                        AssetDatabase.CreateFolder(levelFolderPath, TUTORIALS_NAME);

                        string tutorialsOldPath = string.Format("{0}Level{1}/Level{1}Tutorials.prefab", LEVELS_PATH, i);
                        string tutorialsNewPath = string.Format("{0}Level{1}Tutorials.prefab", tutorialsFolderPath, i);
                        bool canMove = string.IsNullOrEmpty(AssetDatabase.ValidateMoveAsset(tutorialsOldPath, tutorialsNewPath));

                        if (canMove)
                        {
                            {
                                string result = AssetDatabase.MoveAsset(tutorialsOldPath, tutorialsNewPath);
                                Debug.Assert(string.IsNullOrEmpty(result), result);
                                AddressablesUtility.SetAddressableAddress(tutorialsNewPath, tutorialsNewPath);
                            }

                            {
                                string tutorialsFSMOldPath = string.Format("{0}Level{1}/Level{1}TutorialsFSM.asset", LEVELS_PATH, i);
                                string tutorialsFSMNewPath = string.Format("{0}Level{1}TutorialsFSM.asset", tutorialsFolderPath, i);
                                string result = AssetDatabase.MoveAsset(tutorialsFSMOldPath, tutorialsFSMNewPath);
                                Debug.Assert(string.IsNullOrEmpty(result), result);
                                AddressablesUtility.SetAddressableAddress(tutorialsFSMNewPath, tutorialsFSMNewPath);
                            }

                            {
                                string tutorialsDGOldPath = string.Format("{0}Level{1}/Level{1}TutorialsDataGraph.asset", LEVELS_PATH, i);
                                string tutorialsDGNewPath = string.Format("{0}Level{1}TutorialsDG.asset", tutorialsFolderPath, i);
                                string result = AssetDatabase.MoveAsset(tutorialsDGOldPath, tutorialsDGNewPath);
                                Debug.Assert(string.IsNullOrEmpty(result), result);
                                AddressablesUtility.SetAddressableAddress(tutorialsDGNewPath, tutorialsDGNewPath);
                            }
                        }
                        else
                        {
                            AssetDatabase.DeleteAsset(tutorialsFolderPath);
                        }
                    }
                }

                // Doors
                {
                    string doorsFolderPath = string.Format("{0}{1}/", levelFolderPath, DOORS_NAME);
                    if (!Directory.Exists(doorsFolderPath))
                    {
                        string[] doors = AssetDatabase.FindAssets("t:Door", new string[] { levelFolderPath });
                        if (doors.Length > 0)
                        {
                            AssetDatabase.CreateFolder(levelFolderPath, DOORS_NAME);

                            foreach (string doorGuid in doors)
                            {
                                string doorPath = AssetDatabase.GUIDToAssetPath(doorGuid);
                                Door door = AssetDatabase.LoadAssetAtPath<Door>(doorPath);

                                string newDoorPath = string.Format("{0}{1}.asset", doorsFolderPath, door.name);
                                string result = AssetDatabase.MoveAsset(doorPath, newDoorPath);
                                Debug.Assert(string.IsNullOrEmpty(result), result);
                                AddressablesUtility.SetAddressableAddress(newDoorPath, newDoorPath);
                            }
                        }
                    }
                }

                // Tests
                {
                    string testsFolderPath = string.Format("{0}{1}/", levelFolderPath, TESTS_NAME);
                    if (!Directory.Exists(testsFolderPath))
                    {
                        AssetDatabase.CreateFolder(levelFolderPath, TESTS_NAME);

                        string testsOldPath = string.Format("{0}Level{1}/Level{1}IntegrationTestFSM.asset", LEVELS_PATH, i);
                        string testsNewPath = string.Format("{0}Level{1}IntegrationTestFSM.asset", testsFolderPath, i);
                        bool canMove = string.IsNullOrEmpty(AssetDatabase.ValidateMoveAsset(testsOldPath, testsNewPath));

                        if (canMove)
                        {
                            string result = AssetDatabase.MoveAsset(testsOldPath, testsNewPath);
                            Debug.Assert(string.IsNullOrEmpty(result), result);
                        }
                        else
                        {
                            AssetDatabase.DeleteAsset(testsFolderPath);
                        }
                    }
                }

                ++i;
                levelFolderPath = string.Format("{0}Level{1}/", LEVELS_PATH, i);
            }

            AssetDatabase.SaveAssets();
        }
    }
}
