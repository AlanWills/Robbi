using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robbi.Events
{
    public interface IEventRaiser
    {
        void Raise();
    }

    public interface IEventRaiser<T>
    {
        void Raise(T argument);
    }
}
