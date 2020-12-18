using Robbi.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Events
{
    [CustomEditor(typeof(GameObjectEvent))]
    public class GameObjectEventEditor : ParameterisedEventEditor<GameObject, GameObjectEvent>
    {
        protected override GameObject DrawArgument(GameObject argument)
        {
            return EditorGUILayout.ObjectField(argument, typeof(GameObject), true) as GameObject;
        }
    }
}
