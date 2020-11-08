using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Parameters
{
    [CustomEditor(typeof(GameObjectValue))]
    public class GameObjectValueEditor : ParameterValueEditor<GameObject>
    {
        #region GUI

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Parameter.value = EditorGUILayout.ObjectField("Value", Parameter.value, typeof(GameObject), false) as GameObject;
            EditorUtility.SetDirty(Parameter);
        }

        #endregion
    }
}
