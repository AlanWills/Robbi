using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class InteractBoosterUnblocksMovementInputAfterCancelling : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Boosters/Interact Booster/Interact Booster Unblocks Movement Input After Cancelling")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<InteractBoosterUnblocksMovementInputAfterCancelling>();
        }
    }
}
