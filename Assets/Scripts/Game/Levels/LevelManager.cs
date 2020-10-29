using Robbi.Debugging.Logging;
using Robbi.Parameters;
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
    public class LevelManager : ScriptableObject
    {
        #region Properties and Fields

        private const string ADDRESS = "Assets/Levels/LevelManager.asset";

        private static LevelManager instance;
        public static LevelManager Instance
        {
            get 
            {
                Debug.Assert(instance != null, "LevelManager is null.  Did you forget to wait for Load()");
                return instance;
            }
            private set { instance = value; }
        }

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

        public static AsyncOperationHandle<LevelManager> Load()
        {
            return Load(ADDRESS);
        }

        public static AsyncOperationHandle<LevelManager> Load(string filePath)
        {
            AsyncOperationHandle<LevelManager> asyncOperationHandle = Addressables.LoadAssetAsync<LevelManager>(filePath);
            asyncOperationHandle.Completed += Load_Completed;

            return asyncOperationHandle;
        }

        private static void Load_Completed(AsyncOperationHandle<LevelManager> obj)
        {
            if (obj.IsValid() && obj.Result != null)
            {
                Instance = obj.Result;
            }
        }

        #endregion
    }
}
