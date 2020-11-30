using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.AssetUtils
{
#if UNITY_EDITOR
    public partial class EditorOnly
    {
        public static void AddObjectToAsset(Object objectToAdd, Object assetObject)
        {
            UnityEditor.AssetDatabase.AddObjectToAsset(objectToAdd, assetObject);
            UnityEditor.EditorUtility.SetDirty(assetObject);
        }

        public static void AddObjectToMainAsset(Object objectToAdd, Object assetObject)
        {
            string assetPath = UnityEditor.AssetDatabase.GetAssetPath(assetObject);
            UnityEditor.AssetDatabase.AddObjectToAsset(objectToAdd, assetPath);
            UnityEditor.EditorUtility.SetDirty(assetObject);
        }

        public static void RemoveObjectFromAsset(Object objectToRemove)
        {
            UnityEditor.AssetDatabase.RemoveObjectFromAsset(objectToRemove);
        }

        public static void SaveAndRefresh()
        {
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
        }
    }
#endif
}
