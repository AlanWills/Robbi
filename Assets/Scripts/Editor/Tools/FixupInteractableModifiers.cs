using CelesteEditor.Tools;
using Robbi.Events.Levels.Elements;
using Robbi.Levels;
using Robbi.Levels.Elements;
using Robbi.Levels.Modifiers;
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
    public static class FixupInteractableModifiers
    {
        [MenuItem("Robbi/Tools/Fixup Interactable Modifiers")]
        public static void MenuItem()
        {
            foreach (LevelFolder levelFolder in new LevelFolders())
            {
                Level level = AssetDatabase.LoadAssetAtPath<Level>(levelFolder.LevelDataPath);

                foreach (ScriptableObject interactableObject in level.interactables)
                {
                    Interactable interactable = interactableObject as Interactable;
                    if (interactable != null)
                    {
                        FixupModifiers(interactable);
                    }

                    InteractableStateMachine interactableStateMachine = interactableObject as InteractableStateMachine;
                    if (interactableStateMachine != null)
                    {
                        for (int i = 0; i < interactableStateMachine.NumStates; ++i)
                        {
                            FixupModifiers(interactableStateMachine.GetState(i));
                        }
                    }
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static void FixupModifiers(Interactable interactable)
        {
            for (int i = 0; i < interactable.NumInteractedModifiers; ++i)
            {
                LevelModifier modifier = interactable.GetInteractedModifier(i);
                if (modifier is OpenDoor)
                {
                    (modifier as OpenDoor).doorEvent = AssetDatabase.LoadAssetAtPath<DoorEvent>(EventFiles.DOOR_OPENED_EVENT);
                    Debug.AssertFormat((modifier as OpenDoor).door != null, "Modifier {0} has a null open door event", modifier.name);
                    EditorUtility.SetDirty(modifier);
                }
                else if (modifier is CloseDoor)
                {
                    (modifier as CloseDoor).doorEvent = AssetDatabase.LoadAssetAtPath<DoorEvent>(EventFiles.DOOR_CLOSED_EVENT);
                    Debug.AssertFormat((modifier as CloseDoor).door != null, "Modifier {0} has a null close door event", modifier.name);
                    EditorUtility.SetDirty(modifier);
                }
                else if (modifier is ToggleDoor)
                {
                    (modifier as ToggleDoor).doorEvent = AssetDatabase.LoadAssetAtPath<DoorEvent>(EventFiles.DOOR_TOGGLED_EVENT);
                    Debug.AssertFormat((modifier as ToggleDoor).door != null, "Modifier {0} has a null toggle door event", modifier.name);
                    EditorUtility.SetDirty(modifier);
                }
            }
        }
    }
}
