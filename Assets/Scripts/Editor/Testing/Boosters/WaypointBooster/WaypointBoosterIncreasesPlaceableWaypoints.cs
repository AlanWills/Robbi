using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class WaypointBoosterIncreasesPlaceableWaypoints : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Boosters/Waypoint Booster/Waypoint Booster Increases Placeable Waypoints")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<WaypointBoosterIncreasesPlaceableWaypoints>();
        }
    }
}
