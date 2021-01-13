using Celeste.Events;
using Robbi.Levels.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Levels.Effects
{
    [CustomEditor(typeof(ModifyFuel))]
    public class ModifyFuelEditor : PickupEffectEditor
    {
        private void OnEnable()
        {
            (target as ModifyFuel).modifyFuelEvent = AssetDatabase.LoadAssetAtPath<UIntEvent>(EventFiles.ADD_FUEL_EVENT);
            EditorUtility.SetDirty(target);
        }
    }
}
