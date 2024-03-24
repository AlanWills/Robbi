using Celeste.Events;
using Celeste.FSM.Nodes.Events.Conditions;
using Robbi.Events.Runtime.Actors;
using Robbi.Runtime.Actors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.FSM.Nodes.Events.Conditions
{
    [Serializable]
    [DisplayName("Character Runtime")]
    public class CharacterRuntimeEventCondition : ParameterizedEventCondition<CharacterRuntime, CharacterRuntimeEvent>
    {
    }
}
