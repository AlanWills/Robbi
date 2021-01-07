using Robbi.Levels.Elements;
using System;
using UnityEngine;
using Celeste.Tilemaps;
using Celeste.FSM;

namespace Robbi.FSM.Nodes
{
    [Serializable]
    [CreateNodeMenu("Robbi/Level/Operate Interactable")]
    [NodeTint(0.2f, 0.2f, 0.6f)]
    public class OperateInteractableNode : FSMNode
    {
        #region Properties and Fields

        [Input]
        public ScriptableObject interactable;
        public TilemapValue interactablesTilemap;

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            ScriptableObject _interactable = GetInputValue(nameof(interactable), interactable);
            if (!(_interactable is IInteractable))
            {
                Debug.LogAssertionFormat("Chosen Interactable {0} does not derive from IInteractable");
                return;
            }

            (_interactable as IInteractable).Interact(new InteractArgs() { interactablesTilemap = interactablesTilemap.Value });
        }

        #endregion
    }
}
