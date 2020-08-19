using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Robbi.Events
{
    public interface IEventListener<T>
    {
        void OnEventRaised(T arguments);
    }

    public class AbstractEventListener<T, TEvent, TUnityEvent> : MonoBehaviour, IEventListener<T> 
                                                                                       where TEvent : AbstractEvent<T>
                                                                                       where TUnityEvent : UnityEvent<T>
    {
        #region Properties and Fields

        public TEvent gameEvent;
        public TUnityEvent response;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            gameEvent.AddEventListener(this);
        }

        private void OnDisable()
        {
            gameEvent.RemoveEventListener(this);
        }

        #endregion

        #region Response Methods

        public void OnEventRaised(T arguments)
        {
            response.Invoke(arguments);
        }

        #endregion
    }
}
