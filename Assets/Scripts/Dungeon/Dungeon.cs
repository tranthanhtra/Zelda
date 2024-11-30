using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Dungeon : Singleton<Dungeon>
{
    [SerializeField] private RoomObject roomPrefab;
    private RoomObject currentRoom;
    private RoomObject nextRoom;
    
    public RoomObject CurrentRoom => currentRoom;
    public RoomObject NextRoom => nextRoom;

    private void Start()
    {
        currentRoom = Instantiate(roomPrefab);
    }

    public void MoveToNextRoom(Vector2Int direction)
    {
        if (nextRoom == null)
            nextRoom = Instantiate(roomPrefab);
                nextRoom.transform.position =  (Vector2)currentRoom.transform.position + (Vector2)Constants.RoomSize * 1.5f * direction;
    }
}