using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class DoorToggleBoosterBlocksMovementInput : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Boosters/Door Toggle Booster/Door Toggle Booster Blocks Movement Input")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<DoorToggleBoosterBlocksMovementInput>();
        }
    }
}
