using Robbi.Levels.Elements;
using Robbi.Movement;
using Robbi.Runtime.Actors;
using Robbi.Timing;
using System.Collections.Generic;
using UnityEngine;

namespace Robbi.Runtime
{
    [AddComponentMenu("Robbi/Runtime/Level Runtime Managers")]
    public class LevelRuntimeManagers : MonoBehaviour
    {
        #region Properties and Fields

        public const string NAME = "LevelRuntimeManagers";

        [SerializeField] private CollectablesManager collectablesManager;
        [SerializeField] private DestructibleCorridorsManager destructibleCorridorsManager;
        [SerializeField] private DoorsManager doorsManager;
        [SerializeField] private ExitsManager exitsManager;
        [SerializeField] private InteractablesManager interactablesManager;
        [SerializeField] private MovementManager movementManager;
        [SerializeField] private FuelManager fuelManager;
        [SerializeField] private PortalsManager portalsManager;
        [SerializeField] private LasersManager lasersManager;
        [SerializeField] private EnemiesManager enemiesManager;
        [SerializeField] private TimeManager timeManager;
        [SerializeField] private LevelResultManager levelResultManager;
        [SerializeField] private PlayerRuntime playerRuntime;

        #endregion

        public void Initialize(
            IEnumerable<Collectable> collectables,
            IEnumerable<Door> doors,
            IEnumerable<ScriptableObject> interactables,
            IEnumerable<Portal> portals,
            IEnumerable<Laser> lasers,
            IEnumerable<Enemy> enemies)
        {
            // Initialize enemies first so we can pass them to other people if necessary
            enemiesManager.Initialize(enemies, playerRuntime);

            List<CharacterRuntime> allCharacterRuntimes = new List<CharacterRuntime>();
            allCharacterRuntimes.Add(playerRuntime);
            allCharacterRuntimes.AddRange(enemiesManager.Enemies);

            collectablesManager.Initialize(collectables);
            destructibleCorridorsManager.Initialize();
            doorsManager.Initialize(doors);
            exitsManager.Initialize();
            interactablesManager.Initialize(interactables);
            movementManager.Initialize(playerRuntime);
            fuelManager.Initialize();
            portalsManager.Initialize(portals);
            lasersManager.Initialize(lasers, allCharacterRuntimes);
            timeManager.Initialize();
            levelResultManager.Initialize();
            
            for (int i = 0; i < transform.childCount; ++i)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        public void Cleanup()
        {
            collectablesManager.Cleanup();
            destructibleCorridorsManager.Cleanup();
            doorsManager.Cleanup();
            exitsManager.Cleanup();
            interactablesManager.Cleanup();
            movementManager.Cleanup();
            fuelManager.Cleanup();
            portalsManager.Cleanup();
            lasersManager.Cleanup();
            enemiesManager.Cleanup();
            timeManager.Cleanup();
            levelResultManager.Cleanup();

            for (int i = 0; i < transform.childCount; ++i)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
