using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class CannotClickOnWaypointBoosterWhenHaveNoBalance : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Boosters/Waypoint Booster/Cannot Click On Waypoint Booster When Have No Balance")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<CannotClickOnWaypointBoosterWhenHaveNoBalance>();
        }
    }
}
