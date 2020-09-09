using Robbi.FSM.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace Robbi.FSM
{
    [CustomNodeGraphEditor(typeof(FSMGraph))]
    public class FSMGraphEditor : NodeGraphEditor
    {
        #region Context Menu

        public override string GetNodeMenuName(Type type)
        {
            return typeof(FSMNode).IsAssignableFrom(type) ? base.GetNodeMenuName(type) : null;
        }

        #endregion
    }

    [CustomEditor(typeof(FSMGraph))]
    public class FSMGraphInspector : GlobalGraphEditor
    {
        #region Properties and Fields

        private ScriptableObject removeParameter;

        #endregion

        #region Editor Methods

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.BeginHorizontal();

            removeParameter = EditorGUILayout.ObjectField(removeParameter, typeof(ScriptableObject), false) as ScriptableObject;

            if (GUILayout.Button("Remove Parameter"))
            {
                (target as FSMGraph).RemoveParameter(removeParameter);
                EditorUtility.SetDirty(target);
            }

            EditorGUILayout.EndHorizontal();
        }

        #endregion
    }
}
