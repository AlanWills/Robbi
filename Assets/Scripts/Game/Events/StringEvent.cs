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
    public class StringUnityEvent : UnityEvent<string> { }

    [Serializable]
    [CreateAssetMenu(fileName = "StringEvent", menuName = "Robbi/Events/String Event")]
    public class StringEvent : ParameterisedEvent<string>
    {
    }
}
