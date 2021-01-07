using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class UsingWaypointBoosterDecreasesBalance : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Boosters/Waypoint Booster/Using Waypoint Booster Decreases Balance")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<UsingWaypointBoosterDecreasesBalance>();
        }
    }
}
