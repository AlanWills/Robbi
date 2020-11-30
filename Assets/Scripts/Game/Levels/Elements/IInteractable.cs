using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Levels.Elements
{
    public struct InteractArgs
    {
    }

    public interface IInteractable
    {
        #region Properties and Fields

        Vector3Int Position { get; set; }

        #endregion

        void Interact(InteractArgs interactArgs);
    }
}
