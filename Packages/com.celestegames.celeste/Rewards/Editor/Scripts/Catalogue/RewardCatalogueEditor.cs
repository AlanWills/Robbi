using Celeste.Rewards.Catalogue;
using Celeste.Rewards.Objects;
using CelesteEditor.DataStructures;
using UnityEditor;

namespace CelesteEditor.Rewards.Catalogue
{
    [CustomEditor(typeof(RewardCatalogue))]
    public class RewardCatalogueEditor : IIndexableItemsEditor<Reward>
    {
    }
}
