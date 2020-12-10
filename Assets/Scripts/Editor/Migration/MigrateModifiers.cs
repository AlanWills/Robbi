using Robbi.Events.Levels.Elements;
using Robbi.Levels.Elements;
using Robbi.Levels.Modifiers;
using RobbiEditor.Iterators;
using RobbiEditor.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Migration
{
    public static class MigrateModifiers
    {
        [MenuItem("Robbi/Migration/Migrate Modifiers")]
        public static void MenuItem()
        {
            foreach (LevelFolder levelFolder in new LevelFolders())
            {
                foreach (string interactablePath in levelFolder.Interactables)
                {
                    ScriptableObject so = AssetDatabase.LoadAssetAtPath<ScriptableObject>(interactablePath);
                    if (so is Interactable)
                    {
                        MigrateInteractable(so as Interactable);
                    }
                    else if (so is InteractableStateMachine)
                    {
                        InteractableStateMachine interactableStateMachine = so as InteractableStateMachine;
                        for (int i = 0; i < interactableStateMachine.NumStates; ++i)
                        {
                            MigrateInteractable(interactableStateMachine.GetState(i));
                        }
                    }
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static void MigrateInteractable(Interactable interactable)
        {
            DoorEvent openDoorEvent = AssetDatabase.LoadAssetAtPath<DoorEvent>(EventFiles.DOOR_OPENED_EVENT);
            DoorEvent closeDoorEvent = AssetDatabase.LoadAssetAtPath<DoorEvent>(EventFiles.DOOR_CLOSED_EVENT);
            DoorEvent toggleDoorEvent = AssetDatabase.LoadAssetAtPath<DoorEvent>(EventFiles.DOOR_TOGGLED_EVENT);

            for (int i = 0, n = interactable.NumInteractedModifiers; i < n; ++i)
            {
                RaiseDoorEvent raiseDoorEvent = interactable.GetInteractedModifier(i) as RaiseDoorEvent;
                if (raiseDoorEvent.doorEvent == openDoorEvent)
                {
                    OpenDoor openDoor = interactable.AddInteractedModifier<OpenDoor>();
                    openDoor.doorEvent = openDoorEvent;
                    openDoor.door = raiseDoorEvent.door;
                    EditorUtility.SetDirty(openDoor);
                }
                else if (raiseDoorEvent.doorEvent == closeDoorEvent)
                {
                    CloseDoor closeDoor = interactable.AddInteractedModifier<CloseDoor>();
                    closeDoor.doorEvent = closeDoorEvent;
                    closeDoor.door = raiseDoorEvent.door;
                    EditorUtility.SetDirty(closeDoor);
                }
                else if (raiseDoorEvent.doorEvent == toggleDoorEvent)
                {
                    ToggleDoor toggleDoor = interactable.AddInteractedModifier<ToggleDoor>();
                    toggleDoor.doorEvent = toggleDoorEvent;
                    toggleDoor.door = raiseDoorEvent.door;
                    EditorUtility.SetDirty(toggleDoor);
                }
            }

            // Remove old modifiers now
            for (int i = interactable.NumInteractedModifiers / 2; i > 0; --i)
            {
                interactable.RemoveInteractedModifier(i - 1);
            }
        }
    }
}
