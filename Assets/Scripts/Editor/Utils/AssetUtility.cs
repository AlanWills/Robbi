using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RobbiEditor.Utils
{
    public static class AssetUtility
    {
        public static void AddObjectToAsset(Object objectToAdd, Object assetObject)
        {
            UnityEditor.AssetDatabase.AddObjectToAsset(objectToAdd, assetObject);
            UnityEditor.EditorUtility.SetDirty(assetObject);
        }
    }
}
