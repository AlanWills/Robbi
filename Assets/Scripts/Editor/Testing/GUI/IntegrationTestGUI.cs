using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Testing.GUI
{
    public class IntegrationTestGUI
    {
        #region Properties and Fields

        private static GUIStyle passedLabelStyle;
        private static GUIStyle PassedLabelStyle
        {
            get
            {
                if (passedLabelStyle == null)
                {
                    Color green = new Color(0, 0.75f, 0);

                    passedLabelStyle = new GUIStyle(UnityEngine.GUI.skin.label);
                    passedLabelStyle.normal.textColor = green;
                    passedLabelStyle.hover.textColor = green;
                    passedLabelStyle.focused.textColor = green;
                    passedLabelStyle.active.textColor = green;
                }

                return passedLabelStyle;
            }
        }

        private static GUIStyle failedLabelStyle;
        private static GUIStyle FailedLabelStyle
        {
            get
            {
                if (failedLabelStyle == null)
                {
                    failedLabelStyle = new GUIStyle(UnityEngine.GUI.skin.label);
                    failedLabelStyle.normal.textColor = Color.red;
                    failedLabelStyle.hover.textColor = Color.red;
                    failedLabelStyle.focused.textColor = Color.red;
                    failedLabelStyle.active.textColor = Color.red;
                }

                return failedLabelStyle;
            }
        }

        private static GUIStyle notRunLabelStyle;
        private static GUIStyle NotRunLabelStyle
        {
            get
            {
                if (notRunLabelStyle == null)
                {
                    notRunLabelStyle = new GUIStyle(UnityEngine.GUI.skin.label);
                }

                return notRunLabelStyle;
            }
        }

        public bool IsSelected { get; set; }
        public TestResult Result { get; set; }
        public string TestName { get; private set; }

        #endregion

        public IntegrationTestGUI(Type integrationTestType, TestResult result)
        {
            IsSelected = false;
            Result = result;
            TestName = integrationTestType.Name;
        }

        public void GUI()
        {
            GUIStyle style;

            if (Result == TestResult.Passed)
            {
                style = PassedLabelStyle;
            }
            else if (Result == TestResult.Failed)
            {
                style = FailedLabelStyle;
            }
            else
            {
                style = NotRunLabelStyle;
            }

            IsSelected = EditorGUILayout.ToggleLeft(TestName, IsSelected, style);
        }
    }
}
