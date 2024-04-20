using Celeste.Rewards.Objects;
using Robbi.Boosters;
using UnityEngine;

namespace Robbi.Shop
{
    public enum BoosterRewardType
    {
        Waypoint,
        DoorToggle,
        Interact
    }

    [CreateAssetMenu(fileName = nameof(BoosterRewardItem), menuName = "Robbi/Shop/Booster Reward Item")]
    public class BoosterRewardItem : RewardItem
    {
        #region Properties and Fields

        public override Sprite Icon => icon;
        public BoosterRewardType BoosterRewardType => boosterRewardType;

        [SerializeField] private Sprite icon;
        [SerializeField] private BoosterRewardType boosterRewardType;
        [SerializeField] private BoostersRecord boosterRecord;

        #endregion

        public override void AwardReward(int multiplier)
        {
            switch (boosterRewardType)
            {
                case BoosterRewardType.Waypoint:
                    boosterRecord.NumWaypointBoosters += (uint)(multiplier * Quantity);
                    break;

                case BoosterRewardType.DoorToggle:
                    boosterRecord.NumDoorToggleBoosters += (uint)(multiplier * Quantity);
                    break;

                case BoosterRewardType.Interact:
                    boosterRecord.NumInteractBoosters += (uint)(multiplier * Quantity);
                    break;
            }
        }
    }
}