using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Parameters.Numeric
{
    [CustomEditor(typeof(FloatValue))]
    public class FloatValueEditor : ParameterValueEditor<float>
    {
        #region GUI

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Parameter.value = EditorGUILayout.FloatField("Value", Parameter.value);
            EditorUtility.SetDirty(Parameter);
        }

        #endregion
    }
}
