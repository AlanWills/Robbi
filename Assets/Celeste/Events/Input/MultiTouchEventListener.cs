using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Events
{
    [AddComponentMenu("Robbi/Events/Multi Touch Event Listener")]
    public class MultiTouchEventListener : ParameterisedEventListener<MultiTouchEventArgs, MultiTouchEvent, MultiTouchUnityEvent>
    {
    }
}
