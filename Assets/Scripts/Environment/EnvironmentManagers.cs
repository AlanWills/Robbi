using Celeste.Managers;
using Robbi.Levels.Elements;
using Robbi.Movement;
using Robbi.Timing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Environment
{
    [AddComponentMenu("Robbi/Environment/Environment Managers")]
    public class EnvironmentManagers : NamedManager
    {
        #region Properties and Fields

        public const string NAME = "EnvironmentManagers";

        public CollectablesManager collectablesManager;
        public DestructibleCorridorsManager destructibleCorridorsManager;
        public DoorsManager doorsManager;
        public ExitsManager exitsManager;
        public InteractablesManager interactablesManager;
        public MovementManager movementManager;
        public PortalsManager portalsManager;
        public TimeManager timeManager;

        #endregion

        public void Initialize(
            IEnumerable<Collectable> collectables,
            IEnumerable<Door> doors,
            IEnumerable<ScriptableObject> interactables,
            IEnumerable<Portal> portals)
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }

            collectablesManager.Initialize(collectables);
            destructibleCorridorsManager.Initialize();
            doorsManager.Initialize(doors);
            exitsManager.Initialize();
            interactablesManager.Initialize(interactables);
            movementManager.Initialize();
            portalsManager.Initialize(portals);
            timeManager.Initialize();
        }

        public void Cleanup()
        {
            collectablesManager.Cleanup();
            destructibleCorridorsManager.Cleanup();
            doorsManager.Cleanup();
            exitsManager.Cleanup();
            interactablesManager.Cleanup();
            movementManager.Cleanup();
            portalsManager.Cleanup();
            timeManager.Cleanup();

            for (int i = 0; i < transform.childCount; ++i)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
