using Robbi.Levels.Effects;
using Robbi.Levels.Elements;
using CelesteEditor.Tools;
using System;
using UnityEditor;
using UnityEngine;
using CelesteEditor;
using Celeste;

namespace RobbiEditor.Levels.Elements
{
    [CustomEditor(typeof(Collectable))]
    public class CollectableEditor : Editor
    {
        #region Properties and Fields

        private Collectable Collectable
        {
            get { return target as Collectable; }
        }

        private SerializedProperty pickupEffectsProperty;
        private int selectedEffectType = 0;
        private bool isMainAsset;

        private static Type[] effectTypes = new Type[]
        {
            typeof(ModifyFuel),
            typeof(AddCollectionTarget)
        };

        private static string[] modifierDisplayNames = new string[]
        {
            "Modify Fuel",
            "Add Collection Target",
        };

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            pickupEffectsProperty = serializedObject.FindProperty("pickupEffects");
            isMainAsset = AssetDatabase.IsMainAsset(target);
        }

        #endregion

        #region GUI

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawPropertiesExcluding(serializedObject, "m_Script", "pickupEffects");

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Effects", CelesteGUIStyles.BoldLabel);

            if (isMainAsset && GUILayout.Button("Apply Hide Flags", GUILayout.ExpandWidth(false)))
            {
                AssetUtility.ApplyHideFlags(Collectable, HideFlags.HideInHierarchy);
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            {
                ++EditorGUI.indentLevel;

                for (int i = pickupEffectsProperty.arraySize; i > 0; --i)
                {
                    PickupEffect pickupEffect = pickupEffectsProperty.GetArrayElementAtIndex(i - 1).objectReferenceValue as PickupEffect;

                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField(pickupEffect.name, CelesteGUIStyles.BoldLabel);

                        if (GUILayout.Button("Remove", GUILayout.ExpandWidth(false)))
                        {
                            Collectable.RemovePickupEffect(i - 1);
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    Editor effectEditor = Editor.CreateEditor(pickupEffect);
                    effectEditor.OnInspectorGUI();

                    EditorGUILayout.Space();

                    CelesteEditorGUILayout.HorizontalLine();
                }

                --EditorGUI.indentLevel;
            }

            EditorGUILayout.BeginHorizontal();
            {
                selectedEffectType = EditorGUILayout.Popup(selectedEffectType, modifierDisplayNames);

                EditorGUILayout.Space();

                if (GUILayout.Button("Add Effect", GUILayout.ExpandWidth(false)))
                {
                    Collectable.AddPickupEffect(effectTypes[selectedEffectType]);
                }
            }
            EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}
