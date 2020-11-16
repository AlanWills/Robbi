using Robbi.Levels;
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
    public static class FindInteractables
    {
        [MenuItem("Robbi/Tools/Find Interactables")]
        public static void MenuItem()
        {
            int i = 0;
            string levelFolderPath = string.Format("Assets/Levels/Level{0}", i);

            while (Directory.Exists(levelFolderPath))
            {
                Level level = AssetDatabase.LoadAssetAtPath<Level>(string.Format("{0}/Level{1}Data.asset", levelFolderPath, i));
                LevelEditor.FindInteractables(level);

                ++i;
                levelFolderPath = string.Format("Assets/Levels/Level{0}", i);
            }

            AssetDatabase.SaveAssets();
        }
    }
}
