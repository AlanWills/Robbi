using Robbi.DataSystem;
using Robbi.FSM;
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

namespace RobbiEditor.Migration
{
    public static class MigrateHideFlags
    {
        [MenuItem("Robbi/Migration/Migrate Hide Flags")]
        public static void MigrateHorizontalDoors()
        {
            foreach (string fsmGraphGuid in AssetDatabase.FindAssets("t:FSMGraph"))
            {
                string fsmGraphPath = AssetDatabase.GUIDToAssetPath(fsmGraphGuid);
                FSMGraph fsmGraph = AssetDatabase.LoadAssetAtPath<FSMGraph>(fsmGraphPath);

                foreach (UnityEngine.Object obj in AssetDatabase.LoadAllAssetsAtPath(fsmGraphPath))
                {
                    if (obj != null && obj != fsmGraph)
                    {
                        obj.hideFlags = HideFlags.HideInHierarchy;
                        EditorUtility.SetDirty(obj);
                    }
                }
            }

            foreach (string dataGraphGuid in AssetDatabase.FindAssets("t:DataGraph"))
            {
                string dataGraphPath = AssetDatabase.GUIDToAssetPath(dataGraphGuid);
                DataGraph dataGraph = AssetDatabase.LoadAssetAtPath<DataGraph>(dataGraphPath);

                foreach (UnityEngine.Object obj in AssetDatabase.LoadAllAssetsAtPath(dataGraphPath))
                {
                    if (obj != null && obj != dataGraph)
                    {
                        obj.hideFlags = HideFlags.HideInHierarchy;
                        EditorUtility.SetDirty(obj);
                    }
                }
            }

            AssetDatabase.SaveAssets();
        }
    }
}
