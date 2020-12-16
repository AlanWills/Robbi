using Robbi.Levels.Elements;
using Robbi.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Levels.Modifiers
{
    public abstract class LevelModifier : ScriptableObject, ICopyable<LevelModifier>
    {
        public abstract void CopyFrom(LevelModifier original);
        public abstract void Execute(InteractArgs interactArgs);
    }
}
