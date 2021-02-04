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

        public IEnumerable<EnemyRuntime> Enemies
        {
            get { return enemyRuntimes; }
        }

        [Header("Tilemaps")]
        public TilemapValue movementTilemap;

        [Header("Parameters")]
        public BoolValue enemiesMoving;

        private PlayerRuntime playerRuntime;
        private List<EnemyRuntime> enemyRuntimes = new List<EnemyRuntime>();

        #endregion

        public void Initialize(IEnumerable<Enemy> enemies, PlayerRuntime _playerRuntime)
        {
            // Spawn Pool this?  How do we do support variable prefabs for enemies?
            for (int i = enemyRuntimes.Count - 1; i >= 0; --i)
            {
                GameObject.Destroy(enemyRuntimes[i].gameObject);
            }
            enemyRuntimes.Clear();

            foreach (Enemy enemy in enemies)
            {
                enemyRuntimes.Add(enemy.CreateRuntime(movementTilemap.Value));
            }

            playerRuntime = _playerRuntime;
        }

        public void Cleanup()
        {
            playerRuntime = null;
        }

        private void CheckForEnemyReachedPlayer()
        {
            Vector3Int playerTile = playerRuntime.Tile;
            for (int i = 0; i < enemyRuntimes.Count; ++i) 
            {
                Vector3Int enemyTile = enemyRuntimes[i].Tile;
                if (playerTile == enemyTile)
                {
                    playerRuntime.OnReachedByEnemy();
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
