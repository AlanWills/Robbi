using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Celeste.Events
{
    [AddComponentMenu("Celeste/Events/String Event Raiser")]
    public class StringEventRaiser : ParameterisedEventRaiser<string, StringEvent, StringUnityEvent>
    {
    }
}
