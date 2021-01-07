using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class InteractBoosterShownAtUnlockLevel : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Boosters/Interact Booster/Interact Booster Shown At Unlock Level")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<InteractBoosterShownAtUnlockLevel>();
        }
    }
}
