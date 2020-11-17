using Robbi.FSM;
using Robbi.FSM.Nodes;
using Robbi.FSM.Nodes.Events;
using Robbi.FSM.Nodes.Events.Conditions;
using Robbi.FSM.Nodes.Testing;
using Robbi.Levels.Elements;
using RobbiEditor.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using XNode;
using Event = Robbi.Events.Event;

namespace RobbiEditor.Migration
{
    public static class MigrateLevelIntegrationTests
    {
        [MenuItem("Robbi/Migration/Migrate Level Integration Tests")]
        public static void MenuItem()
        {
            int i = 0;
            string levelFolderPath = string.Format("Assets/Levels/Level{0}", i);

            while (Directory.Exists(levelFolderPath))
            {
                string fsmPath = string.Format("{0}/{1}/Level{2}IntegrationTestFSM.asset", levelFolderPath, LevelDirectories.TESTS_NAME, i);
                FSMGraph fsmGraph = AssetDatabase.LoadAssetAtPath<FSMGraph>(fsmPath);

                if (fsmGraph != null)
                {
                    FinishIntegrationTestNode testPassed = fsmGraph.nodes.Find(x => x is FinishIntegrationTestNode && (x as FinishIntegrationTestNode).testResult.name == "IntegrationTestPassed") as FinishIntegrationTestNode;
                    FinishIntegrationTestNode testFailed = fsmGraph.nodes.Find(x => x is FinishIntegrationTestNode && (x as FinishIntegrationTestNode).testResult.name == "IntegrationTestFailed") as FinishIntegrationTestNode;
                    
                    MultiEventListenerNode multiEventListenerNode = fsmGraph.nodes.Find(x => x is MultiEventListenerNode) as MultiEventListenerNode;
                    for (uint e = multiEventListenerNode.NumEvents; e > 0; --e)
                    {
                        EventCondition eventCondition = multiEventListenerNode.GetEvent(e - 1);
                        multiEventListenerNode.RemoveEvent(eventCondition);
                    }

                    // Level Lose Waypoint Unreachable
                    {
                        VoidEventCondition levelLoseWaypointUnreachableEvent = multiEventListenerNode.AddEvent<VoidEventCondition>();
                        levelLoseWaypointUnreachableEvent.listenFor = AssetDatabase.LoadAssetAtPath<Event>(EventFiles.LEVEL_LOSE_WAYPOINT_UNREACHABLE_EVENT);
                        NodePort levelLosePort = multiEventListenerNode.AddEventConditionPort(levelLoseWaypointUnreachableEvent.listenFor.name);
                        levelLosePort.Connect(testFailed.GetInputPort(FSMNode.DEFAULT_INPUT_PORT_NAME));
                    }

                    // Level Lose Out Of Waypoints
                    {
                        VoidEventCondition levelLoseOutOfWaypointsEvent = multiEventListenerNode.AddEvent<VoidEventCondition>();
                        levelLoseOutOfWaypointsEvent.listenFor = AssetDatabase.LoadAssetAtPath<Event>(EventFiles.LEVEL_LOSE_OUT_OF_WAYPOINTS_EVENT);
                        NodePort levelLosePort = multiEventListenerNode.AddEventConditionPort(levelLoseOutOfWaypointsEvent.listenFor.name);
                        levelLosePort.Connect(testFailed.GetInputPort(FSMNode.DEFAULT_INPUT_PORT_NAME));
                    }

                    // Level Won
                    {
                        VoidEventCondition levelWonEvent = multiEventListenerNode.AddEvent<VoidEventCondition>();
                        levelWonEvent.listenFor = AssetDatabase.LoadAssetAtPath<Event>(EventFiles.LEVEL_WON_EVENT);
                        NodePort levelWonPort = multiEventListenerNode.AddEventConditionPort(levelWonEvent.listenFor.name);
                        levelWonPort.Connect(testPassed.GetInputPort(FSMNode.DEFAULT_INPUT_PORT_NAME));
                    }

                    Debug.LogFormat("Fixed up {0}", fsmGraph.name);
                }
                
                ++i;
                levelFolderPath = string.Format("Assets/Levels/Level{0}", i);
            }

            AssetDatabase.SaveAssets();
        }
    }
}
