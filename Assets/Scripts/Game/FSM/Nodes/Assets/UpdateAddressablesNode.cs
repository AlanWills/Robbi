using Robbi.Debugging.Logging;
using Robbi.Parameters;
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
        
        public FloatValue progress;

        private AsyncOperationHandle<List<string>> checkForCatalogueUpdatesOperation;
        private AsyncOperationHandle<List<IResourceLocator>> updateCataloguesOperation;
        private bool complete = false;

        #endregion

        #region FSM Runtime Overrides

        protected override void OnEnter()
        {
            base.OnEnter();

            checkForCatalogueUpdatesOperation = Addressables.CheckForCatalogUpdates();
            checkForCatalogueUpdatesOperation.Completed += CheckForCatalogueUpdatesOperation_Completed;

            complete = false;

            if (progress != null)
            {
                progress.value = 0;
            }
        }

        protected override FSMNode OnUpdate()
        {
            if (progress != null)
            {
                if (checkForCatalogueUpdatesOperation.IsValid() && !checkForCatalogueUpdatesOperation.IsDone)
                {
                    progress.value = checkForCatalogueUpdatesOperation.PercentComplete;
                }
                else if (updateCataloguesOperation.IsValid() && !updateCataloguesOperation.IsDone)
                {
                    progress.value = updateCataloguesOperation.PercentComplete;
                }
            }

            if (complete)
            {
                return base.OnUpdate();
            }

            return this;
        }

        #endregion

        #region Callbacks

        private void CheckForCatalogueUpdatesOperation_Completed(AsyncOperationHandle<List<string>> obj)
        {
            complete = obj.Result.Count == 0;

            if (progress != null)
            {
                progress.value = complete ? 100 : 0;
            }

            if (!complete)
            {
                Debug.LogFormat("Downloading {0} updates", obj.Result.Count);

                updateCataloguesOperation = Addressables.UpdateCatalogs(obj.Result);
                updateCataloguesOperation.Completed += UpdateCataloguesOperation_Completed;
            }
        }

        private void UpdateCataloguesOperation_Completed(AsyncOperationHandle<List<IResourceLocator>> obj)
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
