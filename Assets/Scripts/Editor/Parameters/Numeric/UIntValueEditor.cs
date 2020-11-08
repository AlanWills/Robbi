using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Parameters.Numeric
{
    [CustomEditor(typeof(UIntValue))]
    public class UIntValueEditor : ParameterValueEditor<uint>
    {
        #region GUI

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Parameter.value = RobbiEditorGUILayout.UIntField("Value", Parameter.value);
            EditorUtility.SetDirty(Parameter);
        }

        #endregion
    }
}
