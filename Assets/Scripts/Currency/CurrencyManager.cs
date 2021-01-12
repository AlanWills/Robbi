using Celeste.Assets;
using Celeste.Managers;
using Celeste.Parameters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Robbi.Currency
{
    [CreateAssetMenu(fileName = "CurrencyManager", menuName = "Robbi/Currency/Currency Manager")]
    public class CurrencyManager : PersistentManager<CurrencyManager, CurrencyManagerDTO>
    {
        #region Properties and Fields
        
        private const string ADDRESS = "Assets/Currency/CurrencyManager.asset";

        public static string DefaultSavePath
        {
            get { return Path.Combine(Application.persistentDataPath, "Currency.json"); }
        }

        public uint SoftCurrency
        {
            get { return softCurrency.Value; }
        }

        [SerializeField]
        private UIntValue softCurrency;

        #endregion

        #region Save/Load

        public static AsyncOperationHandleWrapper LoadAsync()
        {
            return LoadAsyncImpl(ADDRESS, DefaultSavePath);
        }

        public static void Reset()
        {
            if (File.Exists(DefaultSavePath))
            {
                File.Delete(DefaultSavePath);
            }
        }

        public void Save()
        {
            Save(DefaultSavePath);
        }

        public void SaveAsync()
        {
            SaveAsync(DefaultSavePath);
        }

        protected override void Deserialize(CurrencyManagerDTO dto)
        {
            softCurrency.Value = dto.softCurrency;
        }

        protected override string Serialize()
        {
            return JsonUtility.ToJson(new CurrencyManagerDTO(this));
        }

        protected override void SetDefaultValues() { }

        #endregion
    }

    [Serializable]
    public struct CurrencyManagerDTO
    {
        public uint softCurrency;

        public CurrencyManagerDTO(CurrencyManager currencyManager)
        {
            softCurrency = currencyManager.SoftCurrency;
        }
    }
}
