using Robbi.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Robbi.FSM.Nodes.Game
{
    [CreateNodeMenu("Robbi/Game/Bootstrap")]
    [NodeTint(0.2f, 0.2f, 0.6f)]
    public class BootstrapNode : FSMNode
    {
        #region Properties and Fields

        private AsyncOperationHandle<LevelManager> loadLevelManager;

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            loadLevelManager = LevelManager.Load();
        }

        protected override FSMNode OnUpdate()
        {
            return loadLevelManager.IsDone ? base.OnUpdate() : this;
        }

        #endregion
    }
}
