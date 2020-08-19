using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Robbi.Events
{
    public interface IEventRaiser<T>
    {
        void Raise(T argument);
    }

    public class AbstractEventRaiser<T, TEvent, TUnityEvent> : MonoBehaviour, IEventRaiser<T>
                                                                                       where TEvent : AbstractEvent<T>
                                                                                       where TUnityEvent : UnityEvent<T>
    {
        #region Properties and Fields

        public TEvent gameEvent;

        #endregion

        #region Response Methods

        public void Raise(T arguments)
        {
            gameEvent.Raise(arguments);
        }

        #endregion
    }
}
