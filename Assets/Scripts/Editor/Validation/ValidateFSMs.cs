﻿using Robbi.FSM;
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
            HashSet<string> failedFsms = new HashSet<string>();
            List<string> allFsms = new List<string>();

            foreach (string fsmGuid in AssetDatabase.FindAssets("t:FSMGraph"))
            {
                string fsmPath = AssetDatabase.GUIDToAssetPath(fsmGuid);
                FSMGraph fsmGraph = AssetDatabase.LoadAssetAtPath<FSMGraph>(fsmPath);

                if (fsmGraph == null)
                {
                    result = false;
                    Debug.LogAssertionFormat("Could not find FSM with path {0}", fsmPath);
                }

                allFsms.Add(fsmGraph.name);

                if (!FSMValidator.Validate(fsmGraph))
                {
                    failedFsms.Add(fsmGraph.name);
                    result = false;
                }
            }

            if (Application.isBatchMode)
            {
                foreach (string fsmName in allFsms)
                {
                    if (failedFsms.Contains(fsmName))
                    {
                        Debug.LogAssertionFormat("{0} failed validation", fsmName);
                    }
                    else
                    {
                        Debug.LogFormat("{0} passed validation, fsmName");
                    }
                }

                // 0 for success
                // 1 for fail
                EditorApplication.Exit(result ? 0 : 1);
            }
            else
            {
                StringBuilder message = new StringBuilder(512);
                foreach (string fsmName in allFsms)
                {
                    message.AppendLineFormat("{0} {1} validation", fsmName, failedFsms.Contains(fsmName) ? "failed" : "passed");
                }

                Debug.Log(message.ToString());
                EditorUtility.DisplayDialog("FSM Validation Result", failedFsms.Count == 0 ? "All FSMs passed validation" : "Some FSMs failed validation", "OK");
            }
        }
    }
}