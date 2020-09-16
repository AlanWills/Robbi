using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Levels
{
    [CreateAssetMenu(fileName = "Level", menuName = "Robbi/Levels/Level")]
    public class Level : ScriptableObject
    {
        #region Properties and Fields

        public GameObject levelPrefab;

        [Header("Parameters")]
        public Vector3Value playerLocalPosition;
        public IntValue remainingWaypointsPlaceable;

        [Header("Level Conditions")]
        public Vector3Int playerStartPosition;
        public int maxWaypointsPlaceable;

        #endregion

        #region Initialization

        public void Begin()
        {
            GameObject level = GameObject.Instantiate(levelPrefab);
            Grid grid = level.GetComponent<Grid>();
            Debug.Assert(grid != null, "No grid component on level prefab");

            playerLocalPosition.value = grid.GetCellCenterLocal(playerStartPosition);
            remainingWaypointsPlaceable.value = maxWaypointsPlaceable;
        }

        #endregion
    }
}
