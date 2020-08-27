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

        protected const string DEFAULT_INPUT_PORT_NAME = " ";
        protected const string DEFAULT_OUTPUT_PORT_NAME = "";

        #endregion

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

        protected virtual FSMNode OnUpdate() { return null; }

        protected virtual void OnExit() { }

        #endregion

        #region FSM Utility Methods

        protected void AddDefaultInputPort(ConnectionType connectionType = ConnectionType.Multiple)
        {
            AddDynamicInput(typeof(void), connectionType, TypeConstraint.None, DEFAULT_INPUT_PORT_NAME);
        }

        protected void AddDefaultOutputPort(ConnectionType connectionType = ConnectionType.Multiple)
        {
            AddDynamicOutput(typeof(void), connectionType, TypeConstraint.None, DEFAULT_OUTPUT_PORT_NAME);
        }

        protected void AddInputPort(string portName, ConnectionType connectionType = ConnectionType.Multiple)
        {
            AddDynamicInput(typeof(void), connectionType, TypeConstraint.None, portName);
        }

        protected void AddOutputPort(string portName, ConnectionType connectionType = ConnectionType.Override)
        {
            AddDynamicInput(typeof(void), connectionType, TypeConstraint.None, portName);
        }

        protected FSMNode GetConnectedNode(string outputPortName)
        {
            NodePort connection = GetOutputPort(outputPortName).GetConnection(0);
            return connection.node as FSMNode;
        }

        #endregion

        #region Context Menu

        [ContextMenu("Set As Start")]
        public void SetAsStart()
        {
            (graph as FSMGraph).startNode = this;
        }

        #endregion
    }
}
