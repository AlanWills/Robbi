using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Parameters.Bool
{
    [CustomEditor(typeof(BoolValue))]
    public class BoolValueEditor : ParameterValueEditor<bool>
    {
        #region GUI

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Parameter.value = EditorGUILayout.Toggle("Value", Parameter.value);
            EditorUtility.SetDirty(Parameter);
        }

        #endregion
    }
}
