using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class UsingDoorToggleBoosterReducesBalance : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Boosters/Door Toggle Booster/Using Door Toggle Booster Reduces Balance")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<UsingDoorToggleBoosterReducesBalance>();
        }
    }
}
