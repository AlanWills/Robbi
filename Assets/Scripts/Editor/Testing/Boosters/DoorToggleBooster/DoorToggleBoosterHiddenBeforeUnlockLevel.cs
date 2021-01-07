using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class DoorToggleBoosterHiddenBeforeUnlockLevel : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Boosters/Door Toggle Booster/Door Toggle Booster Hidden Before Unlock Level")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<DoorToggleBoosterHiddenBeforeUnlockLevel>();
        }
    }
}
