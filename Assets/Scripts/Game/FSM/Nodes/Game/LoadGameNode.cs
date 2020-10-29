using Robbi.Parameters;
using Robbi.Save;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.FSM.Nodes.Game
{
    [CreateNodeMenu("Robbi/Game/Load Game")]
    [NodeTint(0.2f, 0.2f, 0.6f)]
    public class LoadGameNode : FSMNode
    {
        #region Properties and Fields

        public UIntValue currentLevel;

        #endregion

        #region FSM Runtime Overrides

        protected override void OnEnter()
        {
            base.OnEnter();

            SaveData saveData = SaveData.Load();
            currentLevel.value = saveData.currentLevel;
        }

        #endregion
    }
}
