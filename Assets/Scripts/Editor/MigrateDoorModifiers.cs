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

namespace RobbiEditor
{
    public class MigrateDoorModifiers
    {
        [MenuItem("Robbi/Migration/Migrate Door Modifiers")]
        public static void MenuItem()
        {
            DoorEvent doorOpenedEvent = AssetDatabase.LoadAssetAtPath<DoorEvent>(EventFiles.DOOR_OPENED_EVENT);
            DoorEvent doorClosedEvent = AssetDatabase.LoadAssetAtPath<DoorEvent>(EventFiles.DOOR_CLOSED_EVENT);

            foreach (LevelFolder levelFolder in new LevelFolders())
            {
                foreach (string interactablePath in levelFolder.Interactables)
                {
                    Interactable interactable = AssetDatabase.LoadAssetAtPath<Interactable>(interactablePath);

                    for (int i = interactable.NumInteractedModifiers; i > 0; --i)
                    {
                        LevelModifier levelModifier = interactable.GetInteractedModifier(i - 1);
                        
                        if (levelModifier is OpenDoorModifier)
                        {
                            OpenDoorModifier openDoorModifier = levelModifier as OpenDoorModifier;
                            RaiseDoorEvent raiseDoorEvent = interactable.AddInteractedModifier<RaiseDoorEvent>();
                            raiseDoorEvent.doorEvent = doorOpenedEvent;
                            raiseDoorEvent.door = openDoorModifier.door;

                            Robbi.AssetUtils.EditorOnly.RemoveObjectFromAsset(openDoorModifier);
                            interactable.RemoveInteractedModifier(i - 1);

                            EditorUtility.SetDirty(raiseDoorEvent);
                            EditorUtility.SetDirty(interactable);
                        }
                        else if (levelModifier is CloseDoorModifier)
                        {
                            CloseDoorModifier closeDoorModifier = levelModifier as CloseDoorModifier;
                            RaiseDoorEvent raiseDoorEvent = interactable.AddInteractedModifier<RaiseDoorEvent>();
                            raiseDoorEvent.doorEvent = doorClosedEvent;
                            raiseDoorEvent.door = closeDoorModifier.door;

                            Robbi.AssetUtils.EditorOnly.RemoveObjectFromAsset(closeDoorModifier);
                            interactable.RemoveInteractedModifier(i - 1);

                            EditorUtility.SetDirty(raiseDoorEvent);
                            EditorUtility.SetDirty(interactable);
                        }
                    }
                }
            }

            AssetDatabase.SaveAssets();
        }
    }
}
