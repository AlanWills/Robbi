using Celeste.Parameters;
using UnityEngine;

namespace Robbi.Timing
{
    [AddComponentMenu("Robbi/Time/Time Manager")]
    public class TimeManager : MonoBehaviour
    {
        #region Properties and Fields

        [SerializeField] private float speedNormalizer = 4;
        [SerializeField] private FloatValue movementSpeed;
        [SerializeField] private FloatValue timeTaken;

        #endregion

        #region IEnvironmentManager

        public void Initialize() 
        { 
            timeTaken.Value = 0; 
        }

        public void Cleanup() { }

        #endregion

        #region Unity Methods

        private void Update()
        {
            timeTaken.Value += (Time.deltaTime * movementSpeed.Value / speedNormalizer);
        }

        #endregion
    }
}
