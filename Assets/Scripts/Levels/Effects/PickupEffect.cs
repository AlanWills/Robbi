using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Levels.Effects
{
    public struct PickupArgs
    {
    }

    public abstract class PickupEffect : ScriptableObject
    {
        public abstract void Execute(PickupArgs pickupArgs);
    }
}
