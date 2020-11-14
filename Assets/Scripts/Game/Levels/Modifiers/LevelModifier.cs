using Robbi.Levels.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Levels.Modifiers
{
    public abstract class LevelModifier : ScriptableObject
    {
        public abstract void Execute(InteractArgs interactArgs);
    }
}
