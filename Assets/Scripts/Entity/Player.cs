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
            // Handle up key press
            Debug.Log( "up");
            GoUp();
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            // Handle down key press
            GoDown();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Handle left key press
            GoLeft();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            // Handle right key press
            GoRight();
        }
    }
}