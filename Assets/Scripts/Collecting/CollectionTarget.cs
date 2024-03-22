using Celeste.Parameters;
using UnityEngine;

namespace Robbi.Collecting
{
    [CreateAssetMenu(fileName = "CollectionTarget", menuName = "Robbi/Collecting/Collection Target")]
    public class CollectionTarget : ScriptableObject
    {
        #region Properties and Fields

        public Sprite sprite;

        private UIntValue collectedAmount;

        #endregion

        public void Initialize(UIntValue collectedAmount)
        {
            this.collectedAmount = collectedAmount;
            this.collectedAmount.Value = 0;
        }

        #region Quantity Modification

        public void Add(uint amount)
        {
            collectedAmount.Value += amount;
        }

        #endregion
    }
}
