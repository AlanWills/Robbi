using Robbi.Levels.Elements;
using System.Text;
using CelesteEditor.Validation;

namespace RobbiEditor.Validation.Interactables.Conditions
{
    public class InteractableStateMachineHasAtLeastOneState : IValidationCondition<InteractableStateMachine>
    {
        public string DisplayName { get { return "Has At Least One State"; } }

        public bool Validate(InteractableStateMachine obj, StringBuilder output)
        {
            if (obj.NumStates == 0)
            {
                output.AppendLine($"Interactable State Machine {obj.name} has no states");
                return false;
            }

            return true;
        }
    }
}
