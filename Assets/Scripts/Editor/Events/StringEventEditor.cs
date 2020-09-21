using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Robbi.Events
{
    [CustomEditor(typeof(StringEvent))]
    public class StringEventEditor : Editor
    {
        #region Properties and Fields

        private string argument;

        #endregion

        #region GUI

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            argument = EditorGUILayout.TextField("Argument", argument);

            if (GUILayout.Button("Raise"))
            {
                (target as StringEvent).Raise(argument);
            }
        }

        #endregion
    }
}
