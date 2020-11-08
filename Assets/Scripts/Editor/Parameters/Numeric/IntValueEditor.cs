using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Parameters.Numeric
{
    [CustomEditor(typeof(IntValue))]
    public class IntValueEditor : ParameterValueEditor<int>
    {
        #region GUI

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Parameter.value = EditorGUILayout.IntField("Value", Parameter.value);
            EditorUtility.SetDirty(Parameter);
        }

        #endregion
    }
}
