using Celeste.Parameters;
using Celeste.Utils;
using Celeste.Viewport;
using Robbi.Levels;
using Robbi.Tutorials;
using RobbiEditor.Iterators;
using RobbiEditor.Levels;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace RobbiEditor.Migration
{
    public static class MigrateTutorials
    {
        [MenuItem("Robbi/Migration/Migrate Tutorials")]
        public static void MenuItem()
        {
            foreach (LevelFolder levelFolder in new LevelFolders())
            {
                if (!Directory.Exists(levelFolder.TutorialsFolderPath))
                {
                    continue;
                }

                GameObject tutorialsPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(levelFolder.TutorialsPrefabPath);
                Tutorial tutorial = tutorialsPrefab.AddComponent<Tutorial>();
                tutorial.latestUnlockedLevel = AssetDatabase.LoadAssetAtPath<UIntValue>(ParameterFiles.LATEST_UNLOCKED_LEVEL);
                tutorial.currentLevel = AssetDatabase.LoadAssetAtPath<UIntValue>(ParameterFiles.CURRENT_LEVEL);

                EditorUtility.SetDirty(tutorialsPrefab);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
