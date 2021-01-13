using Celeste.FSM;
using Celeste.Log;
using Celeste.Parameters;
using Robbi.Runtime;
using Robbi.Levels;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using static XNode.Node;
using Celeste.Assets;
using Robbi.Collecting;

namespace Robbi.FSM.Nodes
{
    [Serializable]
    [CreateNodeMenu("Robbi/Level/Initialize Level")]
    [NodeTint(0.2f, 0.2f, 0.6f)]
    public class InitializeLevelNode : FSMNode
    {
        #region Properties and Fields

        private bool IsDone
        {
            get { return levelLoadingHandle.IsDone && collectionTargetManagerHandle.IsDone; }
        }

        private bool HasError
        {
            get { return levelLoadingHandle.HasError || collectionTargetManagerHandle.HasError; }
        }

        [Header("Parameters")]
        public LevelData levelData;
        public GameObjectValue levelGameObject;

        private AsyncOperationHandleWrapper levelLoadingHandle;
        private AsyncOperationHandleWrapper collectionTargetManagerHandle;

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            levelLoadingHandle = Level.LoadAsync(LevelManager.Instance.CurrentLevel);
            collectionTargetManagerHandle = CollectionTargetManager.LoadAsync();
        }

        protected override FSMNode OnUpdate()
        {
            if (IsDone)
            {
                if (!HasError)
                {
                    // Don't love it, but sometimes you have to do a little evil to do a greater good
                    LevelRuntimeManagers managers = GameObject.Find(LevelRuntimeManagers.NAME).GetComponent<LevelRuntimeManagers>();
                    Level level = levelLoadingHandle.Get<Level>();
                    CollectionTargetManager collectionTargetManager = collectionTargetManagerHandle.Get<CollectionTargetManager>();

                    level.Begin(levelData, levelGameObject, managers, collectionTargetManager);
                }
                else
                {
                    HudLog.LogError("Error loading Level");
                }

                return base.OnUpdate();
            }

            return this;
        }

        #endregion
    }
}
