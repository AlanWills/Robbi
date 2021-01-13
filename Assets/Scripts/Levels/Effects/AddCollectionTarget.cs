using Celeste.Events;
using Robbi.Collecting;
using Robbi.Events.Levels.Elements;
using Robbi.Levels.Effects;
using Robbi.Levels.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robbi.Levels.Effects
{
    public class AddCollectionTarget : PickupEffect
    {
        public CollectionTarget collectionTarget;

        public override void Execute(PickupArgs pickupArgs)
        {
            collectionTarget.Add(1);
        }
    }
}
