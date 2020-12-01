using Robbi.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Testing
{
    [CustomEditor(typeof(IntegrationTestRunner))]
    public class IntegrationTestRunnerEditor : Editor
    {
        #region GUI

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Clear Tests", GUILayout.ExpandWidth(false)))
            {
                (target as IntegrationTestRunner).ClearTests();
            }

            base.OnInspectorGUI();
        }

        #endregion
    }
}
