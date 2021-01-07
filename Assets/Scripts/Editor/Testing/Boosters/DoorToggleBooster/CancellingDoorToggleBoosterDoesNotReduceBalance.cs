using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class CancellingDoorToggleBoosterDoesNotReduceBalance : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Boosters/Door Toggle Booster/Cancelling Door Toggle Booster Does Not Reduce Balance")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<CancellingDoorToggleBoosterDoesNotReduceBalance>();
        }
    }
}
