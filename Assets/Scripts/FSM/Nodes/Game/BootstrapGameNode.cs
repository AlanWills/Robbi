using Robbi.Levels;
using Robbi.Options;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using Celeste.Log;
using Celeste.FSM;
using Robbi.Boosters;
using Celeste.Tips;
using Robbi.Currency;

namespace Robbi.FSM.Nodes.Game
{
    [CreateNodeMenu("Robbi/Game/Bootstrap Game")]
    [NodeTint(0.2f, 0.2f, 0.6f)]
    public class BootstrapGameNode : FSMNode
    {
        #region Properties and Fields

        private bool IsLoadingCompleted
        {
            get
            {
                foreach (AsyncOperationHandle managerHandle in loadManagers)
                {
                    if (!IsLoaded(managerHandle))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        private List<AsyncOperationHandle> loadManagers = new List<AsyncOperationHandle>();

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            HudLog.LogInfo("Beginning bootstrap");

            loadManagers.Clear();
            loadManagers.Add(LevelManager.LoadAsync());
            loadManagers.Add(OptionsManager.LoadAsync());
            loadManagers.Add(BoostersManager.LoadAsync());
            loadManagers.Add(TipsManager.LoadAsync());
            loadManagers.Add(CurrencyManager.LoadAsync());
        }

        protected override FSMNode OnUpdate()
        {
            return IsLoadingCompleted ? base.OnUpdate() : this;
        }

        protected override void OnExit()
        {
            base.OnExit();

            foreach (AsyncOperationHandle managerHandle in loadManagers)
            {
                CheckError(managerHandle);
            }
        }

        #endregion

        #region Utility Methods

        private bool IsLoaded(AsyncOperationHandle loadingHandle)
        {
            return loadingHandle.IsValid() &&
                   (loadingHandle.IsDone || loadingHandle.Status != AsyncOperationStatus.None);
        }

        private void CheckError(AsyncOperationHandle loadingHandle)
        {
            if (loadingHandle.IsValid() && loadingHandle.Status == AsyncOperationStatus.Failed)
            {
                HudLog.LogError("Failed to load manager");
            }
        }

        #endregion
    }
}