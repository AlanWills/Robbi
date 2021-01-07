using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class DoorToggleBoosterUnblocksMovementInputAfterUse : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Boosters/Door Toggle Booster/Door Toggle Booster Unblocks Movement Input After Use")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<DoorToggleBoosterUnblocksMovementInputAfterUse>();
        }
    }
}
