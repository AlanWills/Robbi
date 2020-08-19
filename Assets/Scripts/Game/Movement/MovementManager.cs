using Robbi.Events;
using Robbi.Parameters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Movement
{
    [AddComponentMenu("Robbi/Movement/Movement Manager")]
    public class MovementManager : MonoBehaviour
    {
        #region Properties and Fields

        public Tilemap movementTilemap;
        public Tilemap doorsTilemap;
        public Vector3Value playerLocalPosition;
        public GameObject movementIndicator;
        public float speed = 1;
        public Vector3IntEvent onMovedTo;

        private Grid grid;
        private Stack<Vector3> waypoints = new Stack<Vector3>();

#if UNITY_ANDROID || UNITY_IPHONE
        private float timeSinceFingerDown = 0;
        private const float TAP_THRESOLD = 0.4f;
#endif

        #endregion

        #region Unity Methods

        private void Start()
        {
            grid = movementTilemap.layoutGrid;
        }

        private void Update()
        {
            if (InputOnGrid())
            {
                Vector3 targetWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int targetGridPosition = grid.WorldToCell(targetWorldPosition);
                Vector3Int currentGridPosition = grid.WorldToCell(playerLocalPosition.value);

                if (targetGridPosition != currentGridPosition && movementTilemap.HasTile(targetGridPosition))
                {
                    CalculateWaypoints(currentGridPosition, targetGridPosition);

                    if (waypoints.Count > 0)
                    {
                        GameObject indicator = GameObject.Instantiate(movementIndicator, movementTilemap.transform);
                        indicator.transform.position = grid.GetCellCenterLocal(targetGridPosition);
                    }
                }
            }

            if (waypoints.Count > 0)
            {
                Vector3 currentWaypoint = waypoints.Peek();
                Vector3 newPosition = Vector3.MoveTowards(playerLocalPosition.value, currentWaypoint, speed * Time.deltaTime);

                if (newPosition == playerLocalPosition.value)
                {
                    onMovedTo.Raise(new Vector3Int((int)newPosition.x, (int)newPosition.y, (int)newPosition.z));
                    waypoints.Pop();
                }
                else
                {
                    playerLocalPosition.value = newPosition;
                }
            }
        }

        #endregion

        #region Input Methods

        private bool InputOnGrid()
        {
#if UNITY_ANDROID || UNITY_IPHONE
            if (Input.touchCount != 1)
            {
                return false;
            }

            TouchPhase touchPhase = Input.GetTouch(0).phase;
            switch (Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    timeSinceFingerDown = 0;
                    return false;

                case TouchPhase.Ended:
                    return timeSinceFingerDown < TAP_THRESOLD;

                case TouchPhase.Stationary:
                    timeSinceFingerDown += Time.deltaTime;
                    return false;

                case TouchPhase.Moved:
                    timeSinceFingerDown += 2 * Time.deltaTime;
                    return false;

                default:
                    timeSinceFingerDown = TAP_THRESOLD;
                    return false;
            }
#else
            return Input.GetMouseButtonDown(0);
#endif
        }

        #endregion

        #region Pathfinding

        private void CalculateWaypoints(Vector3Int startingPosition, Vector3Int targetPosition)
        {
            waypoints.Clear();

            HashSet<Vector3Int> openSet = new HashSet<Vector3Int>() { startingPosition };
            Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int, Vector3Int>();
            Dictionary<Vector3Int, float> costFromStart = new Dictionary<Vector3Int, float>();
            Dictionary<Vector3Int, float> costOverall = new Dictionary<Vector3Int, float>();

            costFromStart.Add(startingPosition, 0);
            costOverall.Add(startingPosition, Math.Abs(targetPosition.x - startingPosition.x) + Math.Abs(targetPosition.y - startingPosition.y));

            while (openSet.Count > 0)
            {
                Vector3Int bestPosition = GetBestPosition(openSet, costOverall);
                if (bestPosition == targetPosition)
                {
                    ConstructWaypoints(targetPosition, cameFrom);
                    return;
                }

                openSet.Remove(bestPosition);

                // Left
                UpdateDirectional(new Vector3Int(-1, 0, 0), bestPosition, targetPosition, openSet, cameFrom, costFromStart, costOverall);

                // Up
                UpdateDirectional(new Vector3Int(0, 1, 0), bestPosition, targetPosition, openSet, cameFrom, costFromStart, costOverall);

                // Right
                UpdateDirectional(new Vector3Int(1, 0, 0), bestPosition, targetPosition, openSet, cameFrom, costFromStart, costOverall);

                // Down
                UpdateDirectional(new Vector3Int(0, -1, 0), bestPosition, targetPosition, openSet, cameFrom, costFromStart, costOverall);
            }
        }

        private Vector3Int GetBestPosition(HashSet<Vector3Int> openSet, Dictionary<Vector3Int, float> costOverall)
        {
            Vector3Int bestPosition = new Vector3Int();
            float currentCost = float.PositiveInfinity;

            foreach (var position in openSet)
            {
                if (costOverall.ContainsKey(position) && costOverall[position] < currentCost)
                {
                    bestPosition = position;
                    currentCost = costOverall[position];
                }
            }

            Debug.Assert(currentCost < float.PositiveInfinity, "Failed to find best cell for A*");
            return bestPosition;
        }

        private void UpdateDirectional(
            Vector3Int delta,
            Vector3Int bestPosition,
            Vector3Int targetPosition,
            HashSet<Vector3Int> openSet,
            Dictionary<Vector3Int, Vector3Int> cameFrom,
            Dictionary<Vector3Int, float> costFromStart,
            Dictionary<Vector3Int, float> costOverall)
        {
            Vector3Int neighbour = bestPosition + delta;
            if (movementTilemap.HasTile(neighbour) && !doorsTilemap.HasTile(neighbour))
            {
                const float distanceToNeighbour = 1;
                float tentativeCostFromStart = costFromStart[bestPosition] + distanceToNeighbour;
                float neighbourScore = costFromStart.ContainsKey(neighbour) ? costFromStart[neighbour] : float.MaxValue;

                if (tentativeCostFromStart < neighbourScore)
                {
                    if (!cameFrom.ContainsKey(neighbour))
                    {
                        cameFrom.Add(neighbour, bestPosition);
                    }
                    else
                    {
                        cameFrom[neighbour] = bestPosition;
                    }

                    if (!costFromStart.ContainsKey(neighbour))
                    {
                        costFromStart.Add(neighbour, tentativeCostFromStart);
                    }
                    else
                    {
                        costFromStart[neighbour] = tentativeCostFromStart;
                    }

                    if (!costOverall.ContainsKey(neighbour))
                    {
                        costOverall.Add(neighbour, costFromStart[neighbour] + Math.Abs(targetPosition.x - neighbour.x) + Math.Abs(targetPosition.y - neighbour.y));
                    }

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }

        private void ConstructWaypoints(Vector3Int targetGridPosition, Dictionary<Vector3Int, Vector3Int> cameFrom)
        {
            while (cameFrom.ContainsKey(targetGridPosition))
            {
                waypoints.Push(grid.GetCellCenterWorld(targetGridPosition));
                targetGridPosition = cameFrom[targetGridPosition];
            }
        }

#endregion
    }
}