using Robbi.Levels.Modifiers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Levels.Elements
{
    [CreateAssetMenu(fileName = "InteractableStateMachine", menuName = "Robbi/Interactables/Interactable State Machine")]
    public class InteractableStateMachine : ScriptableObject, IInteractable
    {
        #region Properties and Fields

        public int NumStates
        {
            get { return states.Count; }
        }

        private bool CurrentStateValid
        {
            get { return 0 <= currentState && currentState < NumStates; }
        }

        public Vector3Int Position
        {
            get { return CurrentStateValid ? states[currentState].Position : new Vector3Int(); }
        }

        public TileBase InteractedTile
        {
            get { return CurrentStateValid ? states[currentState].InteractedTile : null; }
        }

        [Tooltip("Should the state continuously loop, so that when it reaches the final state it will go back to the first state again.")]
        public bool loop = true;

        [SerializeField]
        private List<Interactable> states = new List<Interactable>();

        private int currentState = 0;

        #endregion

        public void Initialize()
        {
            currentState = 0;
        }

        #region State Methods

        public Interactable GetState(int index)
        {
            return 0 <= index && index < NumStates ? states[index] : null;
        }

        public Interactable AddState(string name)
        {
            Interactable state = ScriptableObject.CreateInstance<Interactable>();
            state.name = name;
            state.hideFlags = HideFlags.HideInHierarchy;
            states.Add(state);

#if UNITY_EDITOR
            Celeste.AssetUtils.EditorOnly.AddObjectToMainAsset(state, this);
#endif

            return state;
        }

        public void RemoveState(int index)
        {
            if (0 <= index && index < NumStates)
            {
#if UNITY_EDITOR
                Object.DestroyImmediate(states[index], true);
#endif
                states.RemoveAt(index);
            }
        }

#endregion

#region Interaction Methods

        public void Interact(InteractArgs interactArgs)
        {
            if (currentState < 0 || currentState >= NumStates)
            {
                // We have finished all the interactable states so do nothing
                return;
            }

            // Update current state index first
            currentState = loop ? ((currentState + 1) % NumStates) : currentState + 1;

            // Finally do the interaction - this will override any previous tile changes to ensure the interacted tile is ALWAYS applied
            states[currentState].Interact(interactArgs);
        }

#endregion
    }
}
