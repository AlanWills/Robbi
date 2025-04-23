using Celeste.Parameters;
using Celeste.Shop;
using Celeste.Shop.Catalogue;
using Celeste.Shop.Purchasing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Robbi.Shop
{
    [AddComponentMenu("Robbi/Shop/Shop Item Controller")]
    [RequireComponent(typeof(RectTransform))]
    public class ShopItemController : MonoBehaviour, IShopItemUIController
    {
        #region Properties and Fields

        [SerializeField] private UIntValue currentLevel;
        [SerializeField] private ShopPurchaser shopPurchaser;

        [Header("UI")]
        [SerializeField] private TextMeshProUGUI displayNameText;
        [SerializeField] private TextMeshProUGUI softCurrencyCostText;
        [SerializeField] private GameObject unlocksAtLevelBlocker;
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
        [SerializeField] private IntValue numSoftCurrency;
        [SerializeField] private UIntValue numWaypointBoosters;
        [SerializeField] private UIntValue numDoorToggleBoosters;
        [SerializeField] private UIntValue numInteractBoosters;

        private LockedShopItem shopItem;

        #endregion

        #region Utility Methods

        public void Hookup(ShopItemLayout shopItemLayout)
        {
            shopItem = shopItemLayout.shopItem as LockedShopItem;

            name = shopItem.name;
            displayNameText.text = shopItem.Title.Fallback;

            RefreshPrice();

            waypointBoosterInfo.SetActive(shopItem.NumWaypointBoosters > 0);
            numWaypointBoostersText.text = shopItem.NumWaypointBoosters.ToString();

            doorToggleBoosterInfo.SetActive(shopItem.NumDoorToggleBoosters > 0);
            numDoorToggleBoostersText.text = shopItem.NumDoorToggleBoosters.ToString();

            interactBoosterInfo.SetActive(shopItem.NumInteractBoosters > 0);
            numInteractBoostersText.text = shopItem.NumInteractBoosters.ToString();

            UpdateBlockerUI();
        }

        public void RefreshPrice()
        {
            softCurrencyCostText.text = $"<sprite name=\"SoftCurrencyIcon\"/> {shopItem.Cost.quantity}";
            unlockAtLevelText.text = $"Unlocks At Level {shopItem.UnlocksAtLevel}";}

        private void UpdateBlockerUI()
        {
            bool isUnlocked = shopItem.UnlocksAtLevel <= currentLevel.Value;

            unlocksAtLevelBlocker.SetActive(!isUnlocked);
            buyButton.interactable = numSoftCurrency.Value >= shopItem.Cost.quantity;
        }

        #endregion

        #region Purchasing Methods

        public void Purchase()
        {
            Debug.Assert(numSoftCurrency.Value >= shopItem.Cost.quantity, $"Current currency {numSoftCurrency.Value} less than shop item cost {shopItem.Cost.quantity}.");
            shopPurchaser.Purchase(shopItem);

            UpdateBlockerUI();
        }

        #endregion
    }
}
