using Celeste.Assets;
using Celeste.FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Celeste.FSMNodes.Assets
{
    [CreateNodeMenu("Celeste/Assets/Initialize Addressables")]
    public class InitializeAddressablesNode : FSMNode
    {
        #region Properties and Fields

        private AsyncOperationHandleWrapper operationHandle;

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

#if !UNITY_EDITOR
            // Fix for bug: https://forum.unity.com/threads/addressables-not-loading-in-build.925982/
            PlayerPrefs.DeleteKey(Addressables.kAddressablesRuntimeDataPath);
#endif

            operationHandle = new AsyncOperationHandleWrapper(Addressables.InitializeAsync());
        }

        protected override FSMNode OnUpdate()
        {
            return operationHandle.IsDone ? base.OnUpdate() : this;
        }

        protected override void OnExit()
        {
            base.OnExit();

            if (operationHandle.HasError)
            {
                Debug.LogErrorFormat("Failed to initialize Addressables: {0}", 
                    operationHandle.handle.OperationException != null ? operationHandle.handle.OperationException.Message : "No exception found");
            }
        }

        #endregion
    }
}
