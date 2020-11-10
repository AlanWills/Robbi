using Robbi.Levels.Elements;
using RobbiEditor.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace RobbiEditor.Levels.Migration
{
    public static class MigrateMarkerColours
    {
        [MenuItem("Robbi/Migration/Migrate Marker Colours")]
        public static void MenuItem()
        {
            int i = 0;
            string levelFolderPath = string.Format("Assets/Levels/Level{0}/Level{0}.prefab", i);
            GameObject levelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(levelFolderPath);

            while (levelPrefab != null)
            {
                Transform interactables = levelPrefab.transform.Find("Interactables");
                for (int markerIndex = 0; markerIndex < interactables.childCount; ++markerIndex)
                {
                    Transform markerBackground = interactables.GetChild(markerIndex);
                    SpriteRenderer markerBackgroundRenderer = markerBackground.GetComponent<SpriteRenderer>();
                    markerBackgroundRenderer.color = new Color(0, 0, 0);

                    EditorUtility.SetDirty(markerBackgroundRenderer);
                    EditorUtility.SetDirty(markerBackground.gameObject);

                    Transform markerIcon = markerBackground.GetChild(0);
                    SpriteRenderer markerIconRenderer = markerIcon.GetComponent<SpriteRenderer>();

                    if (markerBackground.name.Contains("Green"))
                    {
                        markerIconRenderer.color = DoorColours.GREEN;
                    }
                    else if (markerBackground.name.Contains("Red"))
                    {
                        markerIconRenderer.color = DoorColours.RED;
                    }
                    else if (markerBackground.name.Contains("Blue"))
                    {
                        markerIconRenderer.color = DoorColours.BLUE;
                    }
                    else if (markerBackground.name.Contains("Grey"))
                    {
                        markerIconRenderer.color = DoorColours.GREY;
                    }
                    else Debug.LogAssertionFormat("Could not find colour for marker {0}", markerBackground.name);

                    EditorUtility.SetDirty(markerIconRenderer);
                    EditorUtility.SetDirty(markerIcon.gameObject);
                }

                EditorUtility.SetDirty(levelPrefab);

                ++i;
                levelFolderPath = string.Format("Assets/Levels/Level{0}/Level{0}.prefab", i);
                levelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(levelFolderPath);
            }

            AssetDatabase.SaveAssets();
        }
    }
}
