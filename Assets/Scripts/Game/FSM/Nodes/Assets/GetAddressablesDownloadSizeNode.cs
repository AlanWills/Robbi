using Robbi.Debugging.Logging;
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
    [CreateNodeMenu("Robbi/Assets/Get Addressables Download Size")]
    public class GetAddressablesDownloadSizeNode : FSMNode
    {
        #region Properties and Fields

        public string label;

        [Output]
        public long size;

        private AsyncOperationHandle<long> downloadOperation;
        private bool complete = false;

        #endregion

        #region FSM Runtime Overrides

        protected override void OnEnter()
        {
            base.OnEnter();

            downloadOperation = Addressables.GetDownloadSizeAsync(label);
            downloadOperation.Completed += DownloadOperation_Completed; ;

            complete = false;
        }

        protected override FSMNode OnUpdate()
        {
            return complete ? base.OnUpdate() : this;
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
                    HudLogger.LogInfo(string.Format("{0} download size is {1}", label, size));
                    Debug.LogFormat("{0} download size is {1}", label, size);
                }
            }
            else
            {
                HudLogger.LogError(string.Format("Failed to check download size for {0}", label));
                Debug.LogErrorFormat("ailed to check download size for {0}", label);
            }
        }

        #endregion

        #region Callbacks

        private void DownloadOperation_Completed(AsyncOperationHandle<long> obj)
        {
            complete = true;
            size = obj.Result;
        }

        #endregion
    }
}
