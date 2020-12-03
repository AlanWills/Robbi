using Robbi.Events;
using Robbi.Managers;
using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Collectables
{
    [AddComponentMenu("Robbi/Collectables/Fuel Manager")]
    public class FuelManager : NamedManager
    {
        #region Properties and Fields

        public BoolValue levelRequiresFuel;
        public UIntValue remainingFuel;
        public Events.Event levelLoseOutOfFuel;

        #endregion

        #region Fuel Methods

        public void OnMovedTo()
        {
            RemoveFuel(1);
        }

        public void RemoveFuel(uint amount)
        {
            if (levelRequiresFuel.value)
            {
                // Make sure we don't go below 0 fuel otherwise we'll wrap around
                remainingFuel.value -= Math.Min(amount, remainingFuel.value);

                if (remainingFuel.value == 0)
                {
                    levelLoseOutOfFuel.Raise();
                }
            }
        }

        #endregion
    }
}
