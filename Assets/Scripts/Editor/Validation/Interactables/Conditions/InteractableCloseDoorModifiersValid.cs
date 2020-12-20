using Robbi.Levels.Elements;
using Robbi.Levels.Modifiers;
using Celeste.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CelesteEditor.Validation;

namespace RobbiEditor.Validation.Interactables.Conditions
{
    public class InteractableCloseDoorModifiersValid : IValidationCondition<Interactable>
    {
        public string DisplayName { get { return "Close Door Modifiers Valid"; } }

        public bool Validate(Interactable obj, StringBuilder output)
        {
            bool valid = true;

            for (int i = 0; i < obj.NumInteractedModifiers; ++i)
            {
                if (obj.GetInteractedModifier(i) is CloseDoor)
                {
                    CloseDoor closeDoor = obj.GetInteractedModifier(i) as CloseDoor;

                    if (closeDoor.doorEvent == null)
                    {
                        valid = false;
                        output.AppendLineFormat("Interactable {0} has CloseDoor modifier with no doorEvent set", obj.name);
                    }

                    if (closeDoor.door == null)
                    {
                        valid = false;
                        output.AppendLineFormat("Interactable {0} has CloseDoor modifier with no door set", obj.name);
                    }
                }
            }

            return valid;
        }
    }
}
