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

        public Tile InteractedTile
        {
            get { return CurrentStateValid ? states[currentState].InteractedTile : null; }
        }

        public Tile UninteractedTile
        {
            get { return CurrentStateValid ? states[currentState].UninteractedTile : null; }
        }

        [Tooltip("Should the state continuously loop, so that when it reaches the final state it will go back to the first state again.")]
        public bool loop = true;

        [SerializeField]
        private List<Interactable> states = new List<Interactable>();

        private int currentState = 0;

        #endregion

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
            AssetUtils.EditorOnly.AddObjectToMainAsset(state, this);
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
            Vector3Int[] vector3Ints = new Vector3Int[NumStates];
            TileBase[] tileBases = new TileBase[NumStates];

            for (int i = 0; i < NumStates; ++i)
            {
                vector3Ints[i] = states[i].Position;
                tileBases[i] = i == currentState ? states[i].InteractedTile : UninteractedTile;
            }

            // Set the tiles for all the states, to make sure we don't have any artifacts from previous states
            // This allows the state machine to have interactables on different tiles
            interactArgs.interactablesTilemap.SetTiles(vector3Ints, tileBases);

            if (0 <= currentState && currentState < NumStates)
            {
                Interactable state = states[currentState];
                state.Interact(interactArgs);
                currentState = loop ? ((currentState + 1) % NumStates) : currentState + 1;
            }
        }

#endregion
    }
}
