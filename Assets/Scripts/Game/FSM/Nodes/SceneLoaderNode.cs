using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using XNode;

namespace Robbi.FSM.Nodes
{
    [Serializable]
    [CreateNodeMenu("Robbi/Scene Loader")]
    public class SceneLoaderNode : FSMNode
    {
        #region Properties and Fields

        public string sceneName;

        #endregion

        public SceneLoaderNode()
        {
            AddDefaultInputPort();
        }

        #region FSM Runtime Methods

        protected override void OnEnter()
        {
            base.OnEnter();

            SceneManager.LoadScene(sceneName);
        }

        #endregion
    }
}
