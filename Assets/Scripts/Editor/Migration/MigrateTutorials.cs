using Celeste.Tilemaps;
using Robbi.Levels;
using RobbiEditor.Iterators;
using RobbiEditor.Levels;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace RobbiEditor.Migration
{
    public static class MigrateTutorials
    {
        [MenuItem("Robbi/Migration/Migrate Tutorials")]
        public static void MenuItem()
        {
            foreach (LevelFolder levelFolder in new LevelFolders())
            {
                if (levelFolder.Index >= 40)
                {
                    continue;
                }

                GameObject levelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(levelFolder.PrefabPath);
                GameObject instantiatedLevelPrefab = PrefabUtility.InstantiatePrefab(levelPrefab) as GameObject;
                GameObject tutorialPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(levelFolder.TutorialsPrefabPath);
                GameObject tutorialInstance = PrefabUtility.InstantiatePrefab(tutorialPrefab, instantiatedLevelPrefab.transform) as GameObject;
                tutorialInstance.name = tutorialPrefab.name;

                PrefabUtility.ApplyAddedGameObject(tutorialInstance, levelFolder.PrefabPath, InteractionMode.AutomatedAction);
                GameObject.DestroyImmediate(instantiatedLevelPrefab);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
