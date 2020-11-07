using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Events
{
    [AddComponentMenu("Robbi/Events/Bool Event Raiser")]
    public class BoolEventRaiser : ParameterisedEventRaiser<bool, BoolEvent, BoolUnityEvent>
    {
    }
}
