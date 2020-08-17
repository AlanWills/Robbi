using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Events
{
    public class AbstractGameEvent<T> : ScriptableObject
    {
        #region Properties and Fields

        private List<IEventListener<T>> gameEventListeners = new List<IEventListener<T>>();

        #endregion

        #region Event Management

        public void AddEventListener(IEventListener<T> listener)
        {
            gameEventListeners.Add(listener);
        }

        public void RemoveEventListener(IEventListener<T> listener)
        {
            gameEventListeners.Remove(listener);
        }

        public void Raise(T arguments)
        {
            for (int i = gameEventListeners.Count - 1; i >= 0; --i)
            {
                gameEventListeners[i].OnEventRaised(arguments);
            }
        }

        #endregion
    }
}
