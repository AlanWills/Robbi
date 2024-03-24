using Celeste.Managers;
using Celeste.Parameters;
using Robbi.Runtime.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Runtime
{
    [AddComponentMenu("Robbi/Runtime/Fuel Manager")]
    public class FuelManager : NamedManager
    {
        #region Properties and Fields

        [SerializeField]
        private BoolValue levelRequiresFuel;

        [SerializeField]
        private UIntValue remainingFuel;

        #endregion

        public void Initialize() { }

        public void Cleanup() { }

        #region Fuel Utilities

        public void AddFuel(uint amount)
        {
            if (levelRequiresFuel.Value)
            {
                remainingFuel.Value += amount;
            }
        }

        public void RemoveFuel(uint amount)
        {
            if (levelRequiresFuel.Value)
            {
                // Make sure we don't go below 0 fuel otherwise we'll wrap around
                remainingFuel.Value -= Math.Min(amount, remainingFuel.Value);
            }
        }

        #endregion

        #region Callbacks

        public void OnCharacterMovedTo(CharacterRuntime characterRuntime)
        {
            RemoveFuel(1);
        }

        #endregion
    }
}
