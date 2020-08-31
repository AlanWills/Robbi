using Robbi.FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace RobbiEditor.FSM
{
    [CustomEditor(typeof(FSMRuntime))]
    public class FSMRuntimeEditor : SceneGraphEditor
    {
        #region GUI

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            FSMRuntime fsmRuntime = (target as FSMRuntime);
            fsmRuntime.graph = EditorGUILayout.ObjectField(fsmRuntime.graph, typeof(FSMGraph), false) as FSMGraph;

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(fsmRuntime);
            }

            base.OnInspectorGUI();
        }

        #endregion
    }
}
