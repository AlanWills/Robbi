using Celeste.FSM;
using Celeste.FSM.Nodes.Events;
using CelesteEditor.Tools;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Migration
{
    public static class MigrateEventRaiserNodes
    {
        [MenuItem("Robbi/Migration/Migrate Event Raiser Nodes")]
        public static void MenuItem()
        {
            foreach (string fsmGraphGuid in AssetDatabase.FindAssets("t:FSMGraph"))
            {
                string fsmPath = AssetDatabase.GUIDToAssetPath(fsmGraphGuid);
                FSMGraph fsmGraph = AssetDatabase.LoadAssetAtPath<FSMGraph>(fsmPath);

                foreach (FSMNode node in fsmGraph.nodes)
                {
                    if (node is BoolEventRaiserNode)
                    {
                        BoolEventRaiserNode boolNode = node as BoolEventRaiserNode;
                        if (boolNode.argument == null)
                        {
                            boolNode.AddToGraph();
                            EditorUtility.SetDirty(boolNode);
                        }
                    }
                    else if (node is StringEventRaiserNode)
                    {
                        StringEventRaiserNode stringNode = node as StringEventRaiserNode;
                        if (stringNode.argument == null)
                        {
                            stringNode.AddToGraph();
                            EditorUtility.SetDirty(stringNode);
                        }
                    }
                    else if (node is FloatEventRaiserNode)
                    {
                        FloatEventRaiserNode floatNode = node as FloatEventRaiserNode;
                        if (floatNode.argument == null)
                        {
                            floatNode.AddToGraph();
                            EditorUtility.SetDirty(floatNode);
                        }
                    }
                    else if (node is Vector3IntEventRaiserNode)
                    {
                        Vector3IntEventRaiserNode vector3IntNode = node as Vector3IntEventRaiserNode;
                        if (vector3IntNode.argument == null)
                        {
                            vector3IntNode.AddToGraph();
                            EditorUtility.SetDirty(vector3IntNode);
                        }
                    }
                }
            }

            AssetDatabase.SaveAssets();
        }
    }
}
