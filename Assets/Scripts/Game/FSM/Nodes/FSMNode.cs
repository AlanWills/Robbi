using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using XNode;

namespace Robbi.FSM.Nodes
{
    [Serializable]
    public abstract class FSMNode : Node
    {
        #region Properties and Fields

        public const string DEFAULT_INPUT_PORT_NAME = " ";
        public const string DEFAULT_OUTPUT_PORT_NAME = "";

        #endregion

        public FSMNode()
        {
            AddDefaultInputPort();
            AddDefaultOutputPort();
        }

        #region FSM Runtime Methods

        public void Enter() 
        { 
            OnEnter(); 
        }

        public FSMNode Update()
        {
            return OnUpdate();
        }

        public void Exit() 
        {
            OnExit();
        }

        protected virtual void OnEnter() { }

        protected virtual FSMNode OnUpdate() { return GetConnectedNode(DEFAULT_OUTPUT_PORT_NAME); }

        protected virtual void OnExit() { }

        #endregion

        #region FSM Utility Methods

        protected void AddDefaultInputPort(ConnectionType connectionType = ConnectionType.Multiple)
        {
            AddInputPort(DEFAULT_INPUT_PORT_NAME, connectionType);
        }

        protected void AddDefaultOutputPort(ConnectionType connectionType = ConnectionType.Override)
        {
            AddOutputPort(DEFAULT_OUTPUT_PORT_NAME, connectionType);
        }

        protected void AddInputPort(string portName, ConnectionType connectionType = ConnectionType.Multiple)
        {
            AddDynamicInput(typeof(void), connectionType, TypeConstraint.None, portName);
        }

        protected void AddOutputPort(string portName, ConnectionType connectionType = ConnectionType.Override)
        {
            AddDynamicOutput(typeof(void), connectionType, TypeConstraint.None, portName);
        }

        protected FSMNode GetConnectedNode(string outputPortName)
        {
            NodePort nodePort = GetOutputPort(outputPortName);
            if (nodePort == null || nodePort.ConnectionCount == 0)
            {
                return null;
            }

            NodePort connection = nodePort.GetConnection(0);
            return connection != null ? connection.node as FSMNode : null;
        }

        #endregion

        #region Value Methods

        public T CreateParameter<T>(string name) where T : ScriptableObject
        {
            T parameter = ScriptableObject.CreateInstance<T>();
            parameter.name = name + "_sceneName";

#if UNITY_EDITOR
            if (UnityEditor.AssetDatabase.IsMainAsset(graph))
            {
                UnityEditor.AssetDatabase.AddObjectToAsset(parameter, this);
                UnityEditor.EditorUtility.SetDirty(graph);
            }
#endif

            return parameter;
        }

        #endregion

        #region Context Menu

        [ContextMenu("Set As Start")]
        public void SetAsStart()
        {
            (graph as FSMGraph).startNode = this;

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(graph);
#endif
        }

        #endregion
    }
}
