using Robbi.FSM;
using RobbiEditor.Utils;
using RobbiEditor.Validation.FSM.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Validation.FSM.GUIs
{
    public class FSMValidatorGUI
    {
        #region Properties and Fields

        private List<IFSMValidationConditionGUI> validationConditionGUIs = new List<IFSMValidationConditionGUI>();

        #endregion

        public FSMValidatorGUI()
        {
            for (int i = 0; i < FSMValidator.NumValidationConditions; ++i)
            {
                validationConditionGUIs.Add(new IFSMValidationConditionGUI(FSMValidator.GetValidationCondition(i)));
            }
        }

        #region GUI

        public void GUI(FSMGraph fsmGraph)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Validation", RobbiEditorStyles.BoldLabel);

            if (GUILayout.Button("Check All", GUILayout.ExpandWidth(false)))
            {
                LogUtils.Clear();

                foreach (IFSMValidationConditionGUI gui in validationConditionGUIs)
                {
                    gui.Check(fsmGraph);
                }
            }

            if (GUILayout.Button("Fix All", GUILayout.ExpandWidth(false)))
            {
                LogUtils.Clear();

                foreach (IFSMValidationConditionGUI gui in validationConditionGUIs)
                {
                    gui.Fix(fsmGraph);
                }
            }

            if (GUILayout.Button("Reset All", GUILayout.ExpandWidth(false)))
            {
                foreach (IFSMValidationConditionGUI gui in validationConditionGUIs)
                {
                    gui.Reset();
                }
            }

            if (GUILayout.Button("Clear Log", GUILayout.ExpandWidth(false)))
            {
                LogUtils.Clear();
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Separator();

            foreach (IFSMValidationConditionGUI gui in validationConditionGUIs)
            {
                gui.GUI(fsmGraph);
            }

            RobbiEditorGUILayout.HorizontalLine();
        }

        #endregion
    }
}
