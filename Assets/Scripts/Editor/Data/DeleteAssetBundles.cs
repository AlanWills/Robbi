using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Data
{
    public static class DeleteAssetBundles
    {
        [MenuItem("Robbi/Data/Delete Asset Bundles")]
        public static void Execute()
        {
            if (Caching.ClearCache())
            {
                Debug.Log("Cached cleared successfully");
            }
            else
            {
                Debug.LogWarning("Clear Cache did not succeed.");
            }
        }
    }
}
