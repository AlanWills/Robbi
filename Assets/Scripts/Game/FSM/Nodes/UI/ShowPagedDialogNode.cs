﻿using Robbi.Events;
using Robbi.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Robbi.UI.PagedDialog;

namespace Robbi.FSM.Nodes.UI
{
    [Serializable]
    [CreateNodeMenu("Robbi/UI/Show Paged Dialog")]
    [NodeWidth(250), NodeTint(0.8f, 0.9f, 0)]
    public class ShowPagedDialogNode : FSMNode
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

        public const string CONFIRM_PRESSED_PORT_NAME = "Confirm Pressed";
        public const string CLOSE_PRESSED_PORT_NAME = "Close Pressed";

        public PagedDialog dialog;
        public ShowPagedDialogParams parameters = new ShowPagedDialogParams();

        private DummyEventListener confirmDummyEventListener = new DummyEventListener();
        private DummyEventListener closeDummyEventListener = new DummyEventListener();
        private PagedDialog dialogInstance;

        #endregion

        public ShowPagedDialogNode()
        {
            RemoveDynamicPort(DEFAULT_OUTPUT_PORT_NAME);

            AddOutputPort(CONFIRM_PRESSED_PORT_NAME);
            AddOutputPort(CLOSE_PRESSED_PORT_NAME);

            parameters.showCloseButton = true;
        }

        #region FSM Runtime Methods

        protected override void OnEnter()
        {
            base.OnEnter();

            confirmDummyEventListener.eventRaised = false;
            closeDummyEventListener.eventRaised = false;

            dialogInstance = GameObject.Instantiate(dialog.gameObject).GetComponent<PagedDialog>();
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

            return this;
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