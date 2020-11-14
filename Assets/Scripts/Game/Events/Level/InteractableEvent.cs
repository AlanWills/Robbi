using Robbi.Levels.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

namespace Robbi.Events
{
    [Serializable]
    public class InteractableUnityEvent : UnityEvent<Interactable> { }

    [Serializable]
    [CreateAssetMenu(fileName = "Interactable", menuName = "Robbi/Events/Interactable Event")]
    public class InteractableEvent : ParameterisedEvent<Interactable>
    {
    }
}
