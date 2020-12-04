using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Events
{
    [AddComponentMenu("Robbi/Events/UInt Event Listener")]
    public class UIntEventListener : ParameterisedEventListener<uint, UIntEvent, UIntUnityEvent>
    {
    }
}
