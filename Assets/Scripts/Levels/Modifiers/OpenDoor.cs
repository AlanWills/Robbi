using Robbi.Events.Levels.Elements;
using Robbi.Levels.Elements;

namespace Robbi.Levels.Modifiers
{
    public class OpenDoor : LevelModifier
    {
        public DoorEvent doorEvent;
        public Door door;

        public override void CopyFrom(LevelModifier original)
        {
            OpenDoor openDoor = original as OpenDoor;
            door = openDoor.door;
            doorEvent = openDoor.doorEvent;
        }

        public override void Execute(InteractArgs interactArgs)
        {
            doorEvent.Invoke(door);
        }
    }
}
