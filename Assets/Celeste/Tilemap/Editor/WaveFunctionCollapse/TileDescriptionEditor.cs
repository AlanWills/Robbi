using Celeste.Tilemaps.WaveFunctionCollapse;
using CelesteEditor.Tilemaps.WaveFunctionCollapse;
using CelesteEditor.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace CelesteEditor.Tilemaps.WaveFunctionCollapse
{
    [CustomEditor(typeof(TileDescription))]
    public class TileDescriptionEditor : Editor
    {
        #region Properties and Fields

        private TileDescription TileDescription
        {
            get { return target as TileDescription; }
        }

        private SerializedProperty rulesProperty;

        #endregion

        #region Menu Items

        [MenuItem("Assets/Create/Celeste/Tilemaps/Wave Function Collapse/Tile Description")]
        public static void CreateDoor()
        {
            CreateTileDescription(GetSelectionObjectPath());
        }

        private static void CreateTileDescription(string path)
        {
            TileDescription tileDescription = ScriptableObject.CreateInstance<TileDescription>();
            tileDescription.name = "TileDescription";
            AssetUtility.CreateAsset(tileDescription, path);

            // Add rules after creating the asset since rules are sub-assets and the tile description must be persistent
            AddRule(tileDescription, Direction.LeftOf);
            AddRule(tileDescription, Direction.RightOf);
            AddRule(tileDescription, Direction.Above);
            AddRule(tileDescription, Direction.Below);
        }

        private static void AddRule(TileDescription tileDescription, Direction direction)
        {
            Rule rule = tileDescription.AddRule();
            rule.direction = direction;
            EditorUtility.SetDirty(rule);
        }

        private static string GetSelectionObjectPath()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (path == "")
            {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }

            return path;
        }

        #endregion

        #region GUI Methods

        private void OnEnable()
        {
            rulesProperty = serializedObject.FindProperty("rules");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            base.OnInspectorGUI();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Add Rule", GUILayout.ExpandWidth(false)))
            {
                TileDescription.AddRule();
            }

            EditorGUILayout.EndHorizontal();

            for (int i = rulesProperty.arraySize - 1; i >= 0; --i)
            {
                EditorGUILayout.PropertyField(rulesProperty.GetArrayElementAtIndex(i), true);

                if (GUILayout.Button("Remove Rule", GUILayout.ExpandWidth(false)))
                {
                    TileDescription.RemoveRule(i);
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}
