using Celeste.Events;
using Celeste.Managers;
using Celeste.Parameters;
using Celeste.Tilemaps;
using Robbi.Runtime.Actors;
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

        public IEnumerable<EnemyRuntime> EnemyRuntimes
        {
            get { return enemyRuntimes; }
        }

        [Header("Tilemaps")]
        public TilemapValue movementTilemap;

        [Header("Level Lose")]
        public StringEvent levelLostEvent;
        public StringValue caughtByEnemyReason;

        [Header("Parameters")]
        public Vector3Value playerPosition;
        public BoolValue enemiesMoving;

        private List<EnemyRuntime> enemyRuntimes = new List<EnemyRuntime>();

        #endregion

        public void Initialize(IEnumerable<Enemy> enemies)
        {
            // Spawn Pool this?  How do we do support variable prefabs for enemies?
            for (int i = enemyRuntimes.Count - 1; i >= 0; --i)
            {
                enemyRuntimes[i].Shutdown();
            }
            enemyRuntimes.Clear();

            foreach (Enemy enemy in enemies)
            {
                enemyRuntimes.Add(enemy.CreateRuntime(movementTilemap.Value));
            }
        }

        public void Cleanup()
        {
        }

        private void CheckForEnemyReachedPlayer()
        {
            Vector3Int playerTile = movementTilemap.Value.WorldToCell(playerPosition.Value);
            for (int i = 0; i < enemyRuntimes.Count; ++i) 
            {
                Vector3Int enemyTile = enemyRuntimes[i].GetTile(movementTilemap.Value);
                if (playerTile == enemyTile)
                {
                    levelLostEvent.Raise(caughtByEnemyReason.Value);
                }
            }
        }

        #region Unity Methods

        private void OnValidate()
        {
            transform.position = Vector3.zero;
        }

        private void Update()
        {
            if (enemiesMoving.Value)
            {
                CheckForEnemyReachedPlayer();
            }
        }

        #endregion
    }
}
