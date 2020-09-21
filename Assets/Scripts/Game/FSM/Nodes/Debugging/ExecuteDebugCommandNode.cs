using Robbi.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.FSM.Nodes.Debugging
{
    [CreateNodeMenu("Robbi/Debugging/Execute Debug Command")]
    public class ExecuteDebugCommandNode : FSMNode
    {
        #region Properties and Fields

#if UNITY_EDITOR
        private const string DEFAULT_EXECUTE_EVENT = "Assets/Events/Debugging/ExecuteDebugCommand.asset";
#endif

        public StringEvent onExecute;
        public string debugCommand;

        #endregion

        #region Initialization

#if UNITY_EDITOR
        protected override void Init()
        {
            if (onExecute == null)
            {
                onExecute = UnityEditor.AssetDatabase.LoadAssetAtPath<StringEvent>(DEFAULT_EXECUTE_EVENT);
            }
            
            Debug.Assert(onExecute != null, "onExecute event is not set and the default event could not be loaded.");
        }
#endif

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            // TODO
        }

        #endregion
    }
}
