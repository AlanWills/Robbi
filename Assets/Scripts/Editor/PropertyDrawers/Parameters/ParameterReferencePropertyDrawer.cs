using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.PropertyDrawers.Parameters
{
    public class ParameterReferencePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            SerializedObject serializedReference = new SerializedObject(property.objectReferenceValue);
            SerializedProperty isConstantProperty = serializedReference.FindProperty("isConstant");

            position = new Rect(position.x, position.y, 16, position.height);
            EditorGUI.PropertyField(position, isConstantProperty, GUIContent.none);

            if (isConstantProperty.boolValue)
            {
                position = new Rect(position.x + 20, position.y, 100, position.height);
                EditorGUI.PropertyField(position, serializedReference.FindProperty("constantValue"), GUIContent.none);
            }
            else
            {
                position = new Rect(position.x + 20, position.y, 100, position.height);
                EditorGUI.PropertyField(position, serializedReference.FindProperty("referenceValue"), GUIContent.none);
            }

            serializedReference.ApplyModifiedProperties();

            EditorGUI.EndProperty();
        }

        public static void OnGUI(Object obj)
        {
            SerializedObject serializedReference = new SerializedObject(obj);
            SerializedProperty isConstantProperty = serializedReference.FindProperty("isConstant");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(isConstantProperty, GUIContent.none);
            
            if (isConstantProperty.boolValue)
            {
                EditorGUILayout.PropertyField(serializedReference.FindProperty("constantValue"), GUIContent.none);
            }
            else
            {
                EditorGUILayout.PropertyField(serializedReference.FindProperty("referenceValue"), GUIContent.none);
            }

            serializedReference.ApplyModifiedProperties();

            EditorGUILayout.EndHorizontal();
        }
    }
}
