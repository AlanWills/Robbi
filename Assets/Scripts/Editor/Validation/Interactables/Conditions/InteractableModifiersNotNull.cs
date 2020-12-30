using Robbi.Levels.Elements;
using Celeste.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    output.AppendLineFormat("Interactable {0} has a null modifier removed at index {1}", interactable.name, i.ToString());
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
                    output.AppendLineFormat("Interactable {0} has a null modifier at index {1}", interactable.name, i.ToString());
                }
            }

            return valid;
        }
    }
}
