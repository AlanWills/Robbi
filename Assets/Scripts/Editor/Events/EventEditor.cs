using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Robbi.Events
{
    [CustomEditor(typeof(Event))]
    public class EventEditor : Editor
    {
        #region GUI

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Raise"))
            {
                (target as Event).Raise();
            }
        }

        #endregion
    }
}