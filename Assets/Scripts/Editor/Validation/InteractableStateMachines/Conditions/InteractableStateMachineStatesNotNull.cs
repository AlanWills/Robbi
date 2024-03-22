using Robbi.Levels.Elements;
using System.Text;
using CelesteEditor.Validation;

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
                    output.AppendLine($"Interactable State Machine {stateMachine.name} has a state removed at index {i}");
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
                    output.AppendLine($"Interactable State Machine {stateMachine.name} has a state at index {i} which is invalid");
                }
            }

            return valid;
        }
    }
}
