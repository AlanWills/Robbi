using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Levels.Elements
{
    [AddComponentMenu("Robbi/Levels/Elements/Door Colour Helper")]
    public class DoorColourHelper : MonoBehaviour
    {
        #region Properties and Fields

        public Color Colour 
        { 
            get { return icon.color; }
            set 
            { 
                icon.color = value;

#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(icon);
#endif
            }
        }

        public SpriteRenderer icon;

        #endregion
    }
}
