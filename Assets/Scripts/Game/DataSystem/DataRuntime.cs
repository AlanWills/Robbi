using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using XNode;

namespace Robbi.DataSystem
{
    [AddComponentMenu("Robbi/Data/Data Runtime")]
    public class DataRuntime : SceneGraph<DataGraph>
    {
        #region Unity Methods

        private void Start()
        {
            graph.Initialize(gameObject);
        }

        private void Update()
        {
            graph.Update();
        }

        #endregion
    }
}
