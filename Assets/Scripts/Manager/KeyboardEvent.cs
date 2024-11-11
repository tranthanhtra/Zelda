using System;
using UnityEngine;

public class KeyboardEvent : Singleton<KeyboardEvent>
{
    public event Action<KeyCode> OnKeyPressed;
    public event Action OnKeyReleased;

    private void Update()
    {
        // Track up, down, left, and right key presses
    }
}