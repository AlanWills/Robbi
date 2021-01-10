using Celeste.Events;
using Celeste.FSM;
using Celeste.FSM.Nodes.Events;
using Celeste.FSM.Nodes.Events.Conditions;
using Celeste.Tilemaps;
using Robbi.Levels;
using RobbiEditor.Iterators;
using RobbiEditor.Levels;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using XNode;

namespace RobbiEditor.Migration
{
    public static class MigrateIntegrationTests
    {
        [MenuItem("Robbi/Migration/Migrate Integration Tests")]
        public static void MenuItem()
        {
            foreach (LevelFolder levelFolder in new LevelFolders())
            {
                FSMGraph fsmGraph = AssetDatabase.LoadAssetAtPath<FSMGraph>(levelFolder.TestFSMPath);



                foreach (FSMNode fsmNode in fsmGraph.nodes)
                {
                    if (fsmNode is MultiEventListenerNode)
                    {
                        MultiEventListenerNode eventListenerNode = fsmNode as MultiEventListenerNode;
                        bool needReplacement = false;
                        NodePort inputPort = null;

                        for (uint i = eventListenerNode.NumEvents; i > 0; --i)
                        {
                            EventCondition eventCondition = eventListenerNode.GetEvent(i - 1);
                            
                            if (eventCondition is VoidEventCondition)
                            {
                                VoidEventCondition voidEventCondition = eventCondition as VoidEventCondition;
                                voidEventCondition.listenFor.name.StartsWith("LevelLose");
                                inputPort = eventListenerNode.GetOutputPort(voidEventCondition.name).GetConnection(0);
                                eventListenerNode.RemoveEvent(voidEventCondition);

                                needReplacement = true;
                            }
                        }

                        if (needReplacement)
                        {
                            StringEventCondition levelLose = eventListenerNode.AddEvent<StringEventCondition>();
                            levelLose.listenFor = AssetDatabase.LoadAssetAtPath<StringEvent>(EventFiles.LEVEL_LOST_EVENT);
                            NodePort outputPort = eventListenerNode.AddEventConditionPort(levelLose.listenFor.name);
                            outputPort.Connect(inputPort);

                            EditorUtility.SetDirty(levelLose);
                            EditorUtility.SetDirty(eventListenerNode);
                        }
                    }
                }

                EditorUtility.SetDirty(fsmGraph);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
