using Robbi.Debugging.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Robbi.FSM.Nodes.Assets
{
    [Serializable]
    [CreateNodeMenu("Robbi/Assets/Update Addressables")]
    public class UpdateAddressablesNode : FSMNode
    {
        #region Properties and Fields

        private AsyncOperationHandle<List<string>> checkForCatalogueUpdatesOperation;
        private AsyncOperationHandle<List<IResourceLocator>> updateCataloguesOperation;
        private bool complete = false;

        #endregion

        #region FSM Runtime Overrides

        protected override void OnEnter()
        {
            base.OnEnter();

            complete = false;

            checkForCatalogueUpdatesOperation = Addressables.CheckForCatalogUpdates();
            checkForCatalogueUpdatesOperation.Completed += CheckForCatalogueUpdatesOperation_Completed;
        }

        protected override FSMNode OnUpdate()
        {
            if (complete)
            {
                return base.OnUpdate();
            }

            return this;
        }

        protected override void OnExit()
        {
            base.OnExit();

            if (updateCataloguesOperation.Status == AsyncOperationStatus.Failed)
            {
                HudLogger.LogError(updateCataloguesOperation.OperationException.Message);
                Debug.LogError(updateCataloguesOperation.OperationException.Message);
            }
            else if (updateCataloguesOperation.Status == AsyncOperationStatus.Succeeded)
            {
                HudLogger.LogInfo("Addressables updated correctly");
            }
        }

        #endregion

        #region Callbacks

        private void CheckForCatalogueUpdatesOperation_Completed(AsyncOperationHandle<List<string>> obj)
        {
            updateCataloguesOperation = Addressables.UpdateCatalogs(obj.Result);
            updateCataloguesOperation.Completed += UpdateCataloguesOperation_Completed;
        }

        private void UpdateCataloguesOperation_Completed(AsyncOperationHandle<List<IResourceLocator>> obj)
        {
            complete = true;
        }

        #endregion
    }
}
