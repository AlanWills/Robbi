using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class DoorToggleBoosterShownAtUnlockLevel : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Boosters/Door Toggle Booster/Door Toggle Booster Shown At Unlock Level")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<DoorToggleBoosterShownAtUnlockLevel>();
        }
    }
}
