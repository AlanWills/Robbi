using Celeste.Events;
using Celeste.Parameters;
using UnityEngine;

namespace Robbi.Runtime.Actors
{
    [AddComponentMenu("Robbi/Runtime/Actors/Player Runtime")]
    public class PlayerRuntime : CharacterRuntime
    {
        #region Properties and Fields

        public override Vector3 Position
        {
            get { return localPosition.Value; }
            set { localPosition.Value = value; }
        }

        [SerializeField] private Vector3Value localPosition;

        [Header("Level Lose")]
        public StringEvent levelLostEvent;
        public StringValue hitLaserReason;
        public StringValue caughtByEnemyReason;

        #endregion

        #region Unity Methods

        private void Update()
        {
            transform.localPosition = localPosition.Value;
        }

        #endregion

        #region Environment Runtime

        public override void OnHitByLaser()
        {
            levelLostEvent.Invoke(hitLaserReason.Value);
        }

        public void OnReachedByEnemy()
        {
            levelLostEvent.Invoke(caughtByEnemyReason.Value);
        }

        #endregion
    }
}
