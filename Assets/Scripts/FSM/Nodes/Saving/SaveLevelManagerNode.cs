using Robbi.Levels;
using Celeste.Parameters;
using Robbi.Options;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Celeste.FSM;

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
