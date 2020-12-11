using Robbi.FSM;
using RobbiEditor.Validation.FSM.Conditions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RobbiEditor.Validation.FSM
{
    public static class FSMValidator
    {
        #region Properties and Fields

        public static int NumValidationConditions
        {
            get { return validationConditions.Count; }
        }

        private static List<IFSMValidationCondition> validationConditions = new List<IFSMValidationCondition>();

        #endregion

        static FSMValidator()
        {
            Debug.Log("Loading FSM Validation conditions");
            validationConditions.Clear();

            Type fsmValidationCondition = typeof(IFSMValidationCondition);
            foreach (Type t in Assembly.GetAssembly(fsmValidationCondition).GetTypes())
            {
                if (fsmValidationCondition.IsAssignableFrom(t) && !t.IsAbstract)
                {
                    validationConditions.Add(Activator.CreateInstance(t) as IFSMValidationCondition);
                }
            }
        }

        #region Validation Methods

        public static IFSMValidationCondition GetValidationCondition(int validationCondition)
        {
            return 0 <= validationCondition && validationCondition < NumValidationConditions ? validationConditions[validationCondition] : null;
        }

        public static bool Validate(FSMGraph fsmGraph)
        {
            bool passesValidation = true;
            StringBuilder error = new StringBuilder(1024);

            foreach (IFSMValidationCondition validationCondition in validationConditions)
            {
                error.Clear();
                passesValidation &= validationCondition.Validate(fsmGraph, error);

                if (error.Length > 0)
                {
                    Debug.LogAssertion(error.ToString());
                }
            }

            return passesValidation;
        }

        #endregion
    }
}
