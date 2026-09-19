using Robbi.Runtime.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Levels.Elements
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Robbi/Levels/Enemy")]
    public class Enemy : ScriptableObject, ILevelElement
    {
        #region Properties and Fields

        public Vector3Int startingPosition;
        public GameObject prefab;

        #endregion

        #region Initialization
        
        public EnemyRuntime CreateRuntime(Tilemap movementTilemap, Transform parentTransform)
        {
            GameObject enemyGameObject = GameObject.Instantiate(prefab, parentTransform);
            enemyGameObject.transform.localPosition = movementTilemap.GetCellCenterWorld(startingPosition);

            EnemyRuntime enemyRuntime = enemyGameObject.GetComponent<EnemyRuntime>();
            Debug.AssertFormat(enemyRuntime != null, "{0} has a prefab which is missing an EnemyRuntime");
            return enemyRuntime;
        }

        #endregion
    }
}
