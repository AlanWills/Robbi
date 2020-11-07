using Robbi.Debugging.Logging;
using Robbi.Managers;
using Robbi.Parameters;
using Robbi.Save;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Robbi.Levels
{
    [CreateAssetMenu(menuName = "Robbi/Levels/Level Manager")]
    public class LevelManager : PersistentManager<LevelManager>
    {
        #region Properties and Fields

        private const string ADDRESS = "Assets/Levels/LevelManager.asset";

        public uint CurrentLevelIndex 
        { 
            get { return currentLevelIndex.value; }
            set { currentLevelIndex.value = value; }
        }

        public uint LatestLevelIndex
        {
            get { return latestLevelIndex.value; }
        }

        [SerializeField]
        private UIntValue currentLevelIndex;

        [SerializeField]
        private UIntValue latestLevelIndex;

        #endregion

        private LevelManager() { }

        #region Save/Load Methods

        public static AsyncOperationHandle Load()
        {
            return Load(ADDRESS);
        }

        public override void Serialize(SaveData saveData)
        {
            saveData.currentLevel = CurrentLevelIndex;
        }

        public override void Deserialize(SaveData saveData)
        {
            CurrentLevelIndex = saveData.currentLevel;
        }

        #endregion
    }
}
