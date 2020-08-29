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

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            loadingCompleted = false;
            parent.SetActive(false);

            LevelManager levelManager = LevelManager.Load();
            //Addressables.LoadAssetAsync<Level>("Level" + levelManager.CurrentLevelIndex.ToString() + "Data").Completed += LoadLevelComplete; ;
            Resources.Load<Level>("Levels/Level" + levelManager.CurrentLevelIndex + "/Level" + levelManager.CurrentLevelIndex.ToString() + "Data").Begin(parent);
            loadingCompleted = true;
        }

        protected override void OnExit()
        {
            base.OnExit();

            parent.SetActive(true);
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
