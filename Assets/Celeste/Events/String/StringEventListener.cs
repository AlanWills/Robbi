using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Events
{
    [AddComponentMenu("Robbi/Events/String Event Listener")]
    public class StringEventListener : ParameterisedEventListener<string, StringEvent, StringUnityEvent>
    {
    }
}
