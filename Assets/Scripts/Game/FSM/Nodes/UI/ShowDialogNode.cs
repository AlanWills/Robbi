using Robbi.Events;
using Robbi.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Robbi.UI.Dialog;

namespace Robbi.FSM.Nodes.UI
{
    [Serializable]
    [CreateNodeMenu("Robbi/UI/Show Dialog Node")]
    public class ShowDialogNode : FSMNode
    {
        private class DummyEventListener : IEventListener
        {
            public bool eventRaised;

            public void OnEventRaised()
            {
                eventRaised = true;
            }
        }

        #region Properties and Fields

        private const string CONFIRM_PRESSED_PORT_NAME = "Confirm Pressed";
        private const string CLOSE_PRESSED_PORT_NAME = "Close Pressed";

        public Dialog dialog;
        public ShowDialogParams parameters = new ShowDialogParams();

        private DummyEventListener confirmDummyEventListener = new DummyEventListener();
        private DummyEventListener closeDummyEventListener = new DummyEventListener();
        private Dialog dialogInstance;

        #endregion

        public ShowDialogNode()
        {
            RemoveDynamicPort(DEFAULT_OUTPUT_PORT_NAME);

            AddOutputPort(CONFIRM_PRESSED_PORT_NAME);
            AddOutputPort(CLOSE_PRESSED_PORT_NAME);

            parameters.showConfirmButton = true;
            parameters.showCloseButton = true;
        }

        #region FSM Runtime Methods

        protected override void OnEnter()
        {
            base.OnEnter();

            confirmDummyEventListener.eventRaised = false;
            closeDummyEventListener.eventRaised = false;

            dialogInstance = GameObject.Instantiate(dialog.gameObject).GetComponent<Dialog>();
            dialogInstance.ConfirmButtonClicked.AddEventListener(confirmDummyEventListener);
            dialogInstance.CloseButtonClicked.AddEventListener(closeDummyEventListener);
            dialogInstance.Show(parameters);
        }

        protected override FSMNode OnUpdate()
        {
            if (confirmDummyEventListener.eventRaised)
            {
                return GetConnectedNode(CONFIRM_PRESSED_PORT_NAME);
            }
            else if (closeDummyEventListener.eventRaised)
            {
                return GetConnectedNode(CLOSE_PRESSED_PORT_NAME);
            }

            return null;
        }

        protected override void OnExit()
        {
            base.OnExit();

            dialogInstance.ConfirmButtonClicked.RemoveEventListener(confirmDummyEventListener);
            dialogInstance.CloseButtonClicked.RemoveEventListener(closeDummyEventListener);
            dialogInstance = null;
        }

        #endregion
    }
}
