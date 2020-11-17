using Robbi.Levels.Modifiers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Levels.Elements
{
    public struct InteractArgs
    {
    }

    [CreateAssetMenu(fileName = "Interactable", menuName = "Robbi/Interactables/Interactable")]
    public class Interactable : ScriptableObject
    {
        #region Properties and Fields

        public int NumInteractedModifiers
        {
            get { return interactedModifiers.Count; }
        }

        public Vector3Int position;

        [SerializeField]
        private List<LevelModifier> interactedModifiers = new List<LevelModifier>();

        #endregion

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
            interactedModifiers.Add(modifier);

#if UNITY_EDITOR
            AssetDatabase.AddObjectToAsset(modifier, this);
            EditorUtility.SetDirty(this);
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
            foreach (LevelModifier modifier in interactedModifiers)
            {
                modifier.Execute(interactArgs);
            }
        }

        #endregion
    }
}
