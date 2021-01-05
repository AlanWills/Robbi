using Celeste.FSM;
using Celeste.Log;
using Celeste.Parameters;
using Robbi.Environment;
using Robbi.Levels;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using static XNode.Node;

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
        public GameObjectValue levelGameObject;

        private AsyncOperationHandle<Level> levelLoadingHandle;

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            levelLoadingHandle = Addressables.LoadAssetAsync<Level>(string.Format("Level{0}Data", LevelManager.Instance.CurrentLevel));
        }

        protected override FSMNode OnUpdate()
        {
            if (IsInvalid())
            {
                HudLog.LogError("Error loading Level - Invalid Handle");
                return base.OnUpdate();
            }
            else if (IsDone())
            {
                if (IsBeginable())
                {
                    // Don't love it, but sometimes you have to do a little evil to do a greater good
                    EnvironmentManagers managers = GameObject.Find(EnvironmentManagers.NAME).GetComponent<EnvironmentManagers>();
                    levelLoadingHandle.Result.Begin(levelData, levelGameObject, managers);
                }
                else
                {
                    HudLog.LogError("Error loading Level - Result Null");
                }

                return base.OnUpdate();
            }

            return this;
        }

        #endregion

        #region Utility Methods

        private bool IsInvalid()
        {
            return !levelLoadingHandle.IsValid();
        }

        private bool IsDone()
        {
            return levelLoadingHandle.IsDone;
        }

        private bool IsBeginable()
        {
            return levelLoadingHandle.Result != null;
        }

        #endregion
    }
}
