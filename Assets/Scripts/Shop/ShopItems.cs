using PolyAndCode.UI;
using Celeste.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Robbi.Shop;

namespace Robbi.Shop
{
    [AddComponentMenu("Robbi/Shop/Shop Items")]
    public class ShopItems : MonoBehaviour, IRecyclableScrollRectDataSource
    {
        #region Properties and Fields

        [Header("UI")]
        [SerializeField]
        private RecyclableScrollRect scrollRect;

        [Header("Parameters")]
        [SerializeField]
        private UIntValue latestUnlockedLevel;

        private List<ShopItemData> shopItemData = new List<ShopItemData>();

        #endregion

        #region Unity Methods

        private void Awake()
        {
            foreach (ShopItem shopItem in ShopItemManager.Instance.ShopItems)
            {
                shopItemData.Add(new ShopItemData(shopItem, shopItem.UnlockAtLevel <= latestUnlockedLevel.Value));
            }

            scrollRect.DataSource = this;
        }

        #endregion

        #region Data Source Methods

        public int GetItemCount()
        {
            return shopItemData.Count;
        }

        public void SetCell(ICell cell, int index)
        {
            ShopItemController shopItemController = cell as ShopItemController;
            shopItemController.ConfigureCell(shopItemData[index], index);
        }

        #endregion
    }
}
