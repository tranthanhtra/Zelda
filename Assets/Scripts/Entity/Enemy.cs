using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using Random = UnityEngine.Random;

public class Enemy : Entity
{
    [SerializeField] private SpriteLibrary spriteLibrary;

    private static Vector2Int[] directions = new[]
    {
        new Vector2Int(0, 1), // up
        new Vector2Int(0, -1), // down
        new Vector2Int(1, 0), // right
        new Vector2Int(-1, 0) // left
    };

    private float changeDirectionTime;
    private float timeSinceChangedDirection;

    private void Awake()
    {
        movement = directions[Random.Range(0, directions.Length)];
        changeDirectionTime = Random.Range(1f, 4f);
        speed = Random.Range(1, 1.5f);
        // Go();
    }

    private void Update()
    {
        if (velocity <= 0.00001f) return;
        if (!Dungeon.Instance.CurrentRoom.CheckInBounds(this, movement))
        {
            movement = directions[Random.Range(0, directions.Length)];
            timeSinceChangedDirection = 0;
        }
        else
        {
            if (timeSinceChangedDirection > changeDirectionTime)
            {
                movement = directions[Random.Range(0, directions.Length)];
                timeSinceChangedDirection = 0;
                changeDirectionTime = Random.Range(1, 4);
            }
            else
            {
                timeSinceChangedDirection += Time.deltaTime;
            }
        }
    }

    public void Setup(SpriteLibraryAsset sprite)
    {
        spriteLibrary.spriteLibraryAsset = sprite;
    }

    public override void Die()
    {
        gameObject.SetActive(false);
        Dungeon.Instance.CurrentRoom.CheckOpenDoor();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<Player>();
            player.TakeDamage(1);
        }

        if (other.gameObject.CompareTag("Sword"))
        {
            TakeDamage(1);
        }
    }
}