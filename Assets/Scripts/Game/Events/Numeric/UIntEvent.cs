using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Robbi.Events
{
    [Serializable]
    public class UIntUnityEvent : UnityEvent<uint> { }

    [Serializable]
    [CreateAssetMenu(fileName = "UIntEvent", menuName = "Robbi/Events/UInt Event")]
    public class UIntEvent : ParameterisedEvent<uint>
    {
    }
}
