using Robbi.Runtime;
using Robbi.Levels.Elements;
using Celeste.Parameters;
using System;
using System.Collections.Generic;
using UnityEngine;
using Robbi.Collecting;
using Celeste.Assets;
using UnityEngine.AddressableAssets;

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
        public UIntValue softCurrencyPrize;
    }

    [Serializable]
    public struct LevelCollectionTarget
    {
        public CollectionTarget collectionTarget;
        public uint target;
    }

    [CreateAssetMenu(fileName = "Level", menuName = "Robbi/Levels/Level")]
    public class Level : ScriptableObject
    {
        #region Properties and Fields

        public GameObject levelPrefab;

        [Header("Level Elements")]
        [SerializeField] private List<Portal> portals = new List<Portal>();
        [SerializeField] private List<Door> doors = new List<Door>();
        [SerializeField] public List<ScriptableObject> interactables = new List<ScriptableObject>();
        [SerializeField] private List<Collectable> collectables = new List<Collectable>();
        [SerializeField] public List<LevelCollectionTarget> collectionTargets = new List<LevelCollectionTarget>();

        [Header("Level Parameters")]
        public Vector3Int playerStartPosition;
        public int maxWaypointsPlaceable;
        public bool requiresFuel = false;
        public uint startingFuel;
        public uint softCurrencyPrize;

        #endregion

        #region Initialization

        public void Begin(LevelData levelData, GameObjectValue levelGameObject, LevelRuntimeManagers managers, CollectionTargetManager collectionTargetManager)
        {
            // Set this before instantiating the level so the UI will correctly adapt
            levelData.tutorialProgression.Value = 0;

            GameObject level = GameObject.Instantiate(levelPrefab);
            level.name = levelPrefab.name;
            levelGameObject.Value = level;

            Grid grid = level.GetComponent<Grid>();
            Debug.Assert(grid != null, "No grid component on level prefab");

            // Make sure this is done before manually setting any game objects to active
            levelData.playerLocalPosition.Value = grid.GetCellCenterLocal(playerStartPosition);
            levelData.remainingWaypointsPlaceable.Value = maxWaypointsPlaceable;
            levelData.levelRequiresFuel.Value = requiresFuel;
            levelData.remainingFuel.Value = startingFuel;
            levelData.softCurrencyPrize.Value = softCurrencyPrize;

            managers.Initialize(collectables, doors, interactables, portals);
            collectionTargetManager.Initialize(collectionTargets);
        }

        #endregion

        #region Loading

        public static AsyncOperationHandleWrapper LoadAsync(uint levelIndex)
        {
            return new AsyncOperationHandleWrapper(Addressables.LoadAssetAsync<Level>(string.Format("Level{0}Data", levelIndex)));
        }

        #endregion
    }
}
