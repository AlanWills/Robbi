using Celeste.Events;
using Robbi.Levels.Elements;
using Robbi.Runtime.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Robbi.Events.Runtime.Actors
{
    [Serializable]
    public class CharacterRuntimeUnityEvent : UnityEvent<CharacterRuntime> { }

    [Serializable]
    [CreateAssetMenu(fileName = "New CharacterRuntimeEvent", menuName = "Robbi/Events/Runtime/Character Runtime Event")]
    public class CharacterRuntimeEvent : ParameterisedEvent<CharacterRuntime>
    {
    }
}
