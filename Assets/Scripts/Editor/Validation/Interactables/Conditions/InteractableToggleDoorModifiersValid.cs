using Robbi.Levels.Elements;
using Robbi.Levels.Modifiers;
using Robbi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobbiEditor.Validation.Interactables.Conditions
{
    public class InteractableToggleDoorModifiersValid : IValidationCondition<Interactable>
    {
        public string DisplayName { get { return "Toggle Door Modifiers Valid"; } }

        public bool Validate(Interactable obj, StringBuilder output)
        {
            bool valid = true;

            for (int i = 0; i < obj.NumInteractedModifiers; ++i)
            {
                if (obj.GetInteractedModifier(i) is ToggleDoor)
                {
                    ToggleDoor toggleDoor = obj.GetInteractedModifier(i) as ToggleDoor;

                    if (toggleDoor.doorEvent == null)
                    {
                        valid = false;
                        output.AppendLineFormat("Interactable {0} has ToggleDoor modifier with no doorEvent set", obj.name);
                    }

                    if (toggleDoor.door == null)
                    {
                        valid = false;
                        output.AppendLineFormat("Interactable {0} has ToggleDoor modifier with no door set", obj.name);
                    }
                }
            }

            return valid;
        }
    }
}
