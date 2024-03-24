using Celeste.Parameters;
using Celeste.Tilemaps;
using Robbi.Runtime.Actors;
using Robbi.Levels.Elements;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Robbi.Runtime
{
    [AddComponentMenu("Robbi/Runtime/Enemies Manager")]
    public class EnemiesManager : MonoBehaviour
    {
        #region Properties and Fields

        public IEnumerable<EnemyRuntime> Enemies => enemyRuntimes;

        [Header("Tilemaps")]
        public TilemapValue movementTilemap;

        [Header("Parameters")]
        public BoolValue enemiesMoving;

        [NonSerialized] private PlayerRuntime playerRuntime;
        [NonSerialized] private List<EnemyRuntime> enemyRuntimes = new List<EnemyRuntime>();

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
            for (int i = 0; i < enemyRuntimes.Count && playerRuntime.isActiveAndEnabled; ++i) 
            {
                if (enemyRuntimes[i].isActiveAndEnabled)
                {
                    Vector3Int enemyTile = enemyRuntimes[i].Tile;
                    int cartesianDistance = CartesianDistance(enemyTile, playerTile);

                    if (cartesianDistance <= 1 && (playerRuntime.Position - enemyRuntimes[i].Position).sqrMagnitude < 0.25f)
                    {
                        playerRuntime.OnReachedByEnemy();
                        return;
                    }
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

        #region Utility Methods

        private int CartesianDistance(Vector3Int first, Vector3Int second)
        {
            return Math.Abs(first.x - second.x) + Math.Abs(first.y - second.y) + Math.Abs(first.z - second.z);
        }

        #endregion
    }
}
