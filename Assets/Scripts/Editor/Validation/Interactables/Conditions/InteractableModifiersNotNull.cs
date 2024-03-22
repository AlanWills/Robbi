using Robbi.Levels.Elements;
using System.Text;
using CelesteEditor.Validation;

namespace RobbiEditor.Validation.Interactables.Conditions
{
    public class InteractableModifiersNotNull : IValidationCondition<Interactable>, IFixableCondition<Interactable>
    {
        public string DisplayName { get { return "Modifiers Not Null"; } }

        public bool CanFix(Interactable asset)
        {
            return true;
        }

        public void Fix(Interactable interactable, StringBuilder output)
        {
            for (int i = interactable.NumInteractedModifiers - 1; i >= 0; --i)
            {
                if (interactable.GetInteractedModifier(i) == null)
                {
                    interactable.RemoveInteractedModifier(i);
                    output.AppendLine($"Interactable {interactable.name} has a null modifier removed at index {i}");
                }
            }
        }

        public bool Validate(Interactable interactable, StringBuilder output)
        {
            bool valid = true;

            for (int i = 0; i < interactable.NumInteractedModifiers; ++i)
            {
                if (interactable.GetInteractedModifier(i) == null)
                {
                    valid = false;
                    output.AppendLine($"Interactable {interactable.name} has a null modifier removed at index {i}");
                }
            }

            return valid;
        }
    }
}
