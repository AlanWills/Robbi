using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robbi.Events
{
    public interface IEvent
    {
        void AddEventListener(IEventListener listener);
        void RemoveEventListener(IEventListener listener);
        void Raise();
    }

    public interface IEvent<T>
    {
        void AddEventListener(IEventListener<T> listener);
        void RemoveEventListener(IEventListener<T> listener);
        void Raise(T argument);
    }
}
