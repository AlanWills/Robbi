using Robbi.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace RobbiEditor.Events
{
    [CustomEditor(typeof(Event))]
    public class EventEditor : Editor
    {
        #region GUI

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (UnityEngine.GUILayout.Button("Raise"))
            {
                (target as Event).Raise();
            }
        }

        #endregion
    }
}