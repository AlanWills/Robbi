using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace Robbi.DataSystem.Nodes.UI
{
    [Serializable]
    [CreateNodeMenu("Robbi/UI/Slider")]
    public class SliderNode : DataNode, IUpdateable
    {
        #region Properties and Fields

        [Input]
        public float value;

        public Slider slider;

        #endregion

        #region IUpdateable

        public void Update()
        {
            slider.value = GetInputValue(nameof(value), value);
        }

        #endregion
    }
}
