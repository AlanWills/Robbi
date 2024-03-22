using Robbi.Levels.Elements;
using Robbi.Levels.Modifiers;
using System.Text;
using CelesteEditor.Validation;

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
                        output.AppendLine($"Interactable State Machine {obj.name} has no states");
                    }

                    if (openDoor.door == null)
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
