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

        public GameObject robbi;

        private bool loadingCompleted = false;

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            loadingCompleted = false;

            GameObject.Instantiate(robbi);

            LevelManager levelManager = LevelManager.Load();
            //Addressables.LoadAssetAsync<Level>("Level" + levelManager.CurrentLevelIndex.ToString() + "Data").Completed += LoadLevelComplete; ;
            Resources.Load<Level>("Levels/Level" + levelManager.CurrentLevelIndex + "/Level" + levelManager.CurrentLevelIndex.ToString() + "Data").Begin();
            loadingCompleted = true;
        }

        protected override FSMNode OnUpdate()
        {
            return loadingCompleted ? base.OnUpdate() : null;
        }

        //private void LoadLevelComplete(AsyncOperationHandle<Level> level)
        //{
        //    level.Result.Begin(parent);
        //    loadingCompleted = true;
        //}

        #endregion
    }
}
