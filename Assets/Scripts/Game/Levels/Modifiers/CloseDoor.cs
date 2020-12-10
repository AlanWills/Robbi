using Robbi.Events.Levels.Elements;
using Robbi.Levels.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robbi.Levels.Modifiers
{
    public class CloseDoor : LevelModifier
    {
        public DoorEvent doorEvent;
        public Door door;

        public override void Execute(InteractArgs interactArgs)
        {
            if (door.DoorState == DoorState.Opened)
            {
                doorEvent.Raise(door);
            }
        }
    }
}
