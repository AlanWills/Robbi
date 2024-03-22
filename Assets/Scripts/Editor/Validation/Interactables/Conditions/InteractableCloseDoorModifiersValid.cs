using Robbi.Levels.Elements;
using Robbi.Levels.Modifiers;
using System.Text;
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
                        output.AppendLine($"Interactable State Machine {obj.name} has no states");
                    }

                    if (closeDoor.door == null)
                    {
                        valid = false;
                        output.AppendLine($"Interactable State Machine {obj.name} has no states");
                    }
                }
            }

            return valid;
        }
    }
}
