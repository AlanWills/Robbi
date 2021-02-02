using Celeste.Events;
using Celeste.Managers;
using Celeste.Parameters;
using Celeste.Tilemaps;
using Robbi.Levels.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Runtime
{
    [AddComponentMenu("Robbi/Runtime/Enemies Manager")]
    public class EnemiesManager : NamedManager
    {
        #region Properties and Fields

        [Header("Tilemaps")]
        public TilemapValue movementTilemap;

        [Header("Level Lose")]
        public StringEvent levelLostEvent;
        public StringValue caughtByEnemyReason;

        [Header("Parameters")]
        public Vector3Value playerPosition;
        public BoolValue enemiesMoving;

        private List<Enemy> enemies = new List<Enemy>();
        private List<GameObject> enemyGameObjects = new List<GameObject>();

        #endregion

        #region IEnvironmentManager

        public void Initialize(IEnumerable<Enemy> _enemies)
        {
            enemies.Clear();
            enemies.AddRange(_enemies);

            // Spawn Pool this?  How do we do variable prefabs for enemies?
            for (int i = enemyGameObjects.Count - 1; i >= 0; --i)
            {
                GameObject.Destroy(enemyGameObjects[i]);
            }
            enemyGameObjects.Clear();

            foreach (Enemy enemy in enemies)
            {
                GameObject enemyGameObject = GameObject.Instantiate(enemy.prefab, transform);
                enemyGameObject.transform.localPosition = movementTilemap.Value.GetCellCenterWorld(enemy.startingPosition);
                enemyGameObjects.Add(enemyGameObject);
            }
        }

        public void Cleanup()
        {
            enemies.Clear();
        }

        private void CheckForReachedPlayer()
        {
            Vector3Int playerTile = movementTilemap.Value.WorldToCell(playerPosition.Value);
            for (int i = 0; i < enemyGameObjects.Count; ++i) 
            {
                Vector3Int enemyTile = movementTilemap.Value.WorldToCell(enemyGameObjects[i].transform.localPosition);
                if (playerTile == enemyTile)
                {
                    levelLostEvent.Raise(caughtByEnemyReason.Value);
                }
            }
        }

        #endregion

        #region Unity Methods

        private void OnValidate()
        {
            transform.position = Vector3.zero;
        }

        private void Update()
        {
            if (enemiesMoving.Value)
            {
                CheckForReachedPlayer();
            }
        }

        #endregion
    }
}
