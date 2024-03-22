using Robbi.Levels;
using System.Collections.Generic;
using Celeste.Log;
using Celeste.FSM;
using Celeste.Tips;
using Robbi.Shop;
using Celeste.Assets;

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
                foreach (AsyncOperationHandleWrapper managerHandle in loadManagers)
                {
                    if (!managerHandle.IsDone)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        private List<AsyncOperationHandleWrapper> loadManagers = new List<AsyncOperationHandleWrapper>();

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            HudLog.LogInfo("Beginning bootstrap");

            loadManagers.Clear();
            //loadManagers.Add(LevelManager.LoadAsync());
            //loadManagers.Add(OptionsManager.LoadAsync());
            //loadManagers.Add(BoostersManager.LoadAsync());
            //loadManagers.Add(TipsManager.LoadAsync());
            //loadManagers.Add(CurrencyManager.LoadAsync());
            //loadManagers.Add(ShopItemManager.LoadAsync());
        }

        protected override FSMNode OnUpdate()
        {
            return IsLoadingCompleted ? base.OnUpdate() : this;
        }

        protected override void OnExit()
        {
            base.OnExit();

            foreach (AsyncOperationHandleWrapper managerHandle in loadManagers)
            {
                if (managerHandle.HasError)
                {
                    HudLog.LogError("Failed to load manager");
                }
            }
        }

        #endregion
    }
}