using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class WinNewLevelGivesReward : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Currency/Win New Level Gives Reward")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<WinNewLevelGivesReward>();
        }
    }
}
