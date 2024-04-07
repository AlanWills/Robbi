using Celeste.FSM;
using Celeste.FSM.Nodes.Input;
using Robbi.Levels;
using RobbiEditor.Iterators;
using RobbiEditor.Levels;
using System.Collections.Generic;
using System.Linq;
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
                Debug.Assert(levelTest != null, $"Failed to find test fsm for level {levelFolder.Index} at {levelFolder.TestFSMPath}.");
                HashSet<Vector3Int> levelSolution = new HashSet<Vector3Int>();

                foreach (var node in levelTest.nodes)
                {
                    if (node is InputRaiserNode inputRaiserNode && string.CompareOrdinal(inputRaiserNode.gameObjectName, "MovementManager") == 0)
                    {
                        levelSolution.Add(new Vector3Int((int)inputRaiserNode.inputPosition.x, (int)inputRaiserNode.inputPosition.y, 0));
                    }
                }

                level.SolutionWaypoints = levelSolution.ToList();
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
