using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Validation.Interfaces
{
    public static class Validate
    {
        public static void MenuItem<T>() where T : Object
        {
            bool result = true;
            HashSet<string> failedAssets = new HashSet<string>();
            List<string> allAssets = new List<string>();

            foreach (string assetGuid in AssetDatabase.FindAssets("t:" + typeof(T).Name))
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
                T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);

                if (asset == null)
                {
                    result = false;
                    Debug.LogAssertionFormat("Could not find asset with path {0}", assetPath);
                }

                allAssets.Add(asset.name);

                if (!Validator<T>.Validate(asset))
                {
                    failedAssets.Add(asset.name);
                    result = false;
                }
            }

            foreach (string assetName in allAssets)
            {
                if (!failedAssets.Contains(assetName))
                {
                    Debug.LogFormat("{0} passed validation", assetName);

                }
            }

            foreach (string failedAssetName in failedAssets)
            {
                Debug.LogAssertionFormat("{0} failed validation", failedAssetName);
            }

            if (Application.isBatchMode)
            {
                // 0 for success
                // 1 for fail
                EditorApplication.Exit(result ? 0 : 1);
            }
            else
            {
                EditorUtility.DisplayDialog("Validation Result", failedAssets.Count == 0 ? "All assets passed validation" : "Some assets failed validation", "OK");
            }
        }
    }
}
