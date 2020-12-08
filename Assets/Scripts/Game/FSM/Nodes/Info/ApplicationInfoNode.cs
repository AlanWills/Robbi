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
        public BoolValue isDebugBuild;

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            if (isEditor != null)
            {
                isEditor.Value = Application.isEditor;
                Debug.LogFormat("isEditor set to {0}", isEditor.Value);
            }

            if (isMobile != null)
            {
                isMobile.Value = Application.isMobilePlatform;
                Debug.LogFormat("isMobile set to {0}", isMobile.Value);
            }

            if (isDebugBuild != null)
            {
                isDebugBuild.Value = Debug.isDebugBuild;
                Debug.LogFormat("isDebugBuild set to {0}", isDebugBuild.Value);
            }
        }

#endregion
    }
}
