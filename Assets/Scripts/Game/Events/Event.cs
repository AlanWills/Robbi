using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Events
{
    [CreateAssetMenu(fileName = "Event", menuName = "Robbi/Events/Event")]
    public class Event : ScriptableObject
    {
        #region Properties and Fields

        private List<IEventListener> gameEventListeners = new List<IEventListener>();

        #endregion

        #region Event Management

        public void AddEventListener(IEventListener listener)
        {
            gameEventListeners.Add(listener);
        }

        public void RemoveEventListener(IEventListener listener)
        {
            gameEventListeners.Remove(listener);
        }

        public void Raise()
        {
            for (int i = gameEventListeners.Count - 1; i >= 0; --i)
            {
                gameEventListeners[i].OnEventRaised();
            }
        }

        #endregion
    }
}
