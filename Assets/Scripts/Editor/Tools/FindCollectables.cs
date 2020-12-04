using Robbi.Levels;
using RobbiEditor.Iterators;
using RobbiEditor.Levels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Tools
{
    public static class FindCollectables
    {
        [MenuItem("Robbi/Tools/Find Collectables")]
        public static void MenuItem()
        {
            foreach (LevelFolder levelFolder in new LevelFolders())
            {
                Level level = AssetDatabase.LoadAssetAtPath<Level>(levelFolder.LevelDataPath);
                LevelEditor.FindCollectables(level);
            }

            AssetDatabase.SaveAssets();
        }
    }
}
