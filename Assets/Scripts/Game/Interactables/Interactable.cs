using Robbi.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Interactables
{
    [CreateAssetMenu(fileName = "Interactable", menuName = "Robbi/Interactables/Interactable")]
    public class Interactable : ScriptableObject
    {
        #region Properties and Fields

        public Events.Event onInteract;

        #endregion

        public void Interact()
        {
            onInteract.Raise();
        }
    }
}
