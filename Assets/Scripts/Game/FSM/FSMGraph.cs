using Robbi.FSM.Nodes;
using Robbi.Objects;
using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using XNode;

namespace Robbi.FSM
{
    [CreateAssetMenu(fileName = "FSMGraph", menuName = "Robbi/FSM/FSM Graph")]
    public class FSMGraph : NodeGraph, IParameterContainer
    {
        #region Properties and Fields

        public FSMNode startNode;

#if UNITY_EDITOR
        [SerializeField]
        private List<ScriptableObject> parameters;
#endif

        #endregion

        #region Node Utility Methods

        public override Node AddNode(Type type)
        {
            FSMNode node = base.AddNode(type) as FSMNode;
            startNode = startNode == null ? node : startNode;
            node.AddToGraph();

            return node;
        }

        public override void RemoveNode(Node node)
        {
            FSMNode fsmNode = node as FSMNode;
            fsmNode.RemoveFromGraph();

            base.RemoveNode(node);
        }

        public override Node CopyNode(Node original)
        {
            FSMNode copy = base.CopyNode(original) as FSMNode;
            copy.CopyInGraph(original as FSMNode);

            return copy;
        }

        #endregion

        #region Parameter Utility Methods

        public T CreateParameter<T>(string name) where T : ScriptableObject
        {
            T parameter = ScriptableObject.CreateInstance<T>();
            parameter.name = name + "_sceneName";

#if UNITY_EDITOR
            parameters.Add(parameter);
            UnityEditor.AssetDatabase.AddObjectToAsset(parameter, this);
            UnityEditor.EditorUtility.SetDirty(this);
#endif

            return parameter;
        }

        public T CreateParameter<T>(T original) where T : ScriptableObject, ICopyable<T>
        {
            T parameter = CreateParameter<T>(original.name);
            parameter.CopyFrom(original);

            return parameter;
        }

        public void RemoveAsset(ScriptableObject asset)
        {
            if (asset != null)
            {
#if UNITY_EDITOR
                UnityEditor.AssetDatabase.RemoveObjectFromAsset(asset);
                parameters.Remove(asset);
#endif
                ScriptableObject.DestroyImmediate(asset);
            }
        }

        #endregion
    }
}
