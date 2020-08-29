using Robbi.Parameters;
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
        public LoadSceneMode loadSceneMode = LoadSceneMode.Single;

        #endregion

        public SceneLoaderNode()
        {
            AddDefaultInputPort();
            AddDefaultOutputPort();
        }

        #region FSM Runtime Methods

        protected override void OnEnter()
        {
            base.OnEnter();
            
            SceneManager.LoadScene(sceneName, loadSceneMode);
        }

        #endregion
    }
}