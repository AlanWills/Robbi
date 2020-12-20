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
            Debug.AssertFormat(graph != null, "{0} has a DataRuntime with no graph set", gameObject.name);
            graph.Initialize(gameObject);
        }

        private void Update()
        {
            graph.Update();
        }

        #endregion
    }
}
