using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Levels.Elements
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Robbi/Levels/Enemy")]
    public class Enemy : ScriptableObject, ILevelElement
    {
        #region Properties and Fields

        public Vector3Int startingPosition;
        public GameObject prefab;

        #endregion
    }
}
