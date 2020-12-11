using Robbi.FSM;
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
    public class IFSMValidationConditionGUI
    {
        private enum ConditionStatus
        { 
            NotRun,
            Passed,
            Failed
        }

        #region Properties and Fields

        private IFSMValidationCondition validationCondition;
        private ConditionStatus conditionStatus = ConditionStatus.NotRun;
        private StringBuilder output = new StringBuilder(512);

        #endregion

        public IFSMValidationConditionGUI(IFSMValidationCondition validationCondition)
        {
            this.validationCondition = validationCondition;
        }

        #region Validation Methods

        public void Reset()
        {
            conditionStatus = ConditionStatus.NotRun;
        }

        public void Check(FSMGraph fsmGraph)
        {
            output.Clear();
            conditionStatus = validationCondition.Validate(fsmGraph, output) ? ConditionStatus.Passed : ConditionStatus.Failed;

            if (output.Length > 0)
            {
                if (conditionStatus == ConditionStatus.Failed)
                {
                    Debug.LogAssertion(output.ToString());
                }
                else
                {
                    Debug.Log(conditionStatus.ToString());
                }
            }
        }

        public bool CanFix(FSMGraph fsmGraph)
        {
            return conditionStatus == ConditionStatus.Failed && 
                   validationCondition is IFixableCondition && 
                   (validationCondition as IFixableCondition).CanFix(fsmGraph);
        }

        public void Fix(FSMGraph fsmGraph)
        {
            if (CanFix(fsmGraph))
            {
                output.Clear();
                (validationCondition as IFixableCondition).Fix(fsmGraph, output);
                Check(fsmGraph);

                if (output.Length > 0)
                {
                    Debug.Log(output.ToString());
                }
            }
        }

        #endregion

        #region GUI

        public void GUI(FSMGraph fsmGraph)
        {
            EditorGUILayout.BeginHorizontal();

            GUIStyle guiStyle = UnityEngine.GUI.skin.label;
            if (conditionStatus == ConditionStatus.Passed)
            {
                guiStyle = RobbiEditorStyles.SuccessLabel;
            }
            else if (conditionStatus == ConditionStatus.Failed)
            {
                guiStyle = RobbiEditorStyles.ErrorLabel;
            }

            EditorGUILayout.LabelField(validationCondition.DisplayName, guiStyle);

            if (GUILayout.Button("Check", GUILayout.ExpandWidth(false)))
            {
                Check(fsmGraph);
            }

            if (CanFix(fsmGraph) && GUILayout.Button("Fix", GUILayout.ExpandWidth(false)))
            {
                Fix(fsmGraph);
            }

            EditorGUILayout.EndHorizontal();
        }

        #endregion
    }
}
