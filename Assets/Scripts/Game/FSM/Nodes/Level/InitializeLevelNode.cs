using Robbi.Debugging.Logging;
using Robbi.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Robbi.FSM.Nodes
{
    [Serializable]
    [CreateNodeMenu("Robbi/Level/Initialize Level Node")]
    [NodeTint(0.2f, 0.2f, 0.6f)]
    public class InitializeLevelNode : FSMNode
    {
        #region Properties and Fields

        public GameObject robbi;

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            GameObject.Instantiate(robbi);

            LevelManager levelManager = LevelManager.Load();
            Resources.Load<Level>("Levels/Level" + levelManager.CurrentLevelIndex + "/Level" + levelManager.CurrentLevelIndex.ToString() + "Data").Begin();
        }

        protected override FSMNode OnUpdate()
        {
            return base.OnUpdate();
        }

        #endregion
    }
}
