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
    [CreateNodeMenu("Robbi/Game/Load Game")]
    [NodeTint(0.2f, 0.2f, 0.6f)]
    public class LoadGameNode : FSMNode
    {
        #region FSM Runtime Overrides

        protected override void OnEnter()
        {
            base.OnEnter();

            SaveData saveData = SaveData.Load();
            LevelManager.Instance.CurrentLevelIndex = saveData.currentLevel;
            OptionsManager.Instance.MusicEnabled = saveData.musicEnabled;
            OptionsManager.Instance.SfxEnabled = saveData.sfxEnabled;
            OptionsManager.Instance.DefaultMovementSpeed = saveData.defaultMovementSpeed;
        }

        #endregion
    }
}
