using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class CancellingInteractBoosterDoesNotReduceBalance : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Boosters/Interact Booster/Cancelling Interact Booster Does Not Reduce Balance")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<CancellingInteractBoosterDoesNotReduceBalance>();
        }
    }
}
