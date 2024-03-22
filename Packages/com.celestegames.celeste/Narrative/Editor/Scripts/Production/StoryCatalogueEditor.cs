using Celeste.Narrative;
using CelesteEditor.DataStructures;
using UnityEditor;

namespace CelesteEditor.Narrative
{
    [CustomEditor(typeof(StoryCatalogue))]
    public class StoryCatalogueEditor : IIndexableItemsEditor<Story>
    {
    }
}