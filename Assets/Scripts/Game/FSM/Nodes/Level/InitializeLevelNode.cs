﻿using Robbi.Debugging.Logging;
using Robbi.Levels;
using System;
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

        private AsyncOperationHandle<Level> levelLoadingHandle;
        private AsyncOperationHandle<GameObject> robbiLoadingHandle;
        private AsyncOperationHandle<GameObject> movementManagerLoadingHandle;

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            levelLoadingHandle = Addressables.LoadAssetAsync<Level>(string.Format("Assets/Levels/Level{0}/Level{0}Data.asset", LevelManager.Instance.CurrentLevelIndex));
            robbiLoadingHandle = Addressables.InstantiateAsync("Assets/Prefabs/Level/Robbi.prefab");
            movementManagerLoadingHandle = Addressables.InstantiateAsync("Assets/Prefabs/Level/MovementManager.prefab");
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
                if (IsBeginable())
                {
                    levelLoadingHandle.Result.Begin(levelData);
                    movementManagerLoadingHandle.Result.SetActive(true);
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
            return !(levelLoadingHandle.IsValid() && robbiLoadingHandle.IsValid() && movementManagerLoadingHandle.IsValid());
        }

        private bool IsDone()
        {
            return levelLoadingHandle.IsDone && robbiLoadingHandle.IsDone && movementManagerLoadingHandle.IsDone;
        }

        private bool IsBeginable()
        {
            return levelLoadingHandle.Result != null && robbiLoadingHandle.Result != null && movementManagerLoadingHandle.Result != null;
        }

        #endregion
    }
}
