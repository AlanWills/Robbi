using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class CanClickOnWaypointBoosterWhenHaveBalance : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Boosters/Waypoint Booster/Can Click On Waypoint Booster When Have Balance")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<CanClickOnWaypointBoosterWhenHaveBalance>();
        }
    }
}
