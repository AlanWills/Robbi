using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class InteractBoosterActivatesToggle : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Boosters/Interact Booster/Interact Booster Activates Toggle")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<InteractBoosterActivatesToggle>();
        }
    }
}
