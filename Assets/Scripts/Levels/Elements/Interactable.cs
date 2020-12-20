using Robbi.Levels.Modifiers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Levels.Elements
{
    [CreateAssetMenu(fileName = "Interactable", menuName = "Robbi/Interactables/Interactable")]
    public class Interactable : ScriptableObject, IInteractable
    {
        #region Properties and Fields

        public int NumInteractedModifiers
        {
            get { return interactedModifiers.Count; }
        }

        [SerializeField]
        private Vector3Int position;
        public Vector3Int Position
        {
            get { return position; }
        }

        [SerializeField]
        private Tile interactedTile;
        public Tile InteractedTile
        {
            get { return interactedTile; }
            set 
            { 
                interactedTile = value;
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }
        }

        [SerializeField]
        private Tile uninteractedTile;
        public Tile UninteractedTile
        {
            get { return uninteractedTile; }
            set
            {
                uninteractedTile = value;
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }
        }

        [SerializeField]
        private List<LevelModifier> interactedModifiers = new List<LevelModifier>();

        #endregion

        public void Initialize()
        {
        }

        #region Interacted Modifier Methods

        public LevelModifier GetInteractedModifier(int index)
        {
            return 0 <= index && index < NumInteractedModifiers ? interactedModifiers[index] : null;
        }

        public T AddInteractedModifier<T>() where T : LevelModifier
        {
            return AddInteractedModifier(typeof(T)) as T;
        }

        public LevelModifier AddInteractedModifier(System.Type modifierType)
        {
            LevelModifier modifier = ScriptableObject.CreateInstance(modifierType) as LevelModifier;
            modifier.name = modifierType.Name;
            modifier.hideFlags = HideFlags.HideInHierarchy;
            interactedModifiers.Add(modifier);

#if UNITY_EDITOR
            Celeste.AssetUtils.EditorOnly.AddObjectToMainAsset(modifier, this);
            UnityEditor.EditorUtility.SetDirty(this);
#endif

            return modifier;
        }

        public T ReplaceInteractedModifier<T>(int index) where T : LevelModifier
        {
            T modifier = ScriptableObject.CreateInstance<T>();
            modifier.name = typeof(T).Name;
            modifier.hideFlags = HideFlags.HideInHierarchy;
            interactedModifiers.Insert(index, modifier);
            RemoveInteractedModifier(index + 1);

#if UNITY_EDITOR
            Celeste.AssetUtils.EditorOnly.AddObjectToMainAsset(modifier, this);
            UnityEditor.EditorUtility.SetDirty(this);
#endif

            return modifier;
        }

        public void RemoveInteractedModifier(int index)
        {
            if (0 <= index && index < NumInteractedModifiers)
            {
#if UNITY_EDITOR
                Object.DestroyImmediate(interactedModifiers[index], true);
#endif
                interactedModifiers.RemoveAt(index);
            }
        }

        #endregion

        #region Interaction Methods

        public void Interact(InteractArgs interactArgs)
        {
            interactArgs.interactablesTilemap.SetTile(position, InteractedTile);

            foreach (LevelModifier modifier in interactedModifiers)
            {
                modifier.Execute(interactArgs);
            }
        }

        #endregion
    }
}
