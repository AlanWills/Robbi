using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class InteractBoosterActivatesSwitch : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Boosters/Interact Booster/Interact Booster Activates Switch")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<InteractBoosterActivatesSwitch>();
        }
    }
}
