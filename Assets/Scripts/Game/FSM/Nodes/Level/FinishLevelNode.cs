using Robbi.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robbi.FSM.Nodes
{
    [Serializable]
    [CreateNodeMenu("Robbi/Level/Finish Level")]
    [NodeTint(0.2f, 0.2f, 0.6f)]
    public class FinishLevelNode : FSMNode
    {
        #region FSM Runtime Methods

        protected override void OnEnter()
        {
            base.OnEnter();

            LevelManager levelManager = LevelManager.Load();
            ++levelManager.CurrentLevelIndex;
            levelManager.Save();
        }

        #endregion
    }
}
