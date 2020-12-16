using Robbi.Events.Levels.Elements;
using Robbi.Levels.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robbi.Levels.Modifiers
{
    public class ToggleDoor : LevelModifier
    {
        public DoorEvent doorEvent;
        public Door door;

        public override void CopyFrom(LevelModifier original)
        {
            ToggleDoor toggleDoor = original as ToggleDoor;
            door = toggleDoor.door;
            doorEvent = toggleDoor.doorEvent;
        }

        public override void Execute(InteractArgs interactArgs)
        {
            doorEvent.Raise(door);
        }
    }
}
