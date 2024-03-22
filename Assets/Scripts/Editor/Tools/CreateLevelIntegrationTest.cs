﻿using Celeste.Events;
using Robbi.Levels;
using Robbi.Movement;
using RobbiEditor.Constants;
using CelesteEditor.Tools;
using UnityEditor;
using UnityEngine;
using XNode;
using static Robbi.Movement.MovementManager;
using static RobbiEditor.LevelDirectories;
using Event = Celeste.Events.Event;
using Celeste.FSM;
using Celeste.FSM.Nodes;
using Celeste.FSM.Nodes.Input;
using Celeste.FSM.Nodes.Events;
using Celeste.FSM.Nodes.Events.Conditions;
using Celeste.FSM.Nodes.Testing;
using CelesteEditor.FSM;

namespace RobbiEditor.Tools
{
    public static class CreateLevelIntegrationTest
    {
        #region Menu Item

        [MenuItem("Robbi/Tools/Create Level Integration Test")]
        public static void MenuItem()
        {
            LevelRecord levelRecord = AssetUtility.FindAsset<LevelRecord>();
            FSMGraph integrationTest = ScriptableObject.CreateInstance<FSMGraph>();
            integrationTest.name = $"Level{levelRecord.CurrentLevel}IntegrationTest";

            string levelsFolderPath = $"{LEVELS_PATH}Level{levelRecord.CurrentLevel}";
            AssetDatabase.CreateFolder(levelsFolderPath, "Tests");
            string integrationTestPath = $"{levelsFolderPath}/{TESTS_NAME}{integrationTest.name}.asset";
            AssetDatabase.CreateAsset(integrationTest, integrationTestPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            FSMNode previousNode;
            integrationTest = AssetDatabase.LoadAssetAtPath<FSMGraph>(integrationTestPath);
            integrationTest.SetAddressableInfo(AddressablesConstants.TESTS_GROUP);

            FSMGraphEditor fsmGraphEditor = new FSMGraphEditor();
            fsmGraphEditor.target = integrationTest;

            // Add Wait node
            WaitNode waitNode = CreateNode<WaitNode>(fsmGraphEditor, new Vector2());
            waitNode.time.Value = 1;
            previousNode = waitNode;

            // Add Input node for each waypoint
            MovementManager movementManager = GameObject.Find("MovementManager").GetComponent<MovementManager>();
            GameObjectClickEvent gameObjectLeftClickEvent = AssetDatabase.LoadAssetAtPath<GameObjectClickEvent>(EventFiles.GAME_OBJECT_LEFT_CLICK_EVENT);
            Debug.Assert(gameObjectLeftClickEvent != null, "Default GameObjectLeftClickEvent could not be found for InputRaiserNode");

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
            Debug.Assert(runProgramNode.toRaise != null, "Default RunProgram event could not be found for EventRaiserNode");

            ConnectNodes(previousNode, runProgramNode);
            previousNode = runProgramNode;

            // Add Multi Event Listener to check for level won/lost
            MultiEventListenerNode multiEventListenerNode = CreateNode<MultiEventListenerNode>(fsmGraphEditor, previousNode);

            // Level Lose
            StringEventCondition levelLostEvent = multiEventListenerNode.AddEvent<StringEventCondition>();
            levelLostEvent.listenFor = AssetDatabase.LoadAssetAtPath<StringEvent>(EventFiles.LEVEL_LOST_EVENT);
            Debug.Assert(levelLostEvent.listenFor != null, "Default LevelLost event could not be found for MultiEventListenerNode");
            NodePort levelLostOutputPort = multiEventListenerNode.AddEventConditionPort(levelLostEvent.listenFor.name);
            
            VoidEventCondition levelWonEvent = multiEventListenerNode.AddEvent<VoidEventCondition>();
            levelWonEvent.listenFor = AssetDatabase.LoadAssetAtPath<Event>(EventFiles.LEVEL_WON_EVENT);
            Debug.Assert(levelWonEvent.listenFor != null, "Default LevelWon event could not be found for MultiEventListenerNode");
            NodePort levelWonOutputPort = multiEventListenerNode.AddEventConditionPort(levelWonEvent.listenFor.name);
            
            ConnectNodes(previousNode, multiEventListenerNode);
            previousNode = multiEventListenerNode;

            // Add Integration Test Fail node
            FinishIntegrationTestNode failTestNode = CreateNode<FinishIntegrationTestNode>(fsmGraphEditor, previousNode.position + new Vector2(300, 100));
            failTestNode.testResult = AssetDatabase.LoadAssetAtPath<StringEvent>(EventFiles.INTEGRATION_TEST_FAILED_EVENT);
            failTestNode.name = "Fail Test";
            Debug.Assert(failTestNode.testResult != null, "Default TestFailed event could not be found for FinishIntegrationTestNode");

            levelLostOutputPort.Connect(failTestNode.GetInputPort(FSMNode.DEFAULT_INPUT_PORT_NAME));

            // Add Integration Test Passed node
            FinishIntegrationTestNode passTestNode = CreateNode<FinishIntegrationTestNode>(fsmGraphEditor, previousNode.position + new Vector2(300, -100));
            passTestNode.testResult = AssetDatabase.LoadAssetAtPath<StringEvent>(EventFiles.INTEGRATION_TEST_PASSED_EVENT);
            passTestNode.name = "Pass Test";
            Debug.Assert(passTestNode.testResult != null, "Default TestPassed event could not be found for FinishIntegrationTestNode");

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
