using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RobbiEditor
{
    public static class RobbiEditorStyles
    {
        #region Properties and Fields

        private static GUIStyle blackLabel;
        public static GUIStyle BlackLabel
        {
            get
            {
                if (blackLabel == null)
                {
                    blackLabel = new GUIStyle(GUI.skin.label);
                    blackLabel.normal.textColor = Color.black;
                }

                return blackLabel;
            }
        }

        private static GUIStyle boldLabel;
        public static GUIStyle BoldLabel
        {
            get
            {
                if (boldLabel == null)
                {
                    boldLabel = new GUIStyle(GUI.skin.label);
                    boldLabel.fontStyle = FontStyle.Bold;
                }

                return boldLabel;
            }
        }

        #endregion
    }
}
