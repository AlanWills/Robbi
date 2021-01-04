using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class PortalsDoNotUseFuel : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Level/Portals/Portals Do Not Use Fuel")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<PortalsDoNotUseFuel>();
        }
    }
}
