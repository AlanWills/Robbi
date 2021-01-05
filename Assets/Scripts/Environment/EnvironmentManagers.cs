using Celeste.Managers;
using Robbi.Levels.Elements;
using Robbi.Movement;
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

        public const string ADDRESSABLE_KEY = "EnvironmentManagers";

        public CollectablesManager collectablesManager;
        public DestructibleCorridorsManager destructibleCorridorsManager;
        public DoorsManager doorsManager;
        public ExitsManager exitsManager;
        public InteractablesManager interactablesManager;
        public MovementManager movementManager;
        public PortalsManager portalsManager;

        #endregion

        public void Initialize(
            IEnumerable<Collectable> collectables,
            IEnumerable<Door> doors,
            IEnumerable<ScriptableObject> interactables,
            IEnumerable<Portal> portals)
        {
            collectablesManager.SetCollectables(collectables);
            doorsManager.SetDoors(doors);
            interactablesManager.SetInteractables(interactables);
            portalsManager.SetPortals(portals);

            gameObject.SetActive(true);
        }
    }
}
