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

        #endregion
    }
}
