using Robbi.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Game.Inputs
{
    [AddComponentMenu("Robbi/Input/Input Listener")]
    public class InputListener : MonoBehaviour, IEventListener<GameObject>
    {
        public void OnEventRaised(GameObject arguments)
        {
            throw new NotImplementedException();
        }
    }
}
