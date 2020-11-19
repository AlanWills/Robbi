using Robbi.Debugging.Logging;
using Robbi.Managers;
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
    public class LevelManager : PersistentManager<LevelManager>
    {
        #region Properties and Fields

        private const string ADDRESS = "Assets/Levels/LevelManager.asset";

        public static string DefaultSavePath
        {
            get { return Path.Combine(Application.persistentDataPath, "LevelManager.json"); }
        }

        public uint CurrentLevelIndex 
        { 
            get { return currentLevelIndex.value; }
            set 
            { 
                currentLevelIndex.value = value;

#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(currentLevelIndex);
#endif
            }
        }

        public uint LatestLevelIndex
        {
            get { return latestLevelIndex.value; }
            set
            { 
                latestLevelIndex.value = value;

#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(latestLevelIndex);
#endif
            }
        }

        [SerializeField]
        private UIntValue currentLevelIndex;

        [SerializeField]
        private UIntValue latestLevelIndex;

#endregion

        private LevelManager() { }

#region Save/Load Methods

#region Editor Only

#if UNITY_EDITOR

        public static LevelManager EditorOnly_Load()
        {
            return EditorOnly_Load(ADDRESS);
        }

#endif

#endregion

        public static AsyncOperationHandle Load()
        {
            return Load(ADDRESS, DefaultSavePath);
        }

        public void Save()
        {
            Save(DefaultSavePath);
        }

        protected override string Serialize()
        {
            return JsonUtility.ToJson(new LevelManagerDTO(this));
        }

        protected override void Deserialize(string fileContents)
        {
            LevelManagerDTO levelManagerDTO = JsonUtility.FromJson<LevelManagerDTO>(fileContents);

            CurrentLevelIndex = levelManagerDTO.currentLevelIndex;

            HudLogger.LogInfoFormat("Current Level Index: {0}", CurrentLevelIndex);
        }

#endregion
    }

    [Serializable]
    public class LevelManagerDTO
    {
        public uint currentLevelIndex = 0;

        public LevelManagerDTO() { }

        public LevelManagerDTO(LevelManager levelManager)
        {
            currentLevelIndex = levelManager.CurrentLevelIndex;
        }
    }
}
