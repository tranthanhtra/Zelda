using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Entity : MonoBehaviour
{
    public int hp;
    public float speed;
    protected Vector2 movement;

    protected Rigidbody2D rigidbody2D;
    protected BoxCollider2D _collider2D;
    protected Animator animator;
    private static readonly int Velocity = Animator.StringToHash("Velocity");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");

    public BoxCollider2D Collider2D => _collider2D;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    protected virtual void FixedUpdate()
    {
        transform.Translate(movement * (speed * Time.deltaTime));
        animator.SetFloat(Horizontal, movement.x);
        animator.SetFloat(Vertical, movement.y);
        animator.SetFloat(Velocity, movement.sqrMagnitude);
    }

    protected void GoLeft()
    {
        movement = Vector2.left;
    }

    protected void GoRight()
    {
        movement = Vector2.right;
    }

    protected void GoUp()
    {
        movement = Vector2.up;
    }

    protected void GoDown()
    {
        movement = Vector2.down;
    }

    protected void Stop()
    {
        movement = Vector2.zero;
    }
}