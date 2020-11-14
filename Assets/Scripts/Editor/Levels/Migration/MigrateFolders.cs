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

namespace RobbiEditor.Levels.Migration
{
    public static class MigrateFolders
    {
        [MenuItem("Robbi/Migration/Migrate Folders")]
        public static void MigrateHorizontalDoors()
        {
            int i = 0;
            string levelPath = string.Format("Assets/Levels/Level{0}/Level{0}.prefab", i);
            GameObject levelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(levelPath);

            while (levelPrefab != null)
            {
                string levelFolderPath = string.Format("Assets/Levels/Level{0}", i);

                // Tutorials
                {
                    string tutorialsFolderPath = string.Format("{0}/Tutorials", levelFolderPath);
                    if (!Directory.Exists(tutorialsFolderPath))
                    {
                        AssetDatabase.CreateFolder(levelFolderPath, "Tutorials");

                        string tutorialsOldPath = string.Format("Assets/Levels/Level{0}/Level{0}Tutorials.prefab", i);
                        string tutorialsNewPath = string.Format("{0}/Level{1}Tutorials.prefab", tutorialsFolderPath, i);
                        bool canMove = string.IsNullOrEmpty(AssetDatabase.ValidateMoveAsset(tutorialsOldPath, tutorialsNewPath));

                        if (canMove)
                        {
                            {
                                string result = AssetDatabase.MoveAsset(tutorialsOldPath, tutorialsNewPath);
                                Debug.Assert(string.IsNullOrEmpty(result), result);
                                AddressablesUtility.SetAddressableAddress(tutorialsNewPath, tutorialsNewPath);
                            }

                            {
                                string tutorialsFSMOldPath = string.Format("Assets/Levels/Level{0}/Level{0}TutorialsFSM.asset", i);
                                string tutorialsFSMNewPath = string.Format("{0}/Level{1}TutorialsFSM.asset", tutorialsFolderPath, i);
                                string result = AssetDatabase.MoveAsset(tutorialsFSMOldPath, tutorialsFSMNewPath);
                                Debug.Assert(string.IsNullOrEmpty(result), result);
                                AddressablesUtility.SetAddressableAddress(tutorialsFSMOldPath, tutorialsFSMNewPath);
                            }

                            {
                                string tutorialsDGOldPath = string.Format("Assets/Levels/Level{0}/Level{0}TutorialsDataGraph.asset", i);
                                string tutorialsDGNewPath = string.Format("{0}/Level{1}TutorialsDG.asset", tutorialsFolderPath, i);
                                string result = AssetDatabase.MoveAsset(tutorialsDGOldPath, tutorialsDGNewPath);
                                Debug.Assert(string.IsNullOrEmpty(result), result);
                                AddressablesUtility.SetAddressableAddress(tutorialsDGOldPath, tutorialsDGNewPath);
                            }
                        }
                        else
                        {
                            FileUtil.DeleteFileOrDirectory(tutorialsFolderPath);
                        }
                    }
                }

                // Doors
                {
                    string doorsFolderPath = string.Format("{0}/Doors", levelFolderPath);
                    if (!Directory.Exists(doorsFolderPath))
                    {
                        string[] doors = AssetDatabase.FindAssets("t:Door", new string[] { levelFolderPath });
                        if (doors.Length > 0)
                        {
                            AssetDatabase.CreateFolder(levelFolderPath, "Doors");

                            foreach (string doorPath in doors)
                            {
                                Door door = AssetDatabase.LoadAssetAtPath<Door>(doorPath);
                                string newDoorPath = string.Format("{0}/{1}.asset", doorsFolderPath, door.name);
                                string result = AssetDatabase.MoveAsset(doorPath, newDoorPath);
                                Debug.Assert(string.IsNullOrEmpty(result), result);
                                AddressablesUtility.SetAddressableAddress(newDoorPath, newDoorPath);
                            }
                        }
                    }
                }

                // Tests
                {
                    string testsFolderPath = string.Format("{0}/Tests", levelFolderPath);
                    if (!Directory.Exists(testsFolderPath))
                    {
                        AssetDatabase.CreateFolder(levelFolderPath, "Tests");

                        string testsOldPath = string.Format("Assets/Levels/Level{0}/Level{0}IntegrationTestFSM.asset", i);
                        string testsNewPath = string.Format("{0}/Level{1}IntegrationTestFSM.asset", testsFolderPath, i);
                        bool canMove = string.IsNullOrEmpty(AssetDatabase.ValidateMoveAsset(testsOldPath, testsNewPath));

                        if (canMove)
                        {
                            string result = AssetDatabase.MoveAsset(testsOldPath, testsNewPath);
                            Debug.Assert(string.IsNullOrEmpty(result), result);
                            AddressablesUtility.SetAddressableAddress(testsNewPath, testsNewPath);
                        }
                        else
                        {
                            FileUtil.DeleteFileOrDirectory(testsFolderPath);
                        }
                    }
                }

                ++i;
                levelPath = string.Format("Assets/Levels/Level{0}/Level{0}.prefab", i);
                levelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(levelPath);
            }

            AssetDatabase.SaveAssets();
        }
    }
}
