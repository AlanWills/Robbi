using Robbi.Events.Levels.Elements;
using Robbi.Levels.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robbi.Levels.Modifiers
{
    public class ToggleLaser : LevelModifier
    {
        public LaserEvent laserEvent;
        public Laser laser;

        public override void CopyFrom(LevelModifier original)
        {
            ToggleLaser toggleLaser = original as ToggleLaser;
            laser = toggleLaser.laser;
            laserEvent = toggleLaser.laserEvent;
        }

        public override void Execute(InteractArgs interactArgs)
        {
            laserEvent.Raise(laser);
        }
    }
}
