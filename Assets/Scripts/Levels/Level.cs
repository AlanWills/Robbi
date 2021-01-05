using Robbi.Environment;
using Robbi.Levels.Elements;
using Celeste.Parameters;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Robbi.Levels
{
    [Serializable]
    public struct LevelData
    {
        public IntValue tutorialProgression;
        public Vector3Value playerLocalPosition;
        public IntValue remainingWaypointsPlaceable;
        public BoolValue levelRequiresFuel;
        public UIntValue remainingFuel;
    }

    [Serializable]
    public struct LevelGameObjects
    {
        public GameObjectValue levelGameObject;
        public GameObjectValue tutorialsGameObject;
        public GameObjectValue managersGameObject;
    }

    [CreateAssetMenu(fileName = "Level", menuName = "Robbi/Levels/Level")]
    public class Level : ScriptableObject
    {
        #region Properties and Fields

        public GameObject levelPrefab;
        public GameObject levelTutorial;

        [Header("Level Elements")]
        [SerializeField]
        private List<Portal> portals = new List<Portal>();
        [SerializeField]
        private List<Door> doors = new List<Door>();
        [SerializeField]
        private List<ScriptableObject> interactables = new List<ScriptableObject>();
        [SerializeField]
        private List<Collectable> collectables = new List<Collectable>();

        [Header("Level Parameters")]
        public Vector3Int playerStartPosition;
        public int maxWaypointsPlaceable;
        public bool requiresFuel = false;
        public uint startingFuel;

        #endregion

        #region Initialization

        public void Begin(LevelData levelData, LevelGameObjects levelGameObjects, EnvironmentManagers managers)
        {
            // Set this before instantiating the level so the UI will correctly adapt
            levelData.tutorialProgression.Value = 0;

            GameObject level = GameObject.Instantiate(levelPrefab);
            level.name = levelPrefab.name;

            Grid grid = level.GetComponent<Grid>();
            Debug.Assert(grid != null, "No grid component on level prefab");

            // Make sure this is done before manually setting any game objects to active
            levelData.playerLocalPosition.Value = grid.GetCellCenterLocal(playerStartPosition);
            levelData.remainingWaypointsPlaceable.Value = maxWaypointsPlaceable;
            levelData.levelRequiresFuel.Value = requiresFuel;
            levelData.remainingFuel.Value = startingFuel;

            managers.Initialize(collectables, doors, interactables, portals);

            levelGameObjects.levelGameObject.Value = level;

            if (levelTutorial != null)
            {
                levelGameObjects.tutorialsGameObject.Value = GameObject.Instantiate(levelTutorial);
                levelGameObjects.tutorialsGameObject.Value.name = levelTutorial.name;
            }
        }

        public void End(LevelGameObjects levelGameObjects)
        {
            GameObject.Destroy(levelGameObjects.levelGameObject.Value);
            GameObject.Destroy(levelGameObjects.managersGameObject.Value);

            if (levelGameObjects.tutorialsGameObject.Value != null)
            {
                GameObject.Destroy(levelGameObjects.tutorialsGameObject.Value);
            }
        }

        #endregion
    }
}
