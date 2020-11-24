using Robbi.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using RobbiEditor.Utils;

namespace RobbiEditor.Testing
{
    public class LevelLoseWaypointUnreachable
    {
        [MenuItem("Robbi/Testing/Level Lose Waypoint Unreachable")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<LevelLoseWaypointUnreachable>();
        }
    }
}
