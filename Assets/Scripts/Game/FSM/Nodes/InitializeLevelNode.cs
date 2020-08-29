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
    [CreateNodeMenu("Robbi/Initialize Level Node")]
    public class InitializeLevelNode : FSMNode
    {
        #region Properties and Fields

        public GameObject parent;

        private bool loadingCompleted = false;

        #endregion

        public InitializeLevelNode()
        {
            AddDefaultOutputPort();
        }

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            loadingCompleted = false;

            LevelManager levelManager = LevelManager.Load();
            Addressables.LoadAssetAsync<Level>("Level" + levelManager.CurrentLevelIndex.ToString() + "Data").Completed += LoadLevelComplete; ;
        }

        protected override FSMNode OnUpdate()
        {
            return loadingCompleted ? GetConnectedNode(DEFAULT_OUTPUT_PORT_NAME) : base.OnUpdate();
        }

        private void LoadLevelComplete(AsyncOperationHandle<Level> level)
        {
            level.Result.Begin(parent);
            loadingCompleted = true;
        }

        #endregion
    }
}
