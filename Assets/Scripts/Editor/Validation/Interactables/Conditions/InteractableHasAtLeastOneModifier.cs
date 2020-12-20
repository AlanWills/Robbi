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
    public class InteractableHasAtLeastOneModifier : IValidationCondition<Interactable>
    {
        public string DisplayName { get { return "Has At Least One Modifier"; } }

        public bool Validate(Interactable obj, StringBuilder output)
        {
            if (obj.NumInteractedModifiers == 0)
            {
                output.AppendLineFormat("Interactable {0} has no modifiers", obj.name);
                return false;
            }

            return true;
        }
    }
}
