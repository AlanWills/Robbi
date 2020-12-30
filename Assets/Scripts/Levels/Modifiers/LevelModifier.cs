using Celeste.Objects;
using Robbi.Levels.Elements;
using UnityEngine;

namespace Robbi.Levels.Modifiers
{
    public abstract class LevelModifier : ScriptableObject, ICopyable<LevelModifier>
    {
        public abstract void CopyFrom(LevelModifier original);
        public abstract void Execute(InteractArgs interactArgs);
    }
}
