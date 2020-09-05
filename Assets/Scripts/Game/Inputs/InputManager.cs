using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Game.Inputs
{
    [AddComponentMenu("Robbi/Input/Input Manager")]
    public class InputManager : MonoBehaviour
    {
        #region Unity Methods

        // Have events fired in particular input situations
        // Also have a function for doing raycasting etc.
        // This will listen out for those events being fired and then do appropriate behaviour (e.g. raycasting)
        // Then we can simulate input
        // Finally, will have to have a component which can be notified it's been clicked or maybe an event where we passed the clicked game object
        // Can have a component which derives from the listener interface, but also does the check internally to see if it's this clicked game object
        // before Invoking on UnityEvent.  Will save boilerplate at listener sites everywhere (call it InputListener)

        #endregion
    }
}
