using PolyAndCode.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        [SerializeField] private TextMeshProUGUI displayNameText;
        [SerializeField] private Image shopImage;
        [SerializeField] private TextMeshProUGUI softCurrencyCostText;
        [SerializeField] private GameObject unlocksAtLevelBlocker;
        [SerializeField] private TextMeshProUGUI unlockAtLevelText;
        [SerializeField] private TextMeshProUGUI numWaypointBoostersText;
        [SerializeField] private TextMeshProUGUI numDoorToggleBoostersText;
        [SerializeField] private TextMeshProUGUI numInteractBoostersText;

        #endregion

        #region Utility Methods

        public void ConfigureCell(ShopItemData shopItemData, int cellIndex)
        {
            ShopItem shopItem = shopItemData.shopItem;
            displayNameText.text = shopItem.DisplayName;
            shopImage.sprite = shopItem.ShopImage;
            softCurrencyCostText.text = string.Format("Buy {0}", shopItem.SoftCurrencyCost);
            unlocksAtLevelBlocker.SetActive(!shopItemData.isUnlocked);
            unlockAtLevelText.text = string.Format("Unlocks At Level {0}", shopItem.UnlockAtLevel);
            numWaypointBoostersText.text = shopItem.NumWaypointBoosters.ToString();
            numDoorToggleBoostersText.text = shopItem.NumDoorToggleBoosters.ToString();
            numInteractBoostersText.text = shopItem.NumInteractBoosters.ToString();
        }

        #endregion
    }
}
