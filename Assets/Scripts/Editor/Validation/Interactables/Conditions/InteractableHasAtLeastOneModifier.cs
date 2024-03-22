using Robbi.Levels.Elements;
using System.Text;
using CelesteEditor.Validation;

namespace RobbiEditor.Validation.Interactables.Conditions
{
    public class InteractableHasAtLeastOneModifier : IValidationCondition<Interactable>
    {
        public string DisplayName { get { return "Has At Least One Modifier"; } }

        public bool Validate(Interactable obj, StringBuilder output)
        {
            if (obj.NumInteractedModifiers == 0)
            {
                output.AppendLine($"Interactable State Machine {obj.name} has no states");
                return false;
            }

            return true;
        }
    }
}
