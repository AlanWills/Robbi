using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robbi.Shop
{
    public class ShopItemData
    {
        public ShopItem shopItem;
        public bool isUnlocked;

        public ShopItemData(ShopItem shopItem, bool isUnlocked)
        {
            this.shopItem = shopItem;
            this.isUnlocked = isUnlocked;
        }
    }
}
