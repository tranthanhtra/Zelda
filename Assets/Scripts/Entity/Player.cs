using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    private static readonly int Swing = Animator.StringToHash("TriggerSwing");
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        GoDown();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwingSword();
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            GoUp();
            if (RoomObject.Instance.CheckInBounds(this, Vector2.up))
                // Handle up key press
                Go();
            else
                Stop();
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            GoDown();
            if (RoomObject.Instance.CheckInBounds(this, Vector2.down))
                Go();
            else
                Stop();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            GoLeft();
            if (RoomObject.Instance.CheckInBounds(this, Vector2.left))
                // Handle left key press
                Go();
            else
                Stop();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            GoRight();
            if (RoomObject.Instance.CheckInBounds(this, Vector2.right))
                // Handle right key press
                Go();
            else
                Stop();
        }

        else
        {
            Stop();
        }
    }

    private void SwingSword()
    {
        Stop();
        animator.SetTrigger(Swing);
    }

    public override void TakeDamage(int damage)
    {
        if (isImmune) return;
        isImmune = true;
        Debug.Log("hit");
        base.TakeDamage(damage);
        StartCoroutine(Flicker());
    }

    public override void Die()
    {
        Debug.Log("game over");
    }

    private IEnumerator Flicker()
    {
        for (int i = 0; i < 6; i++)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.1f);
        }

        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(0.2f);

        isImmune = false;
    }
}