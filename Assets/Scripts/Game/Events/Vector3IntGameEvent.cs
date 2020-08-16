using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Robbi.Events
{
    [System.Serializable]
    public class Vector3IntUnityEvent : UnityEvent<Vector3Int> { }

    [CreateAssetMenu(fileName = "Vector3IntGameEvent", menuName = "Robbi/Events/Vector3Int Game Event")]
    public class Vector3IntGameEvent : ScriptableObject
    {
        #region Properties and Fields

        private List<Vector3IntGameEventListener> gameEventListeners = new List<Vector3IntGameEventListener>();

        #endregion

        #region Event Management

        public void AddEventListener(Vector3IntGameEventListener listener)
        {
            gameEventListeners.Add(listener);
        }

        public void RemoveEventListener(Vector3IntGameEventListener listener)
        {
            gameEventListeners.Remove(listener);
        }

        public void Raise(Vector3Int argument)
        {
            for (int i = gameEventListeners.Count - 1; i >= 0; --i)
            {
                gameEventListeners[i].OnEventRaised(argument);
            }
        }

        #endregion
    }
}
