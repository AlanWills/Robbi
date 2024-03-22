using Robbi.Levels.Elements;
using Robbi.Movement;
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
        [SerializeField] private PortalsManager portalsManager;
        [SerializeField] private LasersManager lasersManager;
        [SerializeField] private TimeManager timeManager;
        [SerializeField] private LevelResultManager levelResultManager;

        #endregion

        public void Initialize(
            IEnumerable<Collectable> collectables,
            IEnumerable<Door> doors,
            IEnumerable<ScriptableObject> interactables,
            IEnumerable<Portal> portals,
            IEnumerable<Laser> lasers)
        {
            collectablesManager.Initialize(collectables);
            doorsManager.Initialize(doors);
            interactablesManager.Initialize(interactables);
            movementManager.Initialize();
            portalsManager.Initialize(portals);
            lasersManager.Initialize(lasers);
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
            doorsManager.Cleanup();
            interactablesManager.Cleanup();
            movementManager.Cleanup();
            portalsManager.Cleanup();
            lasersManager.Cleanup();
            timeManager.Cleanup();
            levelResultManager.Cleanup();

            for (int i = 0; i < transform.childCount; ++i)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
