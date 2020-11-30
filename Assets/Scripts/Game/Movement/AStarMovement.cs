using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Movement
{
    public class AStarMovement
    {
        #region Properties and Fields

        public Tilemap MovementTilemap { get; set; }
        public Tilemap DoorsTilemap { get; set; }

        public bool HasStepsToNextWaypoint { get { return stepsToNextWaypoint.Count > 0; } }
        public Vector3 NextStep { get { return stepsToNextWaypoint.Peek(); } }

        private HashSet<Vector3Int> openSet = new HashSet<Vector3Int>();
        private Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int, Vector3Int>();
        private Dictionary<Vector3Int, float> costFromStart = new Dictionary<Vector3Int, float>();
        private Dictionary<Vector3Int, float> costOverall = new Dictionary<Vector3Int, float>();
        private List<Vector3Int> newWaypoints = new List<Vector3Int>();
        private Stack<Vector3> stepsToNextWaypoint = new Stack<Vector3>();

        #endregion

        #region Pathfinding

        public void CalculateGridSteps(Vector3 startingPosition, Vector3Int targetPosition)
        {
            CalculateGridSteps(MovementTilemap.LocalToCell(startingPosition), targetPosition);
        }

        public void CalculateGridSteps(Vector3Int startingPosition, Vector3Int targetPosition)
        {
            openSet.Clear();
            cameFrom.Clear();
            costFromStart.Clear();
            costOverall.Clear();

            openSet.Add(startingPosition);
            costFromStart.Add(startingPosition, 0);
            costOverall.Add(startingPosition, Math.Abs(targetPosition.x - startingPosition.x) + Math.Abs(targetPosition.y - startingPosition.y));

            while (openSet.Count > 0)
            {
                Vector3Int bestPosition = GetBestPosition(openSet, costOverall);
                if (bestPosition == targetPosition)
                {
                    ConstructGridSteps(targetPosition);
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

            ConstructGridSteps(targetPosition);
        }

        public void CompleteStep()
        {
            if (HasStepsToNextWaypoint)
            {
                stepsToNextWaypoint.Pop();
            }
            else
            {
                Debug.LogAssertion("Trying to complete a step with no steps remaining");
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
            if (MovementTilemap.HasTile(neighbour))
            {
                float distanceToNeighbour = DoorsTilemap.HasTile(neighbour) ? 1000.0f : 1.0f;
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

        private void ConstructGridSteps(Vector3Int targetGridPosition)
        {
            newWaypoints.Clear();
            stepsToNextWaypoint.Clear();

            while (cameFrom.ContainsKey(targetGridPosition))
            {
                newWaypoints.Add(targetGridPosition);
                targetGridPosition = cameFrom[targetGridPosition];
            }

            for (int i = newWaypoints.Count - 1; i >= 0; --i)
            {
                if (DoorsTilemap.HasTile(newWaypoints[i]))
                {
                    for (int j = i; j >= 0; --j)
                    {
                        newWaypoints.RemoveAt(j);
                    }

                    break;
                }
            }

            foreach (Vector3Int waypoint in newWaypoints)
            {
                stepsToNextWaypoint.Push(MovementTilemap.GetCellCenterWorld(waypoint));
            }
        }

        #endregion
    }
}
