﻿using Celeste.Managers;
using Celeste.Tilemaps;
using Robbi.Levels.Elements;
using Robbi.Tilemaps.Tiles;
using System.Collections.Generic;
using UnityEngine;

namespace Robbi.Runtime
{
    [AddComponentMenu("Robbi/Runtime/Doors Manager")]
    [RequireComponent(typeof(BoxCollider2D))]
    public class DoorsManager : NamedManager
    {
        #region Properties and Fields

        public TilemapValue doorsTilemap;
        public BoxCollider2D boundingBox;
        public Celeste.Events.Event boosterUsedEvent;

        private List<Door> doors = new List<Door>();

        #endregion

        #region IEnvironmentManager

        public void Initialize(IEnumerable<Door> _doors)
        {
            doors.Clear();
            doors.AddRange(_doors);

            foreach (Door door in doors)
            {
                door.Initialize(doorsTilemap.Value);
            }

            Vector3Int doorsGridSize = doorsTilemap.Value.size;
            Vector3Int doorsOrigin = doorsTilemap.Value.origin;
            boundingBox.size = new Vector2(doorsGridSize.x, doorsGridSize.y);
            boundingBox.offset = new Vector2(doorsOrigin.x + doorsGridSize.x * 0.5f, doorsOrigin.y + doorsGridSize.y * 0.5f);
        }

        public void Cleanup()
        {
            doors.Clear();
        }

        #endregion

        #region Unity Methods

        private void OnValidate()
        {
            if (boundingBox == null)
            {
                boundingBox = GetComponent<BoxCollider2D>();
            }
        }

        private void Update()
        {
            doorsTilemap.Value.RefreshAllTiles();
        }

        #endregion

        #region Callbacks

        public void OnGameObjectInput(Vector3 inputLocation)
        {
            Vector3Int doorGridPosition = doorsTilemap.Value.WorldToCell(inputLocation);
            if (!doorsTilemap.Value.HasTile(doorGridPosition))
            {
                // Not a valid location in the tilemap
                return;
            }

            if (TryToggle(doorGridPosition))
            {
                boosterUsedEvent.Raise();
            }
        }

        #endregion

        #region Doors Methods

        public void OpenDoor(Door door)
        {
            door.Open(doorsTilemap.Value);
        }

        public void CloseDoor(Door door)
        {
            door.Close(doorsTilemap.Value);
        }

        public void ToggleDoor(Door door)
        {
            door.Toggle(doorsTilemap.Value);
        }

        private bool TryToggle(Vector3Int location)
        {
            foreach (Door door in doors)
            {
                if (door.position == location)
                {
                    door.Toggle(doorsTilemap.Value);
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}