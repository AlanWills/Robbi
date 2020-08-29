﻿using Robbi.Parameters;
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
            
            SceneManager.LoadScene(sceneName.Value, loadMode);
        }

        #endregion
    }
}