using Robbi.FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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

            if (fsmRuntime.graph != null && GUILayout.Button("Open graph", GUILayout.Height(40)))
            {
                NodeEditorWindow.Open(fsmRuntime.graph);
            }
        }

        #endregion
    }
}
