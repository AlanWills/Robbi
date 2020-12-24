using Robbi.Levels;
using Robbi.Levels.Elements;
using RobbiEditor.Constants;
using RobbiEditor.Iterators;
using RobbiEditor.Levels;
using System;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Migration
{
    public static class MigrateDoorColours
    {
        [MenuItem("Robbi/Migration/Migrate Door Colours")]
        public static void MenuItem()
        {
            Color[] newColours = new Color[4]
            {
                new Color(0, 210.0f, 0.0f),
                new Color(214.0f, 40.0f, 0.0f),
                new Color(0, 128.0f, 176.0f),
                new Color(200.0f, 120.0f, 0.0f)
            };

            foreach (LevelFolder levelFolder in new LevelFolders())
            {
                GameObject gameObject = AssetDatabase.LoadAssetAtPath<GameObject>(levelFolder.PrefabPath);
                LevelRoot levelRoot = gameObject.GetComponent<LevelRoot>();
                Transform interactablesTx = levelRoot.interactablesTilemap.transform;

                for (int i = 0; i < interactablesTx.childCount; ++i)
                {
                    DoorColourHelper doorColourHelper = interactablesTx.GetChild(i).GetComponentInChildren<DoorColourHelper>();
                    int index = Array.FindIndex(DoorColours.COLOURS, x => x == doorColourHelper.Colour);

                    if (index > 0 && index < newColours.Length)
                    {
                        doorColourHelper.Colour = newColours[index];
                        EditorUtility.SetDirty(gameObject);
                    }
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}