using Robbi.Levels;
using Robbi.Parameters;
using Robbi.Save;
using Robbi.Settings;
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
            saveData.currentLevel = LevelManager.Instance.CurrentLevelIndex;
            saveData.musicEnabled = SettingsManager.Instance.MusicEnabled;
            saveData.sfxEnabled = SettingsManager.Instance.SfxEnabled;
            saveData.Save();
        }

        #endregion
    }
}
