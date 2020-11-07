using Robbi.Managers;
using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Robbi.Settings
{
    [CreateAssetMenu(menuName = "Robbi/Settings/Settings Manager")]
    public class SettingsManager : PersistentManager<SettingsManager>
    {
        #region Properties and Fields

        private const string ADDRESS = "Assets/Settings/SettingsManager.asset";

        public bool MusicEnabled
        {
            get { return musicEnabled.value; }
            set { musicEnabled.value = value; }
        }

        public bool SfxEnabled
        {
            get { return sfxEnabled.value; }
            set { sfxEnabled.value = value; }
        }

        [SerializeField]
        private BoolValue musicEnabled;

        [SerializeField]
        private BoolValue sfxEnabled;

        #endregion

        private SettingsManager() { }

        #region Save/Load Methods

        public static AsyncOperationHandle Load()
        {
            return Load(ADDRESS);
        }

        #endregion
    }
}
