using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Door
{
    public Vector2Int[] tiles;
    public DoorDirection direction;
    public bool isOpen = false;

    public Door(Vector2Int[] tiles, DoorDirection direction)
    {
        this.tiles = tiles;
        this.direction = direction;
    }
}

public enum DoorDirection
{
   Left,
   Right,
   Up,
   Down
}