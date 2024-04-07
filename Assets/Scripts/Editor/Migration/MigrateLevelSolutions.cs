using Celeste.FSM;
using Celeste.FSM.Nodes.Input;
using Robbi.Levels;
using RobbiEditor.Iterators;
using RobbiEditor.Levels;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Migration
{
    public static class MigrateLevelSolutions
    {
        [MenuItem("Robbi/Migration/Migrate Level Solutions")]
        public static void MenuItem()
        {
            foreach (LevelFolder levelFolder in new LevelFolders())
            {
                Level level = AssetDatabase.LoadAssetAtPath<Level>(levelFolder.LevelDataPath);
                FSMGraph levelTest = AssetDatabase.LoadAssetAtPath<FSMGraph>(levelFolder.TestFSMPath);
                List<Vector3Int> levelSolution = new List<Vector3Int>();

                foreach (var node in levelTest.nodes)
                {
                    if (node is InputRaiserNode)
                    {
                        levelSolution.Add(new Vector3Int((int)node.position.x, (int)node.position.y, 0));
                    }
                }

                level.SolutionWaypoints = levelSolution;
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
