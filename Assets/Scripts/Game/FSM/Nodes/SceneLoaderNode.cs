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
    [CreateNodeMenu("Robbi/Scene Loader")]
    [NodeWidth(250)]
    public class SceneLoaderNode : FSMNode
    {
        #region Properties and Fields

        public StringReference sceneName;
        public LoadSceneMode loadMode = LoadSceneMode.Single;
        public bool makeActive = false;

        private bool sceneLoaded = false;

        #endregion

        #region Initialization

        protected override void Init()
        {
            base.Init();

            if (sceneName == null)
            {
                sceneName = ScriptableObject.CreateInstance<StringReference>();
                sceneName.name = name + "_sceneName";

#if UNITY_EDITOR
                if (UnityEditor.AssetDatabase.IsMainAsset(graph))
                {
                    UnityEditor.AssetDatabase.AddObjectToAsset(sceneName, graph);
                }
#endif
            }
        }

        #endregion

        #region FSM Runtime Methods

        protected override void OnEnter()
        {
            base.OnEnter();

            sceneLoaded = false;

            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            SceneManager.LoadScene(sceneName.Value, loadMode);
        }

        protected override FSMNode OnUpdate()
        {
            return sceneLoaded ? base.OnUpdate() : null;
        }

        protected override void OnExit()
        {
            base.OnExit();

            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        }

        #endregion

        #region Callbacks

        private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode loadMode)
        {
            sceneLoaded = scene.name == sceneName.Value;
            
            if (makeActive && sceneLoaded)
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName.Value));
            }
        }

        #endregion
    }
}