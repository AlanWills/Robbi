using Celeste.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Celeste.FSM.Nodes.Objects
{
    public class HandleWrapper
    {
        public AsyncOperationHandle handle;
    }

    [Serializable]
    public class AsyncOperationHandleUnityEvent : UnityEvent<string, HandleWrapper> { }

    [Serializable]
    [CreateNodeMenu("Celeste/Objects/Execute Async Operation")]
    public class ExecuteAsyncOperationNode : FSMNode
    {
        #region Properties and Fields

        public string address;
        public AsyncOperationHandleUnityEvent function;

        private HandleWrapper handleWrapper = new HandleWrapper();

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            function.Invoke(address, handleWrapper);
        }

        protected override FSMNode OnUpdate()
        {
            if (handleWrapper.handle.IsValid() &&
                (handleWrapper.handle.IsDone || handleWrapper.handle.Status != AsyncOperationStatus.None))
            {
                return base.OnUpdate();
            }

            return this;
        }

        protected override void OnExit()
        {
            base.OnExit();

            if (handleWrapper.handle.IsValid() && handleWrapper.handle.Status == AsyncOperationStatus.Failed)
            {
                HudLog.LogError("Async Operation failed");
            }
        }

        #endregion
    }
}
