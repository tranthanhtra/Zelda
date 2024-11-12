using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (RoomObject.Instance.CheckInBounds(this, Vector2.up))
                // Handle up key press
                GoUp();
            else
                Stop();
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (RoomObject.Instance.CheckInBounds(this, Vector2.down))
                GoDown();
            else
                Stop();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (RoomObject.Instance.CheckInBounds(this, Vector2.left))
                // Handle left key press
                GoLeft();
            else
                Stop();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (RoomObject.Instance.CheckInBounds(this, Vector2.right))
                // Handle right key press
                GoRight();
            else
                Stop();
        }
        else
        {
            Stop();
        }
    }
}