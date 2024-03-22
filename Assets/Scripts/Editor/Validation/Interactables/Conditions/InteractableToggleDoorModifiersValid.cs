using Robbi.Levels.Elements;
using Robbi.Levels.Modifiers;
using System.Text;
using CelesteEditor.Validation;

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
                        output.AppendLine($"Interactable State Machine {obj.name} has no states");
                    }

                    if (toggleDoor.door == null)
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
