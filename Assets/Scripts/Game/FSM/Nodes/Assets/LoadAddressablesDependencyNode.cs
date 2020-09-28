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
    [CreateNodeMenu("Robbi/Assets/Load Addressables Dependency")]
    public class LoadAddressablesDependencyNode : FSMNode
    {
        #region Properties and Fields

        public string label;

        private AsyncOperationHandle downloadOperation;

        #endregion

        #region FSM Runtime Overrides

        protected override void OnEnter()
        {
            base.OnEnter();

            downloadOperation = Addressables.DownloadDependenciesAsync(label);
        }

        protected override FSMNode OnUpdate()
        {
            if (downloadOperation.IsDone)
            {
                return base.OnUpdate();
            }

            return this;
        }

        protected override void OnExit()
        {
            base.OnExit();

            if (downloadOperation.Status == AsyncOperationStatus.Failed)
            {
                HudLogger.LogError(downloadOperation.OperationException.Message);
                Debug.LogError(downloadOperation.OperationException.Message);
            }
            else if (downloadOperation.Status == AsyncOperationStatus.Succeeded)
            {
                HudLogger.LogInfo("Dependencies downloaded correctly");
            }
        }

        #endregion
    }
}
