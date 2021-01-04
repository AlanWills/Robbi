﻿using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;
using System.Linq;
using Celeste.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CelesteEditor.Tilemap
{
    [CustomEditor(typeof(SimpleAnimatedTile))]
    public class SimpleAnimatedTileEditor : Editor
    {
        private static class Styles
        {
            public static readonly GUIContent orderAnimatedTileSpritesInfo =
                EditorGUIUtility.TrTextContent("Place sprites shown based on the order of animation.");
            public static readonly GUIContent emptyAnimatedTileInfo =
                EditorGUIUtility.TrTextContent(
                    "Drag Sprite or Sprite Texture assets \n" +
                    " to start creating an Animated Tile.");
            public static readonly GUIContent speedLabel = EditorGUIUtility.TrTextContent("Speed",
                "The speed at which the Animation of the Tile will be played.");
            public static readonly GUIContent colliderTypeLabel = EditorGUIUtility.TrTextContent("Collider Type", "The Collider Shape generated by the Tile.");
        }

        private SimpleAnimatedTile tile { get { return (target as SimpleAnimatedTile); } }

        private List<Sprite> dragAndDropSprites;

        private ReorderableList reorderableList;

        private void OnEnable()
        {
            reorderableList = new ReorderableList(tile.animatedSprites, typeof(Sprite), true, true, true, true);
            reorderableList.drawHeaderCallback = OnDrawHeader;
            reorderableList.drawElementCallback = OnDrawElement;
            reorderableList.elementHeightCallback = GetElementHeight;
            reorderableList.onAddCallback = OnAddElement;
            reorderableList.onRemoveCallback = OnRemoveElement;
            reorderableList.onReorderCallback = OnReorderElement;
        }

        private void OnDrawHeader(Rect rect)
        {
            GUI.Label(rect, Styles.orderAnimatedTileSpritesInfo);
        }

        private void OnDrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            if (tile.animatedSprites != null && index < tile.animatedSprites.Length)
            {
                var spriteName = tile.animatedSprites[index] != null ? tile.animatedSprites[index].name : "Null";
                tile.animatedSprites[index] = (Sprite)EditorGUI.ObjectField(rect
                    , $"Sprite {index + 1}: {spriteName}"
                    , tile.animatedSprites[index]
                    , typeof(Sprite)
                    , false);
            }
        }

        private float GetElementHeight(int index)
        {
            return 3 * EditorGUI.GetPropertyHeight(SerializedPropertyType.ObjectReference,
                null);
        }

        private void OnAddElement(ReorderableList list)
        {
            if (tile.animatedSprites == null)
            {
                tile.animatedSprites = new Sprite[1];
            }
            else
            {
                System.Array.Resize<Sprite>(ref tile.animatedSprites, tile.animatedSprites.Length + 1);
            }
        }

        private void OnRemoveElement(ReorderableList list)
        {
            if (tile.animatedSprites != null && tile.animatedSprites.Length > 0 && list.index < tile.animatedSprites.Length)
            {
                var sprites = tile.animatedSprites.ToList();
                sprites.RemoveAt(list.index);
                tile.animatedSprites = sprites.ToArray();
            }
        }

        private void OnReorderElement(ReorderableList list)
        {
            // Fix for 2020.1, which does not track changes when reordering in the list
            EditorUtility.SetDirty(tile);
        }

        private void DisplayClipboardText(GUIContent clipboardText, Rect position)
        {
            Color old = GUI.color;
            GUI.color = Color.gray;
            var infoSize = GUI.skin.label.CalcSize(clipboardText);
            Rect rect = new Rect(position.center.x - infoSize.x * .5f
                , position.center.y - infoSize.y * .5f
                , infoSize.x
                , infoSize.y);
            GUI.Label(rect, clipboardText);
            GUI.color = old;
        }

        private bool dragAndDropActive
        {
            get
            {
                return dragAndDropSprites != null
                       && dragAndDropSprites.Count > 0;
            }
        }

        private void DragAndDropClear()
        {
            dragAndDropSprites = null;
            DragAndDrop.visualMode = DragAndDropVisualMode.None;
            Event.current.Use();
        }

        private static List<Sprite> GetSpritesFromTexture(Texture2D texture)
        {
            string path = AssetDatabase.GetAssetPath(texture);
            Object[] assets = AssetDatabase.LoadAllAssetsAtPath(path);
            List<Sprite> sprites = new List<Sprite>();

            foreach (Object asset in assets)
            {
                if (asset is Sprite)
                {
                    sprites.Add(asset as Sprite);
                }
            }

            return sprites;
        }

        private static List<Sprite> GetValidSingleSprites(Object[] objects)
        {
            List<Sprite> result = new List<Sprite>();
            foreach (Object obj in objects)
            {
                if (obj is Sprite)
                {
                    result.Add(obj as Sprite);
                }
                else if (obj is Texture2D)
                {
                    Texture2D texture = obj as Texture2D;
                    List<Sprite> sprites = GetSpritesFromTexture(texture);
                    if (sprites.Count > 0)
                    {
                        result.AddRange(sprites);
                    }
                }
            }
            return result;
        }

        private void HandleDragAndDrop(Rect guiRect)
        {
            if (DragAndDrop.objectReferences.Length == 0 || !guiRect.Contains(Event.current.mousePosition))
                return;

            switch (Event.current.type)
            {
                case EventType.DragUpdated:
                    {
                        dragAndDropSprites = GetValidSingleSprites(DragAndDrop.objectReferences);
                        if (dragAndDropActive)
                        {
                            DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                            Event.current.Use();
                            GUI.changed = true;
                        }
                    }
                    break;
                case EventType.DragPerform:
                    {
                        if (!dragAndDropActive)
                            return;

                        Undo.RegisterCompleteObjectUndo(tile, "Drag and Drop to Animated Tile");
                        System.Array.Resize<Sprite>(ref tile.animatedSprites, dragAndDropSprites.Count);
                        System.Array.Copy(dragAndDropSprites.ToArray(), tile.animatedSprites, dragAndDropSprites.Count);
                        DragAndDropClear();
                        GUI.changed = true;
                        EditorUtility.SetDirty(tile);
                        GUIUtility.ExitGUI();
                    }
                    break;
                case EventType.Repaint:
                    // Handled in Render()
                    break;
            }

            if (Event.current.type == EventType.DragExited ||
                Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Escape)
            {
                DragAndDropClear();
            }
        }

        /// <summary>
        /// Draws an Inspector for the AnimatedTile.
        /// </summary>
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            int count = EditorGUILayout.DelayedIntField("Number of Animated Sprites", tile.animatedSprites != null ? tile.animatedSprites.Length : 0);
            if (count < 0)
                count = 0;

            if (tile.animatedSprites == null || tile.animatedSprites.Length != count)
            {
                System.Array.Resize<Sprite>(ref tile.animatedSprites, count);
            }

            if (count == 0)
            {
                Rect rect = EditorGUILayout.GetControlRect(false, EditorGUIUtility.singleLineHeight * 5);
                HandleDragAndDrop(rect);
                EditorGUI.DrawRect(rect, dragAndDropActive && rect.Contains(Event.current.mousePosition) ? Color.white : Color.black);
                var innerRect = new Rect(rect.x + 1, rect.y + 1, rect.width - 2, rect.height - 2);
                EditorGUI.DrawRect(innerRect, EditorGUIUtility.isProSkin
                    ? (Color)new Color32(56, 56, 56, 255)
                    : (Color)new Color32(194, 194, 194, 255));
                DisplayClipboardText(Styles.emptyAnimatedTileInfo, rect);
                GUILayout.Space(rect.height);
                EditorGUILayout.Space();
            }

            if (reorderableList != null)
            {
                var tileCount = tile.animatedSprites != null ? tile.animatedSprites.Length : 0;
                if (reorderableList.list == null || reorderableList.count != tileCount)
                    reorderableList.list = tile.animatedSprites;
                reorderableList.DoLayoutList();
            }

            using (new EditorGUI.DisabledScope(tile.animatedSprites == null || tile.animatedSprites.Length == 0))
            {
                tile.speed = Mathf.Max(0, EditorGUILayout.FloatField(Styles.speedLabel, tile.speed));
                tile.m_TileColliderType = (Tile.ColliderType)EditorGUILayout.EnumPopup(Styles.colliderTypeLabel, tile.m_TileColliderType);
                tile.randomizeStartTime = EditorGUILayout.Toggle("Randomize Start Time", tile.randomizeStartTime);
            }

            if (EditorGUI.EndChangeCheck())
                EditorUtility.SetDirty(tile);
        }
    }
}