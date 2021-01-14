using CelesteEditor.Tools;
using Robbi.Levels.Elements;
using RobbiEditor.Constants;
using RobbiEditor.Iterators;
using RobbiEditor.Levels;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using static RobbiEditor.LevelDirectories;

namespace RobbiEditor.Tools
{
    public static class EnableTutorialSteps
    {
        [MenuItem("Robbi/Tools/Enable Tutorial Steps")]
        public static void MenuItem()
        {
            foreach (LevelFolder levelFolder in new LevelFolders())
            {
                if (!Directory.Exists(levelFolder.TutorialsFolderPath))
                {
                    continue;
                }

                GameObject gameObject = AssetDatabase.LoadAssetAtPath<GameObject>(levelFolder.TutorialsFolderPath);
                for (int i = 0; i < gameObject.transform.childCount; ++i)
                {
                    gameObject.transform.GetChild(i).gameObject.SetActive(true);
                }

                EditorUtility.SetDirty(gameObject);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
