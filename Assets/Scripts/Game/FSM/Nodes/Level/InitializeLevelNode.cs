using Robbi.Debugging.Logging;
using Robbi.Levels;
using Robbi.Parameters;
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
    [CreateNodeMenu("Robbi/Level/Initialize Level")]
    [NodeTint(0.2f, 0.2f, 0.6f)]
    public class InitializeLevelNode : FSMNode
    {
        #region Properties and Fields

        [Header("Parameters")]
        public LevelData levelData;

        [Header("Objects")]
        public GameObject robbi;

        private AsyncOperationHandle<Level> levelLoadingHandle;

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            LevelManager levelManager = LevelManager.Load();
            levelLoadingHandle = Addressables.LoadAssetAsync<Level>(string.Format("Levels/Level{0}Data.asset", levelManager.CurrentLevelIndex));
        }

        protected override FSMNode OnUpdate()
        {
            if (levelLoadingHandle.IsDone && levelLoadingHandle.Result != null)
            {
                levelLoadingHandle.Result.Begin(levelData);
                GameObject.Instantiate(robbi);
                
                return base.OnUpdate();
            }

            return this;
        }

        #endregion
    }
}
