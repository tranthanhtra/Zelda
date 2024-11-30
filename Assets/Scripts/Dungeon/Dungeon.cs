using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Dungeon : Singleton<Dungeon>
{
    [SerializeField] private RoomObject roomPrefab;
    [SerializeField] private Transform currentContainer;
    [SerializeField] private Transform nextContainer;
    private RoomObject currentRoom;
    private RoomObject nextRoom;

    public RoomObject CurrentRoom => currentRoom;
    public RoomObject NextRoom => nextRoom;
    

    private void Start()
    {
        currentRoom = Instantiate(roomPrefab, currentContainer);
    }

    public void MoveToNextRoom(Vector2Int direction)
    {
        nextContainer.transform.position = currentContainer.transform.position +
                                           new Vector3(Constants.RoomSize.x * direction.x * 1.5f,
                                               Constants.RoomSize.y * direction.y * 1.5f, 0);
        if (nextRoom == null)
        {
            nextRoom = Instantiate(roomPrefab, nextContainer);
        }
        nextRoom.StopAllEnemy();
    }
}