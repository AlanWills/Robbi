using Robbi.Levels.Elements;
using System.Text;
using CelesteEditor.Validation;

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
                    output.AppendLine($"Interactable State Machine {stateMachine.name} has a state {state.name} which is invalid");
                }
            }

            return valid;
        }
    }
}
