using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using XNode;

namespace Robbi.DataSystem.Nodes.UI
{
    [Serializable]
    [CreateNodeMenu("Robbi/UI/Toggle")]
    public class ToggleNode : DataNode, IUpdateable
    {
        #region Properties and Fields

        [Input]
        public bool isChecked;

        [Input]
        public Toggle toggle;

        #endregion

        #region IUpdateable

        public void Update()
        {
            Toggle _toggle = GetToggle();
            if (_toggle != null)
            {
                _toggle.isOn = GetInputValue(nameof(isChecked), isChecked);
            }
        }

        #endregion

        #region Utility Methods

        private Toggle GetToggle()
        {
            Toggle _toggle = GetInputValue(nameof(toggle), toggle);
            if (_toggle == null)
            {
                GameObject gameObject = GetInputValue<GameObject>(nameof(toggle));
                if (gameObject != null)
                {
                    _toggle = gameObject.GetComponent<Toggle>();
                }
            }

            return _toggle;
        }

        #endregion
    }
}
