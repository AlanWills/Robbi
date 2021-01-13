using Celeste.Assets;
using Celeste.Managers;
using Celeste.Parameters;
using Celeste.Parameters.Rendering;
using Robbi.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Collecting
{
    [Serializable]
    public struct CollectionTargetData
    {
        public UIntValue collectedAmount;
        public UIntValue requiredAmount;
        public SpriteValue sprite;
    }

    [CreateAssetMenu(fileName = "CollectionTargetManager", menuName = "Robbi/Collecting/Collection Target Manager")]
    public class CollectionTargetManager : Manager<CollectionTargetManager>
    {
        #region Properties and Fields

        public const string ADDRESS = "Assets/Collecting/CollectionTargetManager.asset";

        public bool CollectionTargetsMet
        {
            get
            {
                foreach (CollectionTargetData data in collectionTargetData)
                {
                    if (data.collectedAmount.Value < data.requiredAmount.Value)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        [SerializeField] private List<CollectionTargetData> collectionTargetData = new List<CollectionTargetData>();

        # endregion

        private CollectionTargetManager() { }

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

        public void Initialize(IEnumerable<LevelCollectionTarget> levelCollectionTargets)
        {
            // Give each collection target a parameter to maintain
            // This allows us to keep collection targets generic
            // We can maintain a fixed number in the UI, but have as many we like to choose from when making the level
            int i = 0;
            foreach (LevelCollectionTarget levelCollectionTarget in levelCollectionTargets)
            {
                levelCollectionTarget.collectionTarget.Initialize(collectionTargetData[i].collectedAmount);
                collectionTargetData[i].requiredAmount.Value = levelCollectionTarget.target;
                collectionTargetData[i].sprite.Value = levelCollectionTarget.collectionTarget.sprite;
                ++i;
            }

            // Set all the other unused parameters to 0
            for (int index = i; index < collectionTargetData.Count; ++index)
            {
                collectionTargetData[index].collectedAmount.Value = 0;
                collectionTargetData[index].requiredAmount.Value = 0;
                collectionTargetData[index].sprite.Value = null;
            }
        }
    }
}
