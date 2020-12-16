using Robbi.Levels.Elements;
using Robbi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobbiEditor.Validation.Interactables.Conditions
{
    public class InteractableStateMachineStatesNotNull : IValidationCondition<InteractableStateMachine>, IFixableCondition<InteractableStateMachine>
    {
        public string DisplayName { get { return "States Not Null"; } }

        public bool CanFix(InteractableStateMachine asset)
        {
            return true;
        }

        public void Fix(InteractableStateMachine stateMachine, StringBuilder output)
        {
            for (int i = stateMachine.NumStates - 1; i >= 0 ; --i)
            {
                Interactable state = stateMachine.GetState(i);
                if (state == null)
                {
                    stateMachine.RemoveState(i);
                    output.AppendLineFormat("Interactable State Machine {0} has a state removed at index {1}", stateMachine.name, i.ToString());
                }
            }
        }

        public bool Validate(InteractableStateMachine stateMachine, StringBuilder output)
        {
            bool valid = true;

            for (int i = 0; i < stateMachine.NumStates; ++i)
            {
                Interactable state = stateMachine.GetState(i);
                if (state == null)
                {
                    valid = false;
                    output.AppendLineFormat("Interactable State Machine {0} has a state at index {1} which is invalid", stateMachine.name, i.ToString());
                }
            }

            return valid;
        }
    }
}
