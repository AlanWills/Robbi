using Robbi.Runtime;
using Robbi.Levels.Elements;
using Celeste.Parameters;
using System;
using System.Collections.Generic;
using UnityEngine;
using Robbi.Collecting;
using Celeste.Assets;
using UnityEngine.AddressableAssets;
using Celeste.Tools.Attributes.GUI;
using Celeste.Tools;

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
        public BoolValue doorToggleBoosterUsable;
        public BoolValue interactBoosterUsable;
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

#if UNITY_EDITOR
        public List<ScriptableObject> Interactables_EditorOnly => interactables;
#endif

        public uint SoftCurrencyPrize
        {
            get => softCurrencyPrize;
            set
            {
                if (value != softCurrencyPrize)
                {
                    softCurrencyPrize = value;
                    EditorOnly.SetDirty(this);
                }
            }
        }

        [SerializeField] private GameObject levelPrefab;

        [Header("Data")]
        [SerializeField] private CollectionTargetRecord collectionTargetRecord;

        [Header("Level Elements")]
        [SerializeField] private List<Portal> portals = new List<Portal>();
        [SerializeField] private List<Door> doors = new List<Door>();
        [SerializeField] private List<ScriptableObject> interactables = new List<ScriptableObject>();
        [SerializeField] private List<Collectable> collectables = new List<Collectable>();
        [SerializeField] private List<LevelCollectionTarget> collectionTargets = new List<LevelCollectionTarget>();
        [SerializeField] private List<Laser> lasers = new List<Laser>();
        [SerializeField] private List<Enemy> enemies = new List<Enemy>();

        [Header("Level Parameters")]
        [SerializeField] private Vector3Int playerStartPosition;
        [SerializeField] private int maxWaypointsPlaceable;
        [SerializeField] private bool requiresFuel = false;
        [ShowIf("requiresFuel")][SerializeField] private uint startingFuel;
        [SerializeField] private uint softCurrencyPrize;

#endregion

        #region Initialization

        public void Begin(LevelData levelData, GameObjectValue levelGameObject, LevelRuntimeManagers managers)
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
            levelData.doorToggleBoosterUsable.Value = doors.Count > 0;
            levelData.interactBoosterUsable.Value = interactables.Count > 0;

            managers.Initialize(collectables, doors, interactables, portals, lasers, enemies);
            collectionTargetRecord.Initialize(collectionTargets);
        }

        #endregion

        #region Factory

        public static Level Create(
            GameObject levelPrefab,
            int maxWaypointsPlaceable,
            bool requiresFuel,
            uint startingFuel,
            uint softCurrencyPrize,
            IReadOnlyList<LevelCollectionTarget> collectionTargets)
        {
            Debug.Assert(levelPrefab != null, "Level Prefab could not be found.");

            Level level = CreateInstance<Level>();
            level.levelPrefab = levelPrefab;
            level.maxWaypointsPlaceable = maxWaypointsPlaceable;
            level.requiresFuel = requiresFuel;
            level.startingFuel = startingFuel;
            level.softCurrencyPrize = softCurrencyPrize;
            level.collectionTargets.AddRange(collectionTargets);

            return level;
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
