using Robbi.Levels;
using Robbi.Parameters;
using Robbi.Save;
using Robbi.Options;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.FSM.Nodes.Game
{
    [CreateNodeMenu("Robbi/Game/Save Game")]
    [NodeTint(0.2f, 0.2f, 0.6f)]
    public class SaveGameNode : FSMNode
    {
        #region FSM Runtime Overrides

        protected override void OnEnter()
        {
            base.OnEnter();

            SaveData saveData = new SaveData();
            LevelManager.Instance.Serialize(saveData);
            OptionsManager.Instance.Serialize(saveData);
            saveData.Save();
        }

        #endregion
    }
}
