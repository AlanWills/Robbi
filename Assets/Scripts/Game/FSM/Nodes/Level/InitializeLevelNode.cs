using Robbi.Debugging.Logging;
using Robbi.Environment;
using Robbi.Levels;
using Robbi.Movement;
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
        public LevelGameObjects levelGameObjects;

        private AsyncOperationHandle<Level> levelLoadingHandle;
        private AsyncOperationHandle<GameObject> robbiLoadingHandle;
        private AsyncOperationHandle<GameObject> managersLoadingHandle;

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            levelLoadingHandle = Addressables.LoadAssetAsync<Level>(string.Format("Level{0}Data", LevelManager.Instance.CurrentLevelIndex));
            robbiLoadingHandle = Addressables.InstantiateAsync("Assets/Prefabs/Level/Robbi.prefab");
            managersLoadingHandle = Addressables.InstantiateAsync("Assets/Prefabs/Level/Managers.prefab");
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
                    LevelManagers managers = new LevelManagers();
                    managers.interactablesManager = managersLoadingHandle.Result.GetComponentInChildren<InteractablesManager>();
                    managers.collectablesManager = managersLoadingHandle.Result.GetComponentInChildren<CollectablesManager>();

                    levelGameObjects.robbiGameObject.value = robbiLoadingHandle.Result;
                    levelGameObjects.managersGameObject.value = managersLoadingHandle.Result;

                    levelLoadingHandle.Result.Begin(levelData, levelGameObjects, managers);
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
            return !(levelLoadingHandle.IsValid() && robbiLoadingHandle.IsValid() && managersLoadingHandle.IsValid());
        }

        private bool IsDone()
        {
            return levelLoadingHandle.IsDone && robbiLoadingHandle.IsDone && managersLoadingHandle.IsDone;
        }

        private bool IsBeginable()
        {
            return levelLoadingHandle.Result != null && robbiLoadingHandle.Result != null && managersLoadingHandle.Result != null;
        }

        #endregion
    }
}
