using Robbi.Events;
using Robbi.FSM;
using Robbi.FSM.Nodes;
using Robbi.FSM.Nodes.Events;
using Robbi.FSM.Nodes.Events.Conditions;
using Robbi.FSM.Nodes.Input;
using Robbi.FSM.Nodes.Testing;
using Robbi.Levels;
using Robbi.Movement;
using RobbiEditor.Constants;
using RobbiEditor.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;
using XNode;
using static Robbi.Movement.MovementManager;
using Event = Robbi.Events.Event;

namespace RobbiEditor.Tools
{
    public static class CreateLevelIntegrationTest
    {
        #region Menu Item

        [MenuItem("Robbi/Tools/Create Level Integration Test")]
        public static void MenuItem()
        {
            FSMGraph integrationTest = ScriptableObject.CreateInstance<FSMGraph>();
            LevelManager levelManager = LevelManager.Load();

            string integrationTestPath = string.Format("{0}/Level{1}/Level{1}IntegrationTestFSM.asset", LevelDirectories.FULL_PATH, levelManager.CurrentLevelIndex);
            AssetDatabase.CreateAsset(integrationTest, integrationTestPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            FSMNode previousNode;
            integrationTest = AssetDatabase.LoadAssetAtPath<FSMGraph>(integrationTestPath);
            integrationTest.SetAddressableGroup(AddressablesConstants.TESTS_GROUP);
            integrationTest.SetAddressableAddress(string.Format("Level{0}IntegrationTest", levelManager.CurrentLevelIndex));

            FSMGraphEditor fsmGraphEditor = new FSMGraphEditor();
            fsmGraphEditor.target = integrationTest;

            // Add Wait node
            WaitNode waitNode = CreateNode<WaitNode>(fsmGraphEditor, new Vector2());
            waitNode.time = 1;
            previousNode = waitNode;

            // Add Input node for each waypoint
            MovementManager movementManager = GameObject.Find("MovementManager").GetComponent<MovementManager>();
            GameObjectClickEvent gameObjectLeftClickEvent = AssetDatabase.LoadAssetAtPath<GameObjectClickEvent>(EventFiles.GAME_OBJECT_LEFT_CLICK_EVENT);
            Debug.Assert(gameObjectLeftClickEvent, "Default GameObjectLeftClickEvent could not be found for InputRaiserNode");

            for (int i = 0; i < movementManager.NumWaypoints; ++i)
            {
                Waypoint waypoint = movementManager.GetWaypoint(i);
                InputRaiserNode inputRaiserNode = CreateNode<InputRaiserNode>(fsmGraphEditor, previousNode);
                inputRaiserNode.gameObjectName = "MovementManager";
                inputRaiserNode.inputEvent = gameObjectLeftClickEvent;
                inputRaiserNode.inputPosition = waypoint.gridPosition;

                ConnectNodes(previousNode, inputRaiserNode);
                previousNode = inputRaiserNode;
            }

            // Add Event Raiser to run the program
            EventRaiserNode runProgramNode = CreateNode<EventRaiserNode>(fsmGraphEditor, previousNode);
            runProgramNode.toRaise = AssetDatabase.LoadAssetAtPath<Event>(EventFiles.RUN_PROGRAM_EVENT);
            Debug.Assert(runProgramNode.toRaise, "Default RunProgram event could not be found for EventRaiserNode");

            ConnectNodes(previousNode, runProgramNode);
            previousNode = runProgramNode;

            // Add Multi Event Listener to check for level won/lost
            MultiEventListenerNode multiEventListenerNode = CreateNode<MultiEventListenerNode>(fsmGraphEditor, previousNode);
            VoidEventCondition levelLostEvent = multiEventListenerNode.AddEvent<VoidEventCondition>();
            levelLostEvent.listenFor = AssetDatabase.LoadAssetAtPath<Event>(EventFiles.LEVEL_LOST_EVENT);
            Debug.Assert(levelLostEvent.listenFor, "Default LevelLost event could not be found for MultiEventListenerNode");

            VoidEventCondition levelWonEvent = multiEventListenerNode.AddEvent<VoidEventCondition>();
            levelWonEvent.listenFor = AssetDatabase.LoadAssetAtPath<Event>(EventFiles.LEVEL_WON_EVENT);
            Debug.Assert(levelLostEvent.listenFor, "Default LevelWon event could not be found for MultiEventListenerNode");

            ConnectNodes(previousNode, multiEventListenerNode);
            previousNode = multiEventListenerNode;

            NodePort levelLostOutputPort = multiEventListenerNode.AddEventConditionPort("LevelLost");
            NodePort levelWonOutputPort = multiEventListenerNode.AddEventConditionPort("LevelWon");

            // Add Integration Test Fail node
            FinishIntegrationTestNode failTestNode = CreateNode<FinishIntegrationTestNode>(fsmGraphEditor, previousNode.position + new Vector2(300, 100));
            failTestNode.testResult = AssetDatabase.LoadAssetAtPath<StringEvent>(EventFiles.INTEGRATION_TEST_FAILED_EVENT);
            Debug.Assert(levelLostEvent.listenFor, "Default TestFailed event could not be found for FinishIntegrationTestNode");

            levelLostOutputPort.Connect(failTestNode.GetInputPort(FSMNode.DEFAULT_INPUT_PORT_NAME));

            // Add Integration Test Passed node
            FinishIntegrationTestNode passTestNode = CreateNode<FinishIntegrationTestNode>(fsmGraphEditor, previousNode.position + new Vector2(300, -100));
            passTestNode.testResult = AssetDatabase.LoadAssetAtPath<StringEvent>(EventFiles.INTEGRATION_TEST_PASSED_EVENT);
            Debug.Assert(levelLostEvent.listenFor, "Default TestPassed event could not be found for FinishIntegrationTestNode");

            levelWonOutputPort.Connect(passTestNode.GetInputPort(FSMNode.DEFAULT_INPUT_PORT_NAME));

            EditorUtility.SetDirty(integrationTest);
            AssetDatabase.SaveAssets();
        }

        #endregion

        #region Utility Methods

        private static T CreateNode<T>(FSMGraphEditor fsmGraphEditor, FSMNode previousNode) where T : FSMNode
        {
            return CreateNode<T>(fsmGraphEditor, previousNode.position + new Vector2(300, 0)) as T;
        }

        private static T CreateNode<T>(FSMGraphEditor fsmGraphEditor, Vector2 position) where T : FSMNode
        {
            return fsmGraphEditor.CreateNode(typeof(T), position) as T;
        }

        private static void ConnectNodes(FSMNode source, FSMNode destination)
        {
            source.GetOutputPort(FSMNode.DEFAULT_OUTPUT_PORT_NAME).Connect(destination.GetInputPort(FSMNode.DEFAULT_INPUT_PORT_NAME));
        }

        #endregion
    }
}
