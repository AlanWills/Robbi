using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.FSM.Nodes.Info
{
    [Serializable]
    [CreateNodeMenu("Robbi/Info/Application Info")]
    public class ApplicationInfoNode : FSMNode
    {
        #region Properties and Fields

        public BoolValue isEditor;
        public BoolValue isMobile;
        public BoolValue isDevBuild;

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            if (isEditor != null)
            {
                isEditor.value = Application.isEditor;
            }

            if (isMobile != null)
            {
                isMobile.value = Application.isMobilePlatform;
            }

            if (isDevBuild != null)
            {
#if DEVELOPMENT_BUILD
                isDevBuild.value = true;
#else
                isDevBuild.value = false;
#endif
            }
        }

#endregion
    }
}
