using Celeste.Managers;
using Celeste.Parameters;
using UnityEngine;

namespace Robbi.Timing
{
    [AddComponentMenu("Robbi/Time/Time Manager")]
    public class TimeManager : NamedManager
    {
        #region Properties and Fields

        public float speedNormalizer = 4;
        public FloatValue movementSpeed;
        public FloatValue timeTaken;

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
