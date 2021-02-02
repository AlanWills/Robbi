using Robbi.Events.Levels.Elements;
using Robbi.Levels.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            laserEvent.Raise(laser);
        }
    }
}
