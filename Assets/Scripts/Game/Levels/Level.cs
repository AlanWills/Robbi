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

        [Header("Level Conditions")]
        public Vector3Int playerStartPosition;
        public int maxWaypointsPlaceable;
        [SerializeField] private List<Vector3Int> exits = new List<Vector3Int>();

        #endregion

        #region Initialization

        public void Begin(Vector3Value playerLocalPosition, IntValue remainingWaypointsPlaceable, Vector3IntArrayValue exitPositions)
        {
            GameObject level = GameObject.Instantiate(levelPrefab);
            Grid grid = level.GetComponent<Grid>();
            Debug.Assert(grid != null, "No grid component on level prefab");

            playerLocalPosition.value = grid.GetCellCenterLocal(playerStartPosition);
            remainingWaypointsPlaceable.value = maxWaypointsPlaceable;
            exitPositions.value.AddRange(exits);
        }

        #endregion
    }
}
