using Robbi.Events.Levels.Elements;
using Robbi.Levels.Elements;

namespace Robbi.Levels.Modifiers
{
    public class CloseDoor : LevelModifier
    {
        public DoorEvent doorEvent;
        public Door door;

        public override void CopyFrom(LevelModifier original)
        {
            CloseDoor closeDoor = original as CloseDoor;
            door = closeDoor.door;
            doorEvent = closeDoor.doorEvent;
        }

        public override void Execute(InteractArgs interactArgs)
        {
            doorEvent.Invoke(door);
        }
    }
}
