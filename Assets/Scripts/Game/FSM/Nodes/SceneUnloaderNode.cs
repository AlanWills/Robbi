using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Robbi.FSM.Nodes
{
    [Serializable]
    [CreateNodeMenu("Robbi/Scene Unloader")]
    [NodeWidth(250)]
    public class SceneUnloaderNode : FSMNode
    {
        #region Properties and Fields

        public StringReference sceneName;

        #endregion

        #region Initialization

        protected override void Init()
        {
            base.Init();

            if (sceneName == null)
            {
                sceneName = ScriptableObject.CreateInstance<StringReference>();
            }
        }

        #endregion

        #region FSM Runtime Methods

        protected override void OnEnter()
        {
            base.OnEnter();

            SceneManager.UnloadSceneAsync(sceneName.Value);
        }

        #endregion
    }
}
