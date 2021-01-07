using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class WaypointBoosterShownAtUnlockLevel : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Boosters/Waypoint Booster/Waypoint Booster Shown At Unlock Level")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<WaypointBoosterShownAtUnlockLevel>();
        }
    }
}
