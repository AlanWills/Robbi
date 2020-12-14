using Robbi.Events;
using Robbi.Events.Levels.Elements;
using Robbi.Levels.Effects;
using Robbi.Levels.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robbi.Levels.Modifiers
{
    public class ModifyFuel : PickupEffect
    {
        public UIntEvent modifyFuelEvent;
        public uint amount = 10;

        public override void Execute(PickupArgs pickupArgs)
        {
            modifyFuelEvent.Raise(amount);
        }
    }
}
