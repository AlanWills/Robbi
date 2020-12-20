using Robbi.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Robbi.Debugging.Commands
{
    [AddComponentMenu("Robbi/Debugging/Execute Debug Command")]
    public class ExecuteDebugCommand : MonoBehaviour
    {
        #region Properties and Fields

        public Text commandText;
        public StringEvent onExecute;

        #endregion

        public void Execute()
        {
            onExecute.Raise(commandText.text);
        }

        #region Unity Methods

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Return))
            {
                Execute();
            }
        }

        #endregion
    }
}
