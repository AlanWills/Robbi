using UnityEngine;

namespace Robbi.Shop
{
    [CreateAssetMenu(fileName = "ShopItem", menuName = "Robbi/Shop/Shop Item")]
    public class ShopItem : ScriptableObject
    {
        #region Properties and Fields

        [SerializeField] private string displayName;
        public string DisplayName
        {
            get { return displayName; }
        }

        [SerializeField] private Sprite shopImage;
        public Sprite ShopImage
        {
            get { return shopImage; }
        }

        [SerializeField] private uint softCurrencyCost;
        public uint SoftCurrencyCost
        {
            get { return softCurrencyCost; }
        }

        [SerializeField] private uint unlockAtLevel;
        public uint UnlockAtLevel
        {
            get { return unlockAtLevel; }
        }

        [SerializeField] private uint numWaypointBoosters;
        public uint NumWaypointBoosters
        {
            get { return numWaypointBoosters; }
        }

        [SerializeField] private uint numDoorToggleBoosters;
        public uint NumDoorToggleBoosters
        {
            get { return numDoorToggleBoosters; }
        }

        [SerializeField] private uint numInteractBoosters;
        public uint NumInteractBoosters
        {
            get { return numInteractBoosters; }
        }

        #endregion
    }
}