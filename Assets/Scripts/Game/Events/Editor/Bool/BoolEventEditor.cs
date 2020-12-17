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
    [CustomEditor(typeof(BoolEvent))]
    public class BoolEventEditor : ParameterisedEventEditor<bool, BoolEvent>
    {
        protected override bool DrawArgument(bool argument)
        {
            return EditorGUILayout.Toggle(argument);
        }
    }
}
