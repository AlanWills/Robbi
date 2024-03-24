using Celeste.FSM;
using Celeste.Log;
using Celeste.Parameters;
using Robbi.Runtime;
using Robbi.Levels;
using System;
using UnityEngine;
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

        private bool IsDone => levelLoadingHandle.IsDone;
        private bool HasError => levelLoadingHandle.HasError;

        [Header("Parameters")]
        [SerializeField] private LevelData levelData;
        [SerializeField] private GameObjectValue levelGameObject;

        [Header("Data")]
        [SerializeField] private LevelRecord levelRecord;
        [SerializeField] private CollectionTargetRecord collectionTargetRecord;

        private AsyncOperationHandleWrapper levelLoadingHandle;

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            levelLoadingHandle = Level.LoadAsync(levelRecord.CurrentLevel);
        }

        protected override FSMNode OnUpdate()
        {
            if (IsDone)
            {
                if (!HasError)
                {
                    // TODO: Use an event here instead
                    LevelRuntimeManagers managers = GameObject.Find(LevelRuntimeManagers.NAME).GetComponent<LevelRuntimeManagers>();
                    Level level = levelLoadingHandle.Get<Level>();

                    level.Begin(levelData, levelGameObject, managers, collectionTargetRecord);
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
