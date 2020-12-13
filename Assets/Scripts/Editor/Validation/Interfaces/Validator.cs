using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RobbiEditor.Validation
{
    public static class Validator<T> where T : UnityEngine.Object
    {
        #region Properties and Fields

        public static int NumValidationConditions
        {
            get { return validationConditions.Count; }
        }

        private static List<IValidationCondition<T>> validationConditions = new List<IValidationCondition<T>>();

        #endregion

        static Validator()
        {
            Debug.LogFormat("Loading {0} validation conditions", typeof(T).Name);
            validationConditions.Clear();

            Type validationCondition = typeof(IValidationCondition<T>);
            foreach (Type t in Assembly.GetAssembly(validationCondition).GetTypes())
            {
                if (validationCondition.IsAssignableFrom(t) && !t.IsAbstract)
                {
                    validationConditions.Add(Activator.CreateInstance(t) as IValidationCondition<T>);
                }
            }
        }

        #region Validation Methods

        public static IValidationCondition<T> GetValidationCondition(int validationCondition)
        {
            return 0 <= validationCondition && validationCondition < NumValidationConditions ? validationConditions[validationCondition] : null;
        }

        public static bool Validate(T asset)
        {
            bool passesValidation = true;
            StringBuilder error = new StringBuilder(1024);

            foreach (IValidationCondition<T> validationCondition in validationConditions)
            {
                error.Clear();
                passesValidation &= validationCondition.Validate(asset, error);

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
