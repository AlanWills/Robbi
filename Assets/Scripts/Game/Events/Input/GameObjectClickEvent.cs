using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Robbi.Events
{
    public struct GameObjectClickEventArgs
    {
        public GameObject gameObject;
        public Vector3 clickWorldPosition;
    }

    [Serializable]
    public class GameObjectClickUnityEvent : UnityEvent<GameObjectClickEventArgs> { }

    [Serializable]
    [CreateAssetMenu(fileName = "GameObjectClickEvent", menuName = "Robbi/Events/GameObject Click Event")]
    public class GameObjectClickEvent : ParameterisedEvent<GameObjectClickEventArgs>
    {
    }
}
