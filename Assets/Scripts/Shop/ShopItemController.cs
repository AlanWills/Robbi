using Celeste.Parameters;
using PolyAndCode.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Robbi.Shop
{
    [AddComponentMenu("Robbi/Shop/Shop Item Controller")]
    [RequireComponent(typeof(RectTransform))]
    public class ShopItemController : MonoBehaviour, ICell
    {
        #region Properties and Fields

        [Header("UI")]
        [SerializeField] private TextMeshProUGUI displayNameText;
        [SerializeField] private TextMeshProUGUI softCurrencyCostText;
        [SerializeField] private GameObject unlocksAtLevelBlocker;
        [SerializeField] private GameObject buttonDisabledBlocker;
        [SerializeField] private Button buyButton;
        [SerializeField] private TextMeshProUGUI unlockAtLevelText;

        [Header("Waypoint Booster")]
        [SerializeField] private GameObject waypointBoosterInfo;
        [SerializeField] private TextMeshProUGUI numWaypointBoostersText;

        [Header("Door Toggle Booster")]
        [SerializeField] private GameObject doorToggleBoosterInfo;
        [SerializeField] private TextMeshProUGUI numDoorToggleBoostersText;
        
        [Header("Interact Booster")]
        [SerializeField] private GameObject interactBoosterInfo;
        [SerializeField] private TextMeshProUGUI numInteractBoostersText;

        [Header("Parameters")]
        [SerializeField] private UIntValue numSoftCurrency;
        [SerializeField] private UIntValue numWaypointBoosters;
        [SerializeField] private UIntValue numDoorToggleBoosters;
        [SerializeField] private UIntValue numInteractBoosters;

        private ShopItemData shopItemData;

        #endregion

        #region Utility Methods

        public void ConfigureCell(ShopItemData shopItemData, int cellIndex)
        {
            this.shopItemData = shopItemData;

            ShopItem shopItem = shopItemData.shopItem;
            name = shopItem.name;
            displayNameText.text = shopItem.DisplayName;
            softCurrencyCostText.text = shopItem.SoftCurrencyCost.ToString();
            unlockAtLevelText.text = string.Format("Unlocks At Level {0}", shopItem.UnlockAtLevel);

            waypointBoosterInfo.SetActive(shopItem.NumWaypointBoosters > 0);
            numWaypointBoostersText.text = shopItem.NumWaypointBoosters.ToString();

            doorToggleBoosterInfo.SetActive(shopItem.NumDoorToggleBoosters > 0);
            numDoorToggleBoostersText.text = shopItem.NumDoorToggleBoosters.ToString();

            interactBoosterInfo.SetActive(shopItem.NumInteractBoosters > 0);
            numInteractBoostersText.text = shopItem.NumInteractBoosters.ToString();

            UpdateBlockerUI();
        }

        private void UpdateBlockerUI()
        {
            unlocksAtLevelBlocker.SetActive(!shopItemData.isUnlocked);
            buttonDisabledBlocker.SetActive(numSoftCurrency.Value < shopItemData.shopItem.SoftCurrencyCost);
            buyButton.interactable = numSoftCurrency.Value >= shopItemData.shopItem.SoftCurrencyCost;
        }

        #endregion

        #region Purchasing Methods

        public void Purchase()
        {
            ShopItem shopItem = shopItemData.shopItem;

            Debug.AssertFormat(numSoftCurrency.Value >= shopItem.SoftCurrencyCost, 
                "Current currency {0} less than shop item cost {1}", numSoftCurrency.Value, shopItem.SoftCurrencyCost);
            numSoftCurrency.Value = numSoftCurrency.Value < shopItem.SoftCurrencyCost ? 0 : numSoftCurrency.Value - shopItem.SoftCurrencyCost;
            numWaypointBoosters.Value += shopItem.NumWaypointBoosters;
            numDoorToggleBoosters.Value += shopItem.NumDoorToggleBoosters;
            numInteractBoosters.Value += shopItem.NumInteractBoosters;

            UpdateBlockerUI();
        }

        #endregion
    }
}
