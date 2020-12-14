using Robbi.Levels.Elements;
using RobbiEditor.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Levels.Elements
{
    [CustomEditor(typeof(DoorColourHelper))]
    public class DoorColourHelperEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            DoorColourHelper doorColourHelper = target as DoorColourHelper;

            if (doorColourHelper.icon != null)
            {
                int index = Array.FindIndex(DoorColours.COLOURS, x => x == doorColourHelper.icon.color);

                EditorGUI.BeginChangeCheck();
                index = EditorGUILayout.Popup(index, DoorColours.COLOUR_NAMES);
                if (EditorGUI.EndChangeCheck() && index >= 0)
                {
                    doorColourHelper.icon.color = DoorColours.COLOURS[index];
                    EditorUtility.SetDirty(doorColourHelper.icon);
                }
            }
        }
    }
}
