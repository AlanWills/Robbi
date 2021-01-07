using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class InteractBoosterHiddenBeforeUnlockLevel : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Boosters/Interact Booster/Interact Booster Hidden Before Unlock Level")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<InteractBoosterHiddenBeforeUnlockLevel>();
        }
    }
}
