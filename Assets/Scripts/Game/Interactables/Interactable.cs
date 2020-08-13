using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Interactables
{
    public class Interactable : ScriptableObject
    {
        public virtual void Interact() { }
    }
}
