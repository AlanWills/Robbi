using Robbi.Levels.Elements;
using Robbi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobbiEditor.Validation.Interactables.Conditions
{
    public class InteractableStateMachineHasAtLeastOneState : IValidationCondition<InteractableStateMachine>
    {
        public string DisplayName { get { return "Has At Least One State"; } }

        public bool Validate(InteractableStateMachine obj, StringBuilder output)
        {
            if (obj.NumStates == 0)
            {
                output.AppendLineFormat("Interactable State Machine {0} has no states", obj.name);
                return false;
            }

            return true;
        }
    }
}
