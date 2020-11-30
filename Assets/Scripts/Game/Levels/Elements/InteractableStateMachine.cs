using Robbi.Levels.Modifiers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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

        [Tooltip("Should the state continuously loop, so that when it reaches the final state it will go back to the first state again.")]
        public bool loop = true;

        [SerializeField]
        private Vector3Int position;
        public Vector3Int Position
        {
            get { return position; }
            set { position = value; }
        }

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
            state.Position = Position;
            states.Add(state);

#if UNITY_EDITOR
            UnityEditor.AssetDatabase.AddObjectToAsset(state, this);
            UnityEditor.EditorUtility.SetDirty(this);
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
