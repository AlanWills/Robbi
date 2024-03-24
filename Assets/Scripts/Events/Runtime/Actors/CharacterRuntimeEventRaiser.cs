using Celeste.Events;
using Robbi.Levels.Elements;
using Robbi.Runtime.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Events.Runtime.Actors
{
    [AddComponentMenu("Robbi/Events/Runtime/Character Runtime Event Raiser")]
    public class CharacterRuntimeEventRaiser : ParameterisedEventRaiser<CharacterRuntime, CharacterRuntimeEvent>
    {
    }
}
