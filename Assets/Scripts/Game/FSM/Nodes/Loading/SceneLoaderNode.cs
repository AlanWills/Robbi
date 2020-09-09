using Robbi.Objects;
using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using XNode;

namespace Robbi.FSM.Nodes
{
    [Serializable]
    [CreateNodeMenu("Robbi/Loading/Scene Loader")]
    [NodeWidth(250), NodeTint(0.2f, 0.2f, 0.6f)]
    public class SceneLoaderNode : FSMNode
    {
        #region Properties and Fields

        public StringReference sceneName;
        public LoadSceneMode loadMode = LoadSceneMode.Single;

        private AsyncOperation loadOperation;

        #endregion

        #region Add/Remove/Copy

        protected override void OnAddToGraph()
        {
            base.OnAddToGraph();

            if (sceneName == null)
            {
                sceneName = CreateParameter<StringReference>(name + "_sceneName");
            }
        }

        protected override void OnRemoveFromGraph()
        {
            base.OnRemoveFromGraph();

            RemoveParameter(sceneName);
        }

        protected override void OnCopyInGraph(FSMNode original)
        {
            base.OnCopyInGraph(original);

            SceneLoaderNode originalSceneLoader = original as SceneLoaderNode;
            sceneName = CreateParameter(originalSceneLoader.sceneName);
        }

        #endregion

        #region FSM Runtime Methods

        protected override void OnEnter()
        {
            base.OnEnter();

            loadOperation = SceneManager.LoadSceneAsync(sceneName.Value, loadMode);
        }

        protected override FSMNode OnUpdate()
        {
            return loadOperation.isDone ? base.OnUpdate() : null;
        }

        #endregion
    }
}