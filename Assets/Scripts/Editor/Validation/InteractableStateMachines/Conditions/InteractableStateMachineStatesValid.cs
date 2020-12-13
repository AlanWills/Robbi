using Robbi.Levels.Elements;
using Robbi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobbiEditor.Validation.Interactables.Conditions
{
    public class InteractableStateMachineStatesValid : IValidationCondition<InteractableStateMachine>
    {
        public string DisplayName { get { return "States Valid"; } }

        public bool Validate(InteractableStateMachine stateMachine, StringBuilder output)
        {
            bool valid = true;

            for (int i = 0; i < stateMachine.NumStates; ++i)
            {
                Interactable state = stateMachine.GetState(i);
                if (!Validator<Interactable>.Validate(state))
                {
                    valid = false;
                    output.AppendLineFormat("Interactable State Machine {0} has a state {1} which is invalid", stateMachine.name, state.name);
                }
            }

            return valid;
        }
    }
}
