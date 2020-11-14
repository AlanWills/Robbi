using Robbi.Events;
using Robbi.Levels.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robbi.FSM.Nodes.Events
{
    [Serializable]
    [CreateNodeMenu("Robbi/Events/Raisers/InteractableEvent Raiser")]
    public class InteractableEventRaiserNode : ParameterisedEventRaiserNode<Interactable, InteractableEvent>
    {
    }
}
