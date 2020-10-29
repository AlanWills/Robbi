using Robbi.Debugging.Logging;
using Robbi.Levels;
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

        private AsyncOperationHandle<LevelManager> loadLevelManager;

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            HudLogger.LogInfo("Beginning bootstrap");
            Debug.Log("Beginning bootstrap");
            loadLevelManager = LevelManager.Load();
        }

        protected override FSMNode OnUpdate()
        {
            return (loadLevelManager.IsValid() &&
                   (loadLevelManager.IsDone || loadLevelManager.Status != AsyncOperationStatus.None)) ? base.OnUpdate() : this;
        }

        protected override void OnExit()
        {
            base.OnExit();

            if (loadLevelManager.IsValid() && loadLevelManager.Status == AsyncOperationStatus.Failed)
            {
                HudLogger.LogError("Failed to load level manager");
                Debug.LogError("Failed to load level manager");
            }
        }

        #endregion
    }
}
