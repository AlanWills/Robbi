using Robbi.Levels.Effects;
using System.Collections.Generic;
using UnityEngine;

namespace Robbi.Levels.Elements
{
    [CreateAssetMenu(fileName = "Collectable", menuName = "Robbi/Collectables/Collectable")]
    public class Collectable : ScriptableObject, ILevelElement
    {
        #region Properties and Fields

        public int NumPickupEffects
        {
            get { return pickupEffects.Count; }
        }

        [SerializeField]
        private Vector3Int position;
        public Vector3Int Position
        {
            get { return position; }
        }

        [SerializeField]
        private List<PickupEffect> pickupEffects = new List<PickupEffect>();

        #endregion

        #region Pickup Effect Methods

        public PickupEffect GetPickupEffect(int index)
        {
            return 0 <= index && index < NumPickupEffects ? pickupEffects[index] : null;
        }

        public T AddPickupEffect<T>() where T : PickupEffect
        {
            return AddPickupEffect(typeof(T)) as T;
        }

        public PickupEffect AddPickupEffect(System.Type modifierType)
        {
            PickupEffect modifier = ScriptableObject.CreateInstance(modifierType) as PickupEffect;
            modifier.name = modifierType.Name;
            modifier.hideFlags = HideFlags.HideInHierarchy;
            pickupEffects.Add(modifier);

            Celeste.Tools.EditorOnly.AddObjectToMainAsset(modifier, this);

            return modifier;
        }

        public void RemovePickupEffect(int index)
        {
            if (0 <= index && index < NumPickupEffects)
            {
                Celeste.Tools.EditorOnly.RemoveObjectFromAsset(pickupEffects[index], true);
                pickupEffects.RemoveAt(index);
            }
        }

        #endregion

        #region Pickup Methods

        public void Pickup(PickupArgs pickupArgs)
        {
            foreach (PickupEffect effect in pickupEffects)
            {
                effect.Execute(pickupArgs);
            }
        }

        #endregion
    }
}
