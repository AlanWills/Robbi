using Robbi.Debugging.Logging;
using Robbi.Levels;
using Robbi.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

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

            HudLogger.LogInfo("Beginning bootstrap");
            Debug.Log("Beginning bootstrap");

            loadManagers.Clear();
            loadManagers.Add(LevelManager.Load());
            loadManagers.Add(OptionsManager.Load());
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
                HudLogger.LogError("Failed to load manager");
                Debug.LogError("Failed to load manager");
            }
        }

        #endregion
    }
}