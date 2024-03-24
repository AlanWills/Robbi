using System;
using System.Collections.Generic;
using UnityEngine;

namespace Celeste.Tips
{
    [CreateAssetMenu(fileName = nameof(TipsRecord), menuName = "Robbi/Tips/Tips Record")]
    public class TipsRecord : ScriptableObject
    {
        #region Properties and Fields

        public IReadOnlyList<string> AllTips => allTips;
        public IReadOnlyList<uint> UnseenIndexes => unseenIndexes;
        public IReadOnlyList<uint> SeenIndexes => seenIndexes;

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

                int chosenUnseenIndex = UnityEngine.Random.Range(0, unseenIndexes.Count);
                uint tipIndex = unseenIndexes[chosenUnseenIndex];
                unseenIndexes.RemoveAt(chosenUnseenIndex);
                seenIndexes.Add(tipIndex);
                onChanged?.Invoke();

                return allTips[(int)tipIndex];
            }
        }

        [Header("Data")]
        [SerializeField] private List<string> commonTips = new List<string>();
        [SerializeField] private List<string> desktopTips = new List<string>();
        [SerializeField] private List<string> mobileTips = new List<string>();
        [SerializeField] private List<string> html5Tips = new List<string>();

        [Header("Events")]
        [SerializeField] private Events.Event onChanged;

        [NonSerialized] private List<string> allTips = new List<string>();
        [NonSerialized] private List<uint> unseenIndexes = new List<uint>();
        [NonSerialized] private List<uint> seenIndexes = new List<uint>();

        #endregion

        #region Save/Load Methods

        public void Initialize()
        {
            Initialize(new List<uint>(), new List<uint>());
        }

        public void Initialize(IReadOnlyList<uint> unseenIndexes, IReadOnlyList<uint> seenIndexes)
        {
            SetUpTips();

            if (unseenIndexes.Count + seenIndexes.Count != allTips.Count)
            {
                // The number of total tips has changed (either we've added more in a new update or removed some)
                // Just wipe our indexes and start again
                this.seenIndexes.Clear();
                this.unseenIndexes.Clear();
                this.unseenIndexes.Capacity = allTips.Count;

                for (uint i = 0; i < (uint)allTips.Count; ++i)
                {
                    this.unseenIndexes.Add(i);
                }
            }
            else
            {
                this.unseenIndexes.AddRange(unseenIndexes);
                this.seenIndexes.AddRange(seenIndexes);
            }
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

        #endregion
    }
}
