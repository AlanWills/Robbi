using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Robbi.Events
{
    [CustomEditor(typeof(Vector3IntEvent))]
    public class Vector3EventEditor : Editor
    {
        #region Properties and Fields

        private Vector3Int argument;

        #endregion

        #region GUI

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            argument = EditorGUILayout.Vector3IntField("Argument", argument);

            if (GUILayout.Button("Raise"))
            {
                (target as Vector3IntEvent).Raise(argument);
            }
        }

        #endregion
    }
}
