using Celeste.Events;

namespace Robbi.Levels.Effects
{
    public class ModifyFuel : PickupEffect
    {
        public UIntEvent modifyFuelEvent;
        public uint amount = 10;

        public override void Execute(PickupArgs pickupArgs)
        {
            modifyFuelEvent.Invoke(amount);
        }
    }
}
