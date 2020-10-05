﻿using Robbi.Debugging.Logging;
using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Robbi.FSM.Nodes.Assets
{
    [Serializable]
    [CreateNodeMenu("Robbi/Assets/Download Addressables")]
    public class DownloadAddressablesNode : FSMNode
    {
        #region Properties and Fields

        public string label;
        public FloatValue progress;

        private AsyncOperationHandle downloadOperation;
        private bool complete = false;

        #endregion

        #region FSM Runtime Overrides

        protected override void OnEnter()
        {
            base.OnEnter();
            
            downloadOperation = Addressables.DownloadDependenciesAsync(label);
            downloadOperation.Completed += DownloadOperation_Completed;

            complete = false;

            if (progress != null)
            {
                progress.value = 0;
            }
        }

        protected override FSMNode OnUpdate()
        {
            if (progress != null && downloadOperation.IsValid() && !downloadOperation.IsDone)
            {
                progress.value = downloadOperation.PercentComplete;
            }

            if (complete)
            {
                return base.OnUpdate();
            }

            return this;
        }

        protected override void OnExit()
        {
            base.OnExit();

            if (downloadOperation.IsValid())
            {
                if (downloadOperation.Status == AsyncOperationStatus.Failed)
                {
                    HudLogger.LogError(downloadOperation.OperationException.Message);
                    Debug.LogError(downloadOperation.OperationException.Message);
                }
                else if (downloadOperation.Status == AsyncOperationStatus.Succeeded)
                {
                    HudLogger.LogInfo(string.Format("{0} downloaded correctly", label));
                }
            }
            else
            {
                HudLogger.LogError(string.Format("Failed to download {0}", label));
                Debug.LogErrorFormat("Failed to download {0}", label);
            }
        }

        #endregion

        #region Callbacks

        private void DownloadOperation_Completed(AsyncOperationHandle obj)
        {
            complete = true;

            if (progress != null)
            {
                progress.value = 100;
            }
        }

        #endregion
    }
}
