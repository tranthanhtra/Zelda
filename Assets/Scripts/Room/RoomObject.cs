using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;

public class RoomObject : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    [SerializeField] private TileBase[] ground;
    [SerializeField] private TileBase[] corners;
    [SerializeField] private TileBase[] leftTile;

    // Start is called before the first frame update
    void Start()
    {
        var mostLeft = -Constants.RoomSize.x / 2 - 1;
        var mostRight = Constants.RoomSize.x / 2 + 1;
        var mostUp = Constants.RoomSize.y / 2 + 1;
        var mostDown = -Constants.RoomSize.y / 2 - 1;
        for (var y = mostDown; y < mostUp + 1; y++)
        {
            for (int x = mostLeft; x < mostRight + 1; x++)
            {
                TileBase tile;
                if (x == mostLeft)
                {
                    if (y == mostUp)
                    {
                        tile = corners[0];
                    }
                    else
                    {
                        tile = leftTile[Random.Range(0, leftTile.Length)];
                    }
                }
                else
                {
                    tile = ground[Random.Range(0, ground.Length)];
                }

                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}