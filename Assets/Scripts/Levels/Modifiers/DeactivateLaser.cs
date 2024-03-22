using Robbi.Events.Levels.Elements;
using Robbi.Levels.Elements;

namespace Robbi.Levels.Modifiers
{
    public class DeactivateLaser : LevelModifier
    {
        public LaserEvent laserEvent;
        public Laser laser;

        public override void CopyFrom(LevelModifier original)
        {
            DeactivateLaser deactivateLaser = original as DeactivateLaser;
            laser = deactivateLaser.laser;
            laserEvent = deactivateLaser.laserEvent;
        }

        public override void Execute(InteractArgs interactArgs)
        {
            laserEvent.Invoke(laser);
        }
    }
}
