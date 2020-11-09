using Robbi.Levels;
using Robbi.Parameters;
using Robbi.Options;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.FSM.Nodes.Game
{
    [CreateNodeMenu("Robbi/Saving/Save Level Manager")]
    [NodeTint(0.2f, 0.2f, 0.6f)]
    public class SaveLevelManagerNode : FSMNode
    {
        #region FSM Runtime Overrides

        protected override void OnEnter()
        {
            base.OnEnter();

            LevelManager.Instance.Save();
        }

        #endregion
    }
}
