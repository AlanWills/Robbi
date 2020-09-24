using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Levels
{
    [Serializable]
    public struct LevelData
    {
        public Vector3Value playerLocalPosition;
        public IntValue remainingWaypointsPlaceable;
        public TilemapValue exitsTilemap;
        public TilemapValue doorsTilemap;
    }

    [CreateAssetMenu(fileName = "Level", menuName = "Robbi/Levels/Level")]
    public class Level : ScriptableObject
    {
        #region Properties and Fields

        public GameObject levelPrefab;

        [Header("Level Conditions")]
        public Vector3Int playerStartPosition;
        public int maxWaypointsPlaceable;

        #endregion

        #region Initialization

        public void Begin(LevelData levelData)
        {
            GameObject level = GameObject.Instantiate(levelPrefab);
            Grid grid = level.GetComponent<Grid>();
            Debug.Assert(grid != null, "No grid component on level prefab");

            levelData.playerLocalPosition.value = grid.GetCellCenterLocal(playerStartPosition);
            levelData.remainingWaypointsPlaceable.value = maxWaypointsPlaceable;
            levelData.exitsTilemap.value = level.transform.Find("Exits").GetComponent<Tilemap>();
            levelData.doorsTilemap.value = level.transform.Find("Doors").GetComponent<Tilemap>();
        }

        #endregion
    }
}
