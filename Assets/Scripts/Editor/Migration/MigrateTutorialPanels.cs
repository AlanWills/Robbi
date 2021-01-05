using Celeste.Utils;
using Celeste.Viewport;
using Robbi.Levels;
using RobbiEditor.Iterators;
using RobbiEditor.Levels;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace RobbiEditor.Migration
{
    public static class MigrateTutorialPanels
    {
        [MenuItem("Robbi/Migration/Migrate Tutorial Panels")]
        public static void MenuItem()
        {
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/UI/BluePressed.png");
            if (sprite == null)
            {
                Debug.LogAssertion("Could not find sprite");
                return;
            }

            foreach (LevelFolder levelFolder in new LevelFolders())
            {
                if (!Directory.Exists(levelFolder.TutorialsFolderPath))
                {
                    continue;
                }

                GameObject tutorialsPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(levelFolder.TutorialsPrefabPath);
                for (int i = 0; i < tutorialsPrefab.transform.childCount; ++i)
                {
                    Transform child = tutorialsPrefab.transform.GetChild(i);
                    GameObject panel = GameObjectUtils.FindGameObjectRecursive(child, "Panel", FindConstraint.NoConstraint);
                    
                    if (panel != null)
                    {
                        Image image = panel.GetComponent<Image>();
                        if (image != null)
                        {
                            image.sprite = sprite;
                            image.color = Color.white;
                            image.type = Image.Type.Sliced;
                            image.pixelsPerUnitMultiplier = 1;
                            
                            EditorUtility.SetDirty(tutorialsPrefab);
                        }
                    }
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
