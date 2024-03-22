using Robbi.Events.Levels.Elements;
using Robbi.Levels.Elements;

namespace Robbi.Levels.Modifiers
{
    public class ActivateLaser : LevelModifier
    {
        public LaserEvent laserEvent;
        public Laser laser;

        public override void CopyFrom(LevelModifier original)
        {
            ActivateLaser activateLaser = original as ActivateLaser;
            laser = activateLaser.laser;
            laserEvent = activateLaser.laserEvent;
        }

        public override void Execute(InteractArgs interactArgs)
        {
            laserEvent.Invoke(laser);
        }
    }
}
