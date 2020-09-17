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
        public Vector3Value playerLocalPosition;
        public IntValue remainingWaypointsPlaceable;
        public Vector3IntArrayValue exitPositions;

        [Header("Objects")]
        public GameObject robbi;

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            GameObject.Instantiate(robbi);
            LevelManager levelManager = LevelManager.Load();

            Level level = Resources.Load<Level>("Levels/Level" + levelManager.CurrentLevelIndex + "/Level" + levelManager.CurrentLevelIndex.ToString() + "Data");
            Debug.AssertFormat(level != null, "Level {0} could not be loaded", levelManager.CurrentLevelIndex);

            if (level != null)
            {
                level.Begin(playerLocalPosition, remainingWaypointsPlaceable, exitPositions);
            }
        }

        protected override FSMNode OnUpdate()
        {
            return base.OnUpdate();
        }

        #endregion
    }
}
