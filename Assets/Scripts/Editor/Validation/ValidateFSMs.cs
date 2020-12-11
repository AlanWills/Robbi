using Robbi.FSM;
using Robbi.Utils;
using RobbiEditor.Validation.FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Validation
{
    public static class ValidateFSMs
    {
        [MenuItem("Robbi/Validation/Validate FSMs")]
        public static void MenuItem()
        {
            bool result = true;
            List<string> failedFsms = new List<string>();

            foreach (string fsmGuid in AssetDatabase.FindAssets("t:FSMGraph"))
            {
                string fsmPath = AssetDatabase.GUIDToAssetPath(fsmGuid);
                FSMGraph fsmGraph = AssetDatabase.LoadAssetAtPath<FSMGraph>(fsmPath);

                if (fsmGraph == null)
                {
                    result = false;
                    Debug.LogAssertionFormat("Could not find FSM with path {0}", fsmPath);
                }

                if (!FSMValidator.Validate(fsmGraph))
                {
                    failedFsms.Add(fsmGraph.name);
                    result = false;
                }
            }

            if (Application.isBatchMode)
            {
                if (failedFsms.Count == 0)
                {
                    Debug.Log("All FSMs passed validation");
                }
                else
                {
                    foreach (string str in failedFsms)
                    {
                        Debug.LogAssertionFormat("{0} failed validation", str);
                    }
                }
                
                // 0 for success
                // 1 for fail
                EditorApplication.Exit(result ? 0 : 1);
            }
            else
            {
                StringBuilder message = new StringBuilder(512);
                if (failedFsms.Count == 0)
                {
                    message.Append("All FSMs passed validation");
                }
                else
                {
                    foreach (string str in failedFsms)
                    {
                        message.AppendLineFormat("{0} failed validation", str);
                    }
                }

                EditorUtility.DisplayDialog("FSM Validation Result", message.ToString(), "OK");
            }
        }
    }
}
