using Robbi.Attributes.GUI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(HideIfAttribute))]
    public class HideIfPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            HideIfAttribute hideIfAttribute = attribute as HideIfAttribute;
            SerializedProperty dependentProperty = property.serializedObject.FindProperty(hideIfAttribute.DependentName);

            return dependentProperty.boolValue ? EditorGUI.GetPropertyHeight(property, label, true) : 0f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            HideIfAttribute hideIfAttribute = attribute as HideIfAttribute;
            SerializedProperty dependentProperty = property.serializedObject.FindProperty(hideIfAttribute.DependentName);

            EditorGUI.BeginProperty(position, label, property);

            if (dependentProperty.boolValue)
            {
                EditorGUI.PropertyField(position, property, label, true);
            }

            EditorGUI.EndProperty();
        }
    }
}