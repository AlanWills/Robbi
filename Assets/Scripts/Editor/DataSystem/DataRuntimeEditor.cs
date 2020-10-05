using Robbi.DataSystem;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace RobbiEditor.FSM
{
    [CustomEditor(typeof(DataRuntime))]
    public class DataRuntimeEditor : SceneGraphEditor
    {
        #region Properties and Fields

        private Object parentObject;

        #endregion

        #region GUI

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUI.BeginChangeCheck();

            DataRuntime dataRuntime = (target as DataRuntime);
            dataRuntime.graph = EditorGUILayout.ObjectField(dataRuntime.graph, typeof(DataGraph), false) as DataGraph;

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(dataRuntime);
            }
        }

        #endregion
    }
}
