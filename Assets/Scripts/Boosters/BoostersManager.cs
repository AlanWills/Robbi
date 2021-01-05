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

namespace Robbi.Boosters
{
    [CreateAssetMenu(menuName = "Robbi/Boosters/Boosters Manager")]
    public class BoostersManager : PersistentManager<BoostersManager, BoostersManagerDTO>
    {
        #region Properties and Fields

        private const string ADDRESS = "Assets/Boosters/BoostersManager.asset";

        public static string DefaultSavePath
        {
            get { return Path.Combine(UnityEngine.Application.persistentDataPath, "BoostersManager.json"); }
        }

        public uint NumWaypointBoosters
        {
            get { return numWaypointBoosters.Value; }
            set { numWaypointBoosters.Value = value; }
        }

        [SerializeField]
        private UIntValue numWaypointBoosters;

        [SerializeField]
        private UIntValue waypointBoosterUnlockLevel;

        #endregion

        private BoostersManager() { }

        #region Save/Load Methods

        public static AsyncOperationHandle Load()
        {
            return Load(ADDRESS, DefaultSavePath);
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

        protected override string Serialize()
        {
            return JsonUtility.ToJson(new BoostersManagerDTO(this));
        }

        protected override void Deserialize(BoostersManagerDTO optionsManagerDTO)
        {
            NumWaypointBoosters = optionsManagerDTO.numWaypointBoosters;
        }

        #endregion
    }

    [Serializable]
    public class BoostersManagerDTO
    {
        public uint numWaypointBoosters;

        public BoostersManagerDTO() { }

        public BoostersManagerDTO(BoostersManager boostersManager)
        {
            numWaypointBoosters = boostersManager.NumWaypointBoosters;
        }
    }
}
