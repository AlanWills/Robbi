using Robbi.Debugging.Logging;
using Robbi.Environment;
using Robbi.Levels;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Robbi.FSM.Nodes
{
    [Serializable]
    [CreateNodeMenu("Robbi/Level/Cleanup Level")]
    [NodeTint(0.2f, 0.2f, 0.6f)]
    public class CleanupLevelNode : FSMNode
    {
        #region Properties and Fields

        [Header("Parameters")]
        public LevelGameObjects levelGameObjects;

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
                HudLogger.LogError("Error loading Level - Invalid Handle");
                return base.OnUpdate();
            }
            else if (IsDone())
            {
                if (IsCleanupable())
                {
                    levelLoadingHandle.Result.End(levelGameObjects);
                }
                else
                {
                    HudLogger.LogError("Error loading Level - Result Null");
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

        private bool IsCleanupable()
        {
            return levelLoadingHandle.Result != null;
        }

        #endregion
    }
}
