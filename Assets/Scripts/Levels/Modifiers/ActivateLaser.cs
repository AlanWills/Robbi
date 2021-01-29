using Robbi.Events.Levels.Elements;
using Robbi.Levels.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            laserEvent.Raise(laser);
        }
    }
}
