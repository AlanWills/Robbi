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

    [Serializable]
    public struct LevelGameObjects
    {
        public GameObjectValue levelGameObject;
        public GameObjectValue robbiGameObject;
        public GameObjectValue managersGameObject;
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

        public void Begin(LevelData levelData, LevelGameObjects levelGameObjects, LevelManagers managers)
        {
            // Set this before instantiating the level so the UI will correctly adapt
            levelData.tutorialProgression.value = 0;

            GameObject level = GameObject.Instantiate(levelPrefab);
            level.name = levelPrefab.name;

            levelGameObjects.levelGameObject.value = level;
            levelGameObjects.robbiGameObject.value.name = "Robbi";
            levelGameObjects.managersGameObject.value.SetActive(true);
            levelGameObjects.managersGameObject.value.name = "Managers";

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

        public void End(LevelGameObjects levelGameObjects)
        {
            GameObject.Destroy(levelGameObjects.levelGameObject.value);
            GameObject.Destroy(levelGameObjects.robbiGameObject.value);
            GameObject.Destroy(levelGameObjects.managersGameObject.value);
        }

        #endregion
    }
}
