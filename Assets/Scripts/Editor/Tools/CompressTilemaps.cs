using Robbi.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Tools
{
    public static class CompressTilemaps
    {
        [MenuItem("Robbi/Tools/Compress Tilemaps")]
        public static void MenuItem()
        {
            int i = 0;
            string levelFolderPath = string.Format("Assets/Levels/Level{0}/Level{0}.prefab", i);
            GameObject levelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(levelFolderPath);

            while (levelPrefab != null)
            {
                LevelRoot levelRoot = levelPrefab.GetComponent<LevelRoot>();
                levelRoot.EditorOnly_CompressAllTilemaps();

                EditorUtility.SetDirty(levelRoot);

                ++i;
                levelFolderPath = string.Format("Assets/Levels/Level{0}/Level{0}.prefab", i);
                levelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(levelFolderPath);
            }

            AssetDatabase.SaveAssets();
        }
    }
}
