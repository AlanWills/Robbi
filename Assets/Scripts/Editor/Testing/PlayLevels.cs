using Robbi.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace RobbiEditor.Testing
{
    [InitializeOnLoad]
    public static class PlayLevels
    {
        [MenuItem("Robbi/Testing/Play Levels")]
        public static void MenuItem()
        {
            IntegrationTestRunner.Instance.RunTest("PlayLevels");
        }
    }
}
