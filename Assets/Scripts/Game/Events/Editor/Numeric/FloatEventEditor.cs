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
    [CustomEditor(typeof(FloatEvent))]
    public class FloatEventEditor : ParameterisedEventEditor<float, FloatEvent>
    {
        protected override float DrawArgument(float argument)
        {
            return EditorGUILayout.FloatField(argument);
        }
    }
}
