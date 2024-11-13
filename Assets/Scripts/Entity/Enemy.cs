using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : Entity
{
    private static Vector2[] directions = new[]
    {
        new Vector2(0, 1), // up
        new Vector2(0, -1), // down
        new Vector2(1, 0), // right
        new Vector2(-1, 0) // left
    };

    private void Awake()
    {
        movement = directions[Random.Range(0, directions.Length)];
    }

    private void Update()
    {
        if (!RoomObject.Instance.CheckInBounds(this, movement))
        {
            movement = directions[Random.Range(0, directions.Length)];
        }
    }
}