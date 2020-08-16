using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Events
{
    [CreateAssetMenu(fileName = "GameEvent", menuName = "Robbi/Events/Game Event")]
    public class GameEvent : ScriptableObject
    {
        #region Properties and Fields

        private List<GameEventListener> gameEventListeners = new List<GameEventListener>();

        #endregion

        #region Event Management

        public void AddEventListener(GameEventListener listener)
        {
            gameEventListeners.Add(listener);
        }

        public void RemoveEventListener(GameEventListener listener)
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
