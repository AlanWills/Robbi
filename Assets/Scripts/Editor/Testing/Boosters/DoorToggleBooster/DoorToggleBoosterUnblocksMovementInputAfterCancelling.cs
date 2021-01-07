using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class DoorToggleBoosterUnblocksMovementInputAfterCancelling : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Boosters/Door Toggle Booster/Door Toggle Booster Unblocks Movement Input After Cancelling")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<DoorToggleBoosterUnblocksMovementInputAfterCancelling>();
        }
    }
}
