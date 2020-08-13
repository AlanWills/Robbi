#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Exit
{
    [AddComponentMenu("Robbi/Exit/Exit Manager")]
    public class ExitManager : MonoBehaviour
    {
        #region Properties and Fields

        public Tilemap exitTilemap;
        public GameObject player;

        private Grid grid;

        #endregion

        #region Unity Methods

        private void Start()
        {
            grid = exitTilemap.layoutGrid;
        }

        private void Update()
        {
            Vector3Int currentCell = grid.WorldToCell(player.transform.position);
            if (exitTilemap.HasTile(currentCell))
            {
                // Finish Level
#if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
#else
                Application.Quit();
#endif
            }
        }

#endregion
    }
}
