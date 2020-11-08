using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Parameters.Vector
{
    [CustomEditor(typeof(Vector3IntValue))]
    public class Vector3IntEditor : ParameterValueEditor<Vector3Int>
    {
        #region GUI

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Parameter.value = EditorGUILayout.Vector3IntField("Value", Parameter.value);
            EditorUtility.SetDirty(Parameter);
        }

        #endregion
    }
}
