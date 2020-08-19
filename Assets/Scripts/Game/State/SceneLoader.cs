using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Robbi.State
{
    [AddComponentMenu("Robbi/State/Scene Loader")]
    public class SceneLoader : MonoBehaviour
    {
        #region Properties and Fields

        //public bool preload = false;
        //public bool async = true;
        //public bool activateOnLoad = false;
        public string sceneName;
        public LoadSceneMode loadSceneMode = LoadSceneMode.Single;

        //private AsyncOperation sceneLoadOperation;

        #endregion

        #region Unity Methods

        //private void Start()
        //{
        //    if (preload)
        //    {
        //        if (async)
        //        {
        //            sceneLoadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        //            sceneLoadOperation.allowSceneActivation = activateOnLoad;
        //        }
        //        else
        //        {
        //            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

        //            if (!activateOnLoad)
        //            {
        //                SceneManager.GetSceneByName(sceneName).
        //            }
        //        }
        //    }
        //}

        #endregion

        #region Scene Loading Methods

        public void Load()
        {
            SceneManager.LoadScene(sceneName, loadSceneMode);
        }

        #endregion
    }
}
