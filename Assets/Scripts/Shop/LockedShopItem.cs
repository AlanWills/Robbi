using Celeste.Shop;
using Celeste.Shop.Catalogue;
using UnityEngine;

namespace Robbi.Shop
{
    [CreateAssetMenu(fileName = nameof(LockedShopItem), menuName = "Robbi/Shop/Locked Shop Item")]
    public class LockedShopItem : ShopItem
    {
        #region Properties and Fields

        public int UnlocksAtLevel => unlocksAtLevel;
        public int NumWaypointBoosters => GetBoosterRewardsOfType(BoosterRewardType.Waypoint);
        public int NumDoorToggleBoosters => GetBoosterRewardsOfType(BoosterRewardType.DoorToggle);
        public int NumInteractBoosters => GetBoosterRewardsOfType(BoosterRewardType.Interact);

        [SerializeField] private int unlocksAtLevel;

        #endregion

        private int GetBoosterRewardsOfType(BoosterRewardType boosterRewardType)
        {
            int numBoosters = 0;

            foreach (var rewardItem in Reward.Items)
            {
                if (rewardItem.isConditional && !rewardItem.shouldRewardCondition.IsMet)
                {
                    continue;
                }

                if (rewardItem.rewardItem is BoosterRewardItem boosterRewardItem &&
                    boosterRewardItem.BoosterRewardType == boosterRewardType)
                {
                    numBoosters += boosterRewardItem.Quantity;
                }
            }

            return numBoosters;
        }
    }
}