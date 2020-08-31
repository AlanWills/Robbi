using Robbi.FSM.Nodes.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

using Event = Robbi.Events.Event;

namespace RobbiEditor.FSM.Nodes.UI
{
    [CustomNodeEditor(typeof(ShowDialogNode))]
    public class ShowDialogNodeEditor : FSMNodeEditor
    {
        #region Properties and Fields

        private static string DEFAULT_CONFIRM_PRESSED_EVENT = Path.Combine("Assets", "Events", "UI", "DialogConfirmPressed.asset");
        private static string DEFAULT_CLOSE_PRESSED_EVENT = Path.Combine("Assets", "Events", "UI", "DialogClosePressed.asset");

        public override Color NodeColour
        {
            get { return new Color(0.8f, 0.9f, 0); }
        }

        #endregion

        #region Appearance

        public override int GetWidth()
        {
            return 250;
        }

        #endregion

        #region Unity Methods

        public override void OnCreate()
        {
            base.OnCreate();

            ShowDialogNode showDialogNode = target as ShowDialogNode;

            if (showDialogNode.parameters.confirmButtonPressed == null)
            {
                showDialogNode.parameters.confirmButtonPressed = AssetDatabase.LoadAssetAtPath<Event>(DEFAULT_CONFIRM_PRESSED_EVENT);
            }

            if (showDialogNode.parameters.closeButtonPressed == null)
            {
                showDialogNode.parameters.closeButtonPressed = AssetDatabase.LoadAssetAtPath<Event>(DEFAULT_CLOSE_PRESSED_EVENT);
            }
        }

        #endregion
    }
}
