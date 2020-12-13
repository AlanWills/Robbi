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
    public class InteractableOpenDoorModifiersValid : IValidationCondition<Interactable>
    {
        public string DisplayName { get { return "Open Door Modifiers Valid"; } }

        public bool Validate(Interactable obj, StringBuilder output)
        {
            bool valid = true;

            for (int i = 0; i < obj.NumInteractedModifiers; ++i)
            {
                if (obj.GetInteractedModifier(i) is OpenDoor)
                {
                    OpenDoor openDoor = obj.GetInteractedModifier(i) as OpenDoor;

                    if (openDoor.doorEvent == null)
                    {
                        valid = false;
                        output.AppendLineFormat("Interactable {0} has OpenDoor modifier with no doorEvent set", obj.name);
                    }

                    if (openDoor.door == null)
                    {
                        valid = false;
                        output.AppendLineFormat("Interactable {0} has OpenDoor modifier with no door set", obj.name);
                    }
                }
            }

            return valid;
        }
    }
}
