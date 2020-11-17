using Robbi.Levels;
using Robbi.Levels.Elements;
using Robbi.Parameters;
using RobbiEditor.Constants;
using RobbiEditor.Levels;
using RobbiEditor.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using static RobbiEditor.LevelDirectories;

namespace RobbiEditor.Migration
{
    public static class MigrateLevels
    {
        [MenuItem("Robbi/Migration/Migrate Levels")]
        public static void MenuItem()
        {
            int i = 0;
            string levelFolderPath = string.Format("{0}Level{1}/", LEVELS_PATH, i);

            while (Directory.Exists(levelFolderPath))
            {
                Level level = AssetDatabase.LoadAssetAtPath<Level>(string.Format("{0}Level{1}Data.asset", levelFolderPath, i));
                level.levelTutorial = AssetDatabase.LoadAssetAtPath<GameObject>(string.Format("{0}{1}Level{2}Tutorials.prefab", levelFolderPath, TUTORIALS_NAME, i));
                LevelEditor.FindInteractables(level);

                ++i;
                levelFolderPath = string.Format("{0}Level{1}/", LEVELS_PATH, i);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
