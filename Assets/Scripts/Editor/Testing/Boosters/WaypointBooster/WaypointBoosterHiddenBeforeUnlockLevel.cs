using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class WaypointBoosterHiddenBeforeUnlockLevel : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Boosters/Waypoint Booster/Waypoint Booster Hidden Before Unlock Level")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<WaypointBoosterHiddenBeforeUnlockLevel>();
        }
    }
}
