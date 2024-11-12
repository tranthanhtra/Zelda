using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Utils.Constants;

public class RoomObject : Singleton<RoomObject>
{
    [SerializeField] private Tilemap tilemap;

    [SerializeField] private TileBase[] ground;
    [SerializeField] private TileBase[] corners;
    [SerializeField] private TileBase[] leftTile;
    [SerializeField] private TileBase[] rightTile;
    [SerializeField] private TileBase[] topTile;
    [SerializeField] private TileBase[] bottomTile;
    [SerializeField] private TileBase[] doorTilesLeftOpen, doorTilesLeftClose;
    [SerializeField] private TileBase[] doorTilesRightOpen, doorTilesRightClose;
    [SerializeField] private TileBase[] doorTilesUpOpen, doorTilesUpClose;
    [SerializeField] private TileBase[] doorTilesDownOpen, doorTilesDownClose;

    [SerializeField] private TileBase emptyTile;
    // Start is called before the first frame update

    private Vector2Int sceneTileSize;
    private TileType[] roomTileData;
    private Door[] doorWays;

    void Start()
    {
        sceneTileSize = new Vector2Int(RoomSize.x + 4, RoomSize.y + 4);
        roomTileData = new TileType[sceneTileSize.x * sceneTileSize.y];
        var mostLeft = 2;
        var mostRight = RoomSize.x + 2;
        var mostUp = RoomSize.y + 2;
        var mostDown = 2;

        // Generate ground and wall tiles for the room
        for (var y = mostDown; y < mostUp + 1; y++)
        {
            for (int x = mostLeft; x < mostRight + 1; x++)
            {
                // Determine the tile type based on the position
                TileBase tile = GetTile(x, y);

                // Set the tile in the room data and tilemap
                roomTileData[y * sceneTileSize.x + x] = GetTileType(x, y);
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }

// Helper function to get the tile based on position
        TileBase GetTile(int x, int y)
        {
            // Check if the tile is a corner
            if (IsCorner(x, y))
            {
                return GetCornerTile(x, y);
            }
            // Check if the tile is on an edge
            else if (IsEdge(x, y))
            {
                return GetEdgeTile(x, y);
            }
            // The tile is not on an edge or corner
            else
            {
                return ground[Random.Range(0, ground.Length)];
            }
        }

// Helper function to get the tile type based on position
        TileType GetTileType(int x, int y)
        {
            // Check if the tile is on an edge or corner
            if (IsEdge(x, y) || IsCorner(x, y))
            {
                return TileType.Wall;
            }
            // The tile is not on an edge or corner
            else
            {
                return TileType.Ground;
            }
        }

// Helper function to check if a tile is a corner
        bool IsCorner(int x, int y)
        {
            return (x == mostLeft && y == mostUp) ||
                   (x == mostLeft && y == mostDown) ||
                   (x == mostRight && y == mostUp) ||
                   (x == mostRight && y == mostDown);
        }

// Helper function to get the corner tile
        TileBase GetCornerTile(int x, int y)
        {
            if (x == mostLeft && y == mostUp)
            {
                return corners[0];
            }
            else if (x == mostLeft && y == mostDown)
            {
                return corners[3];
            }
            else if (x == mostRight && y == mostUp)
            {
                return corners[1];
            }
            else
            {
                return corners[2];
            }
        }

// Helper function to check if a tile is on an edge
        bool IsEdge(int x, int y)
        {
            return x == mostLeft || x == mostRight || y == mostUp || y == mostDown;
        }

// Helper function to get the edge tile
        TileBase GetEdgeTile(int x, int y)
        {
            if (x == mostLeft)
            {
                return leftTile[Random.Range(0, leftTile.Length)];
            }
            else if (x == mostRight)
            {
                return rightTile[Random.Range(0, rightTile.Length)];
            }
            else if (y == mostUp)
            {
                return topTile[Random.Range(0, topTile.Length)];
            }
            else
            {
                return bottomTile[Random.Range(0, bottomTile.Length)];
            }
        }

        //Generate door: calculate door position then 
        doorWays = CalculateDoor();
        foreach (var doorWay in doorWays)
        {
            Vector2Int[] doorTiles = { };

            switch (doorWay.direction)
            {
                case DoorDirection.Left:
                    doorTiles = new Vector2Int[]
                    {
                        new Vector2Int(mostLeft - 1, mostDown + RoomSize.y / 2 + 1),
                        new Vector2Int(mostLeft, mostDown + RoomSize.y / 2 + 1),
                        new Vector2Int(mostLeft, mostDown + RoomSize.y / 2),
                        new Vector2Int(mostLeft - 1, mostDown + RoomSize.y / 2),
                    };

                    break;
                case DoorDirection.Right:
                    doorTiles = new Vector2Int[]
                    {
                        new Vector2Int(mostRight, mostDown + RoomSize.y / 2 + 1),
                        new Vector2Int(mostRight + 1, mostDown + RoomSize.y / 2 + 1),
                        new Vector2Int(mostRight + 1, mostDown + RoomSize.y / 2),
                        new Vector2Int(mostRight, mostDown + RoomSize.y / 2),
                    };
                    break;
                case DoorDirection.Down:
                    doorTiles = new Vector2Int[]
                    {
                        new Vector2Int(mostLeft + RoomSize.x / 2, mostDown),
                        new Vector2Int(mostLeft + RoomSize.x / 2 + 1, mostDown),
                        new Vector2Int(mostLeft + RoomSize.x / 2 + 1, mostDown - 1),
                        new Vector2Int(mostLeft + RoomSize.x / 2, mostDown - 1),
                    };

                    break;
                case DoorDirection.Up:
                    doorTiles = new Vector2Int[]
                    {
                        new Vector2Int(mostLeft + RoomSize.x / 2, mostUp + 1),
                        new Vector2Int(mostLeft + RoomSize.x / 2 + 1, mostUp + 1),
                        new Vector2Int(mostLeft + RoomSize.x / 2 + 1, mostUp),
                        new Vector2Int(mostLeft + RoomSize.x / 2, mostUp),
                    };

                    break;
            }

            doorWay.tiles = doorTiles;
        }

        SetupDoors();
        tilemap.transform.position = new Vector3(-RoomSize.x / 2 - mostLeft, -RoomSize.y / 2 - mostDown, 0);
    }

    #region Public Methods

    public bool CheckInBounds(Entity entity, Vector2 direction)
    {
        Vector2 checkpoint = ((Vector2)entity.transform.position + Vector2.down *  entity.Collider2D.size.y/3) +
                             new Vector2(direction.x * entity.Collider2D.size.x / 2,
                                 direction.y * entity.Collider2D.size.y / 6) -
                             (Vector2)tilemap.transform.position;

        var tilePosition = new Vector2Int(Mathf.FloorToInt(checkpoint.x), Mathf.FloorToInt(checkpoint.y));

        return roomTileData[tilePosition.y * sceneTileSize.x + tilePosition.x] == TileType.Ground;
    }

    #endregion

    #region Private Methods

    private Door[] CalculateDoor()
    {
        return new Door[]
        {
            new Door(new Vector2Int[4], DoorDirection.Left),
            new Door(new Vector2Int[4], DoorDirection.Right),
            new Door(new Vector2Int[4], DoorDirection.Down),
            new Door(new Vector2Int[4], DoorDirection.Up),
        };
    }

    private void SetupDoors()
    {
        foreach (var door in doorWays)
        {
            TileBase[] tileArray = new TileBase[4];

            switch (door.direction)
            {
                case DoorDirection.Left:
                    tileArray = door.isOpen ? doorTilesLeftOpen : doorTilesLeftClose;
                    break;
                case DoorDirection.Right:
                    tileArray = door.isOpen ? doorTilesRightOpen : doorTilesRightClose;
                    break;
                case DoorDirection.Down:
                    tileArray = door.isOpen ? doorTilesDownOpen : doorTilesDownClose;
                    break;
                case DoorDirection.Up:
                    tileArray = door.isOpen ? doorTilesUpOpen : doorTilesUpClose;
                    break;
            }

            for (int i = 0; i < door.tiles.Length; i++)
            {
                tilemap.SetTile(new Vector3Int(door.tiles[i].x, door.tiles[i].y, 0), tileArray[i]);
                roomTileData[door.tiles[i].y * sceneTileSize.x + door.tiles[i].x] = TileType.Door;
            }
        }
    }

    #endregion
}

public enum TileType
{
    Empty,
    Ground,
    Wall,
    Door,
}