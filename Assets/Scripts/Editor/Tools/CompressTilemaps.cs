using Robbi.Levels;
using RobbiEditor.Iterators;
using RobbiEditor.Levels;
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
            foreach (LevelFolder levelFolder in new LevelFolders())
            {
                GameObject levelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(levelFolder.PrefabPath);
                LevelRoot levelRoot = levelPrefab.GetComponent<LevelRoot>();
                levelRoot.EditorOnly_CompressAllTilemaps();
            }

            AssetDatabase.SaveAssets();
        }
    }
}
