using Celeste.Managers;
using Robbi.Levels.Elements;
using Robbi.Movement;
using Robbi.Runtime.Actors;
using Robbi.Timing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Runtime
{
    [AddComponentMenu("Robbi/Runtime/Level Runtime Managers")]
    public class LevelRuntimeManagers : NamedManager
    {
        #region Properties and Fields

        public const string NAME = "LevelRuntimeManagers";

        public CollectablesManager collectablesManager;
        public DestructibleCorridorsManager destructibleCorridorsManager;
        public DoorsManager doorsManager;
        public ExitsManager exitsManager;
        public InteractablesManager interactablesManager;
        public MovementManager movementManager;
        public FuelManager fuelManager;
        public PortalsManager portalsManager;
        public LasersManager lasersManager;
        public EnemiesManager enemiesManager;
        public TimeManager timeManager;
        public LevelResultManager levelResultManager;
        public PlayerRuntime playerRuntime;

        #endregion

        public void Initialize(
            IEnumerable<Collectable> collectables,
            IEnumerable<Door> doors,
            IEnumerable<ScriptableObject> interactables,
            IEnumerable<Portal> portals,
            IEnumerable<Laser> lasers,
            IEnumerable<Enemy> enemies)
        {
            collectablesManager.Initialize(collectables);
            destructibleCorridorsManager.Initialize();
            doorsManager.Initialize(doors);
            exitsManager.Initialize();
            interactablesManager.Initialize(interactables);
            movementManager.Initialize();
            fuelManager.Initialize();
            portalsManager.Initialize(portals);
            lasersManager.Initialize(lasers);
            enemiesManager.Initialize(enemies);
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
