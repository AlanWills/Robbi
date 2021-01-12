using Celeste.Assets;
using Celeste.FSM.Nodes.Objects;
using Celeste.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Robbi.Shop
{
    [CreateAssetMenu(fileName = "ShopItemManager", menuName = "Robbi/Shop/Shop Item Manager")]
    public class ShopItemManager : Manager<ShopItemManager>
    {
        #region Properties and Fields

        public const string ADDRESS = "Assets/Shop/ShopItemManager.asset";

        public IEnumerable<ShopItem> ShopItems
        {
            get { return shopItems; }
        }

        [SerializeField]
        private List<ShopItem> shopItems = new List<ShopItem>();

        #endregion

        private ShopItemManager() { }

        #region Save/Load

        public static AsyncOperationHandleWrapper LoadAsync()
        {
            return LoadAsyncImpl(ADDRESS);
        }

        // Used by FSMs
        public static void LoadAsync(string addressablePath, AsyncOperationHandleWrapper wrapper)
        {
            LoadAsyncImpl(addressablePath, wrapper);
        }

        #endregion
    }
}
