﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace CelesteEditor.Tools
{
    public static class AssetUtility
    {
        public static void AddObjectToAsset(Object objectToAdd, Object assetObject)
        {
            AssetDatabase.AddObjectToAsset(objectToAdd, assetObject);
            EditorUtility.SetDirty(assetObject);
        }

		public static void CreateAsset<T>(T asset, string path) where T : ScriptableObject
		{
			string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(string.Format("{0}/{1}.asset", path, asset.name));

			AssetDatabase.CreateAsset(asset, assetPathAndName);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

			EditorUtility.FocusProjectWindow();
			Selection.activeObject = asset;
		}

		public static void CreateFolder(string parent, string folderName)
        {
			parent = parent.EndsWith("/") ? parent.Substring(0, parent.Length - 1) : parent;
			folderName = folderName.EndsWith("/") ? folderName.Substring(0, folderName.Length - 1) : folderName;
			AssetDatabase.CreateFolder(parent, folderName);
        }

		public static void ApplyHideFlags(Object obj, HideFlags hideFlags)
        {
            foreach (Object o in AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(obj)))
            {
                if (o != null && !AssetDatabase.IsMainAsset(o))
                {
                    o.hideFlags = hideFlags;
                    EditorUtility.SetDirty(o);
                    EditorUtility.SetDirty(obj);
                }
            }

            AssetDatabase.SaveAssets();
        }

        public static string GetAssetFolderPath(Object obj)
        {
            string assetPath = AssetDatabase.GetAssetPath(obj);
            int indexOfSlash = assetPath.LastIndexOf('/');
            return indexOfSlash > 0 ? assetPath.Substring(0, indexOfSlash) : assetPath;
        }
	}
}