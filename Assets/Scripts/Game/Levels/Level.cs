using Robbi.Environment;
using Robbi.Levels.Elements;
using Robbi.Movement;
using Robbi.Parameters;
using Robbi.Viewport;
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
        public IntValue tutorialProgression;
        public Vector3Value playerLocalPosition;
        public IntValue remainingWaypointsPlaceable;
    }

    public struct LevelManagers
    {
        public InteractablesManager interactablesManager;
    }

    [CreateAssetMenu(fileName = "Level", menuName = "Robbi/Levels/Level")]
    public class Level : ScriptableObject
    {
        #region Properties and Fields

        public GameObject levelPrefab;
        public GameObject levelTutorial;

        [Header("Level Parameters")]
        public Vector3Int playerStartPosition;
        public int maxWaypointsPlaceable;

        [Header("Level Elements")]
        [SerializeField]
        private List<Interactable> interactables = new List<Interactable>();

        #endregion

        #region Initialization

        public void Begin(LevelData levelData, LevelManagers managers)
        {
            // Set this before instantiating the level so the UI will correctly adapt
            levelData.tutorialProgression.value = 0;

            GameObject level = GameObject.Instantiate(levelPrefab);
            Grid grid = level.GetComponent<Grid>();
            Debug.Assert(grid != null, "No grid component on level prefab");

            levelData.playerLocalPosition.value = grid.GetCellCenterLocal(playerStartPosition);
            levelData.remainingWaypointsPlaceable.value = maxWaypointsPlaceable;
            
            managers.interactablesManager.Interactables = interactables;

            if (levelTutorial != null)
            {
                GameObject.Instantiate(levelTutorial);
            }
        }

        #endregion
    }
}
