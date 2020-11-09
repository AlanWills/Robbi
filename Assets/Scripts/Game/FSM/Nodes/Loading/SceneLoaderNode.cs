﻿using Robbi.Debugging.Logging;
using Robbi.Objects;
using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using XNode;

namespace Robbi.FSM.Nodes
{
    [Serializable]
    [CreateNodeMenu("Robbi/Loading/Scene Loader")]
    [NodeWidth(250), NodeTint(0.2f, 0.2f, 0.6f)]
    public class SceneLoaderNode : FSMNode
    {
        #region Properties and Fields

        public StringReference sceneName;
        public LoadSceneMode loadMode = LoadSceneMode.Single;
        public bool isAddressable = false;

        private AsyncOperation loadOperation;
        private AsyncOperationHandle<SceneInstance> addressablesOperation;

        #endregion

        #region Add/Remove/Copy

        protected override void OnAddToGraph()
        {
            base.OnAddToGraph();

            if (sceneName == null)
            {
                sceneName = CreateParameter<StringReference>(name + "_sceneName");
            }
        }

        protected override void OnRemoveFromGraph()
        {
            base.OnRemoveFromGraph();

            RemoveParameter(sceneName);
        }

        protected override void OnCopyInGraph(FSMNode original)
        {
            base.OnCopyInGraph(original);

            SceneLoaderNode originalSceneLoader = original as SceneLoaderNode;
            sceneName = CreateParameter(originalSceneLoader.sceneName);
        }

        #endregion

        #region FSM Runtime Methods

        protected override void OnEnter()
        {
            base.OnEnter();

            if (isAddressable)
            {
                addressablesOperation = Addressables.LoadSceneAsync(sceneName.Value, loadMode);
            }
            else
            {
                loadOperation = SceneManager.LoadSceneAsync(sceneName.Value, loadMode);
            }
        }

        protected override FSMNode OnUpdate()
        {
            if (isAddressable)
            {
                return addressablesOperation.IsDone ? base.OnUpdate() : this;
            }
            else
            {
                return loadOperation.isDone ? base.OnUpdate() : this;
            }
        }

        protected override void OnExit()
        {
            base.OnExit();

            if (isAddressable && addressablesOperation.Status == AsyncOperationStatus.Failed)
            {
                HudLogger.LogError(addressablesOperation.OperationException.Message);
            }
        }

        #endregion
    }
}