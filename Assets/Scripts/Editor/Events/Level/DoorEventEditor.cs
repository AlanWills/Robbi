using Robbi.Events;
using Robbi.Levels.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Events
{
    [CustomEditor(typeof(DoorEvent))]
    public class DoorEventEditor : ParameterisedEventEditor<Door, DoorEvent>
    {
        protected override Door DrawArgument(Door argument)
        {
            return EditorGUILayout.ObjectField(argument, typeof(Door), false) as Door;
        }
    }
}
