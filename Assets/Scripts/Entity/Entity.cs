using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public int hp;
    public float speed;

    private Rigidbody2D rigidbody2D;
    private BoxCollider2D _collider2D;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<BoxCollider2D>();
    }

    protected void GoLeft()
    {
        // transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
        if (RoomObject.Instance.CheckInBounds((Vector2)transform.position + _collider2D.offset, _collider2D.size,
                Vector2.left))
            transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
    }

    protected void GoRight()
    {
        // transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
        if (RoomObject.Instance.CheckInBounds((Vector2)transform.position + _collider2D.offset, _collider2D.size,
                Vector2.right))

            transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
    }

    protected void GoUp()
    {
        if (RoomObject.Instance.CheckInBounds((Vector2)transform.position + _collider2D.offset, _collider2D.size,
                Vector2.up))
            transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
    }

    protected void GoDown()
    {
        if (RoomObject.Instance.CheckInBounds((Vector2)transform.position + _collider2D.offset, _collider2D.size,
                Vector2.down))
            transform.position = new Vector3(transform.position.x, transform.position.y - speed, transform.position.z);
    }
}