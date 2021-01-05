using Celeste.FSM;
using Celeste.Log;
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
        public LevelGameObjects levelGameObjects;

        private AsyncOperationHandle<Level> levelLoadingHandle;
        private AsyncOperationHandle<GameObject> managersLoadingHandle;

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            levelLoadingHandle = Addressables.LoadAssetAsync<Level>(string.Format("Level{0}Data", LevelManager.Instance.CurrentLevel));
            managersLoadingHandle = Addressables.InstantiateAsync(EnvironmentManagers.ADDRESSABLE_KEY);
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
                    levelGameObjects.managersGameObject.Value = managersLoadingHandle.Result;

                    EnvironmentManagers managers = managersLoadingHandle.Result.GetComponent<EnvironmentManagers>();
                    levelLoadingHandle.Result.Begin(levelData, levelGameObjects, managers);
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
            return !(levelLoadingHandle.IsValid() && managersLoadingHandle.IsValid());
        }

        private bool IsDone()
        {
            return levelLoadingHandle.IsDone && managersLoadingHandle.IsDone;
        }

        private bool IsBeginable()
        {
            return levelLoadingHandle.Result != null && managersLoadingHandle.Result != null;
        }

        #endregion
    }
}
