using Robbi.Events.Levels.Elements;
using Robbi.Levels.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            doorEvent.Raise(door);
        }
    }
}
