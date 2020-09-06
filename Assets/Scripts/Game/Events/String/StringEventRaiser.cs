using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Events
{
    [AddComponentMenu("Robbi/Events/String Event Raiser")]
    public class StringEventRaiser : ParameterisedEventRaiser<string, StringEvent, StringUnityEvent>
    {
    }
}
