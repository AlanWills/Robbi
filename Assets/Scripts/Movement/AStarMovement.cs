﻿using Robbi.Tilemaps;
using System;
using System.Collections;
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
        public Tilemap PortalsTilemap { get; set; }

        public float MovementWeight { get; set; } = 1;
        public float DoorsWeight { get; set; } = 1000;
        public float PortalsWeight { get; set; } = 0;
        
        public bool HasStepsToNextWaypoint { get { return stepsToNextWaypoint.Count > 0; } }
        public Vector3 NextStep { get { return stepsToNextWaypoint.Peek(); } }

        private HashSet<Vector3Int> openSet = new HashSet<Vector3Int>();
        private Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int, Vector3Int>();
        private Dictionary<Vector3Int, float> costFromStart = new Dictionary<Vector3Int, float>();
        private Dictionary<Vector3Int, float> costOverall = new Dictionary<Vector3Int, float>();
        private List<Vector3Int> tilesToNextWaypoint = new List<Vector3Int>();
        private Stack<Vector3> stepsToNextWaypoint = new Stack<Vector3>();

        #endregion

        #region Pathfinding

        public void CalculateGridSteps(Vector3 startingPosition, Vector3Int targetPosition)
        {
            CalculateGridStepsImpl(startingPosition, targetPosition, stepsToNextWaypoint);
        }

        public Stack<Vector3> CalculateGridStepsWithoutCaching(Vector3 startingPosition, Vector3Int targetPosition)
        {
            Stack<Vector3> stepsToNextWaypoint = new Stack<Vector3>();
            CalculateGridStepsImpl(startingPosition, targetPosition, stepsToNextWaypoint);
            return stepsToNextWaypoint;
        }

        public void CalculateGridStepsImpl(Vector3 startingPosition, Vector3Int targetPosition, Stack<Vector3> stepsToNextWaypoint)
        {
            Vector3Int startingTilePosition = MovementTilemap.WorldToCell(startingPosition);

            if (!HasStepsToNextWaypoint)
            {
                // We recalculated with no existing steps underway
                // Go ahead and recalculate with impunity, everything will be gucci
                CalculateCosts(startingTilePosition, targetPosition);
                ConstructGridSteps(targetPosition, stepsToNextWaypoint);

                return;
            }

            // We are currently executing a step
            // If we recalculate now, the progress we've made will be lost and we could possibly skip this step entirely
            // So we cache some information
            Vector3 firstStep = NextStep;
            Vector3Int firstStepTilePosition = MovementTilemap.WorldToCell(firstStep);

            // Calculate the new steps
            CalculateCosts(startingTilePosition, targetPosition);
            ConstructGridSteps(targetPosition, stepsToNextWaypoint);

            // Now we check to see if our step needed completing
            // If we are on the same tile as the discarded executing step from the perspective of the tilemap
            // But we hadn't completed the step (actual positions different)
            // And we haven't recalculated we need to do that step anyway
            if (startingTilePosition == firstStepTilePosition &&
                firstStep != startingPosition &&
                (!HasStepsToNextWaypoint || stepsToNextWaypoint.Peek() != firstStep))
            {
                // We re-add the step so that it's remainder is completed
                stepsToNextWaypoint.Push(firstStep);
            }
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

        private void CalculateCosts(Vector3Int startingPosition, Vector3Int targetPosition)
        {
            openSet.Clear();
            cameFrom.Clear();
            costFromStart.Clear();
            costOverall.Clear();

            openSet.Add(startingPosition);
            costFromStart.Add(startingPosition, 0);
            costOverall.Add(startingPosition, Math.Abs(targetPosition.x - startingPosition.x) + Math.Abs(targetPosition.y - startingPosition.y));

            if (!MovementTilemap.HasTile(startingPosition) && !PortalsTilemap.HasTile(startingPosition))
            {
                // Movement capability = portal or movement entry in tilemap
                // We are on a tile without movement capabilities, so we cannot move off of it
                return;
            }

            while (openSet.Count > 0)
            {
                Vector3Int bestPosition = GetBestPosition(openSet, costOverall);
                if (bestPosition == targetPosition)
                {
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
            if (MovementTilemap.HasTile(neighbour))
            {
                float distanceToNeighbour = MovementWeight;
                
                if (DoorsTilemap.HasClosedDoor(neighbour))
                {
                    distanceToNeighbour += DoorsWeight;
                }

                if (PortalsTilemap.HasTile(neighbour))
                {
                    distanceToNeighbour += PortalsWeight;
                }

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

        private void ConstructGridSteps(Vector3Int targetGridPosition, Stack<Vector3> tileCentresToNextWaypoint)
        {
            tilesToNextWaypoint.Clear();
            tileCentresToNextWaypoint.Clear();

            while (cameFrom.ContainsKey(targetGridPosition))
            {
                tilesToNextWaypoint.Add(targetGridPosition);
                targetGridPosition = cameFrom[targetGridPosition];
            }

            // If we encounter a closed door for a step, remove all steps after and including that step 
            // (since it's not traversable, but we want to move as close to the waypoint as possible
            for (int i = tilesToNextWaypoint.Count - 1; i >= 0; --i)
            {
                if (DoorsTilemap.HasClosedDoor(tilesToNextWaypoint[i]))
                {
                    for (int j = i; j >= 0; --j)
                    {
                        tilesToNextWaypoint.RemoveAt(j);
                    }

                    break;
                }
            }

            foreach (Vector3Int waypoint in tilesToNextWaypoint)
            {
                tileCentresToNextWaypoint.Push(MovementTilemap.GetCellCenterWorld(waypoint));
            }
        }

        #endregion
    }
}
