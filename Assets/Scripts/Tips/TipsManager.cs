using Celeste.Assets;
using Celeste.Managers;
using Celeste.Managers.DTOs;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Celeste.Tips
{
    [CreateAssetMenu(menuName = "Robbi/Tips/Tips Manager")]
    public class TipsManager : PersistentManager<TipsManager, TipsManagerDTO>
    {
        #region Properties and Fields

        private const string ADDRESS = "Assets/Tips/TipsManager.asset";

        public static string DefaultSavePath
        {
            get { return Path.Combine(UnityEngine.Application.persistentDataPath, "Tips.dat"); }
        }

        public IEnumerable<uint> UnseenIndexes
        {
            get { return unseenIndexes; }
        }

        public IEnumerable<uint> SeenIndexes
        {
            get { return seenIndexes; }
        }

        public string RandomTip
        {
            get
            {
                if (allTips.Count == 0)
                {
                    UnityEngine.Debug.LogAssertion("No tips found in TipsManager");
                    return "";
                }

                if (unseenIndexes.Count == 0)
                {
                    List<uint> temp = unseenIndexes;
                    unseenIndexes = seenIndexes;
                    seenIndexes = temp;
                }

                int chosenUnseenIndex = Random.Range(0, unseenIndexes.Count);
                uint tipIndex = unseenIndexes[chosenUnseenIndex];
                unseenIndexes.RemoveAt(chosenUnseenIndex);
                seenIndexes.Add(tipIndex);

                return allTips[(int)tipIndex];
            }
        }

        [SerializeField]
        private List<string> commonTips = new List<string>();

        [SerializeField]
        private List<string> desktopTips = new List<string>();

        [SerializeField]
        private List<string> mobileTips = new List<string>();

        [SerializeField]
        private List<string> html5Tips = new List<string>();

        private List<string> allTips = new List<string>();
        private List<uint> unseenIndexes = new List<uint>();
        private List<uint> seenIndexes = new List<uint>();

        #endregion

        private TipsManager() { }

        #region Save/Load Methods

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

        protected override TipsManagerDTO Serialize()
        {
            UnityEngine.Debug.AssertFormat(allTips.Count == (unseenIndexes.Count + seenIndexes.Count), 
                "Tip index mismatch.  All: {0}.  Unseen: {1}.  Seen: {2}", allTips.Count, unseenIndexes.Count, seenIndexes.Count);
            return new TipsManagerDTO(this);
        }

        protected override void Deserialize(TipsManagerDTO tipsManagerDTO)
        {
            SetUpTips();

            if ((tipsManagerDTO.unseenIndexes.Count + tipsManagerDTO.seenIndexes.Count) != allTips.Count)
            {
                // The number of total tips has changed (either we've added more in a new update or removed some)
                // Just wipe our indexes and start again
                ResetIndexes();
            }
            else
            {
                unseenIndexes = tipsManagerDTO.unseenIndexes;
                seenIndexes = tipsManagerDTO.seenIndexes;
            }
        }

        protected override void SetDefaultValues()
        {
            SetUpTips();
            ResetIndexes();
        }

        #endregion

        #region Utility Functions

        private void SetUpTips()
        {
            allTips.Clear();
            allTips.AddRange(commonTips);
#if UNITY_ANDROID || UNITY_IOS
            allTips.AddRange(mobileTips);
#elif UNITY_STANDALONE
            allTips.AddRange(desktopTips);
#elif UNITY_WEBGL
            allTips.AddRange(html5Tips);
#endif
        }

        private void ResetIndexes()
        {
            seenIndexes.Clear();
            unseenIndexes.Clear();
            unseenIndexes.Capacity = allTips.Count;

            for (uint i = 0; i < (uint)allTips.Count; ++i)
            {
                unseenIndexes.Add(i);
            }
        }

        #endregion
    }

    [System.Serializable]
    public class TipsManagerDTO : IPersistentManagerDTO<TipsManager, TipsManagerDTO>
    {
        public List<uint> unseenIndexes;
        public List<uint> seenIndexes;

        public TipsManagerDTO(TipsManager tipsManager)
        {
            unseenIndexes = new List<uint>(tipsManager.UnseenIndexes);
            seenIndexes = new List<uint>(tipsManager.SeenIndexes);
        }
    }
}
