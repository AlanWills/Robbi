using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class WinOldLevelDoesNotGiveReward : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Currency/Win Old Level Does Not Give Reward")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<WinOldLevelDoesNotGiveReward>();
        }
    }
}
