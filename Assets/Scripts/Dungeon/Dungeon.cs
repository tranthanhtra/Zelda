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
    public bool isTransitioning { get; private set; }

    private Vector3 toCurrentPosition;
    private Vector3 toNextPosition;


    private void Start()
    {
        currentRoom = Instantiate(roomPrefab, currentContainer);
    }

    public void MoveToNextRoom(Vector2Int direction)
    {
        isTransitioning = true;
        toCurrentPosition = currentContainer.transform.position;
        Debug.Log(toCurrentPosition);
        toNextPosition = currentContainer.transform.position -
                         new Vector3(Constants.RoomSize.x * direction.x * 1.5f,
                             Constants.RoomSize.y * direction.y * 1.5f, 0);
        nextContainer.transform.position = currentContainer.transform.position +
                                           new Vector3(Constants.RoomSize.x * direction.x * 1.5f,
                                               Constants.RoomSize.y * direction.y * 1.5f, 0);
        if (nextRoom == null)
        {
            nextRoom = Instantiate(roomPrefab, nextContainer);
        }

        StartCoroutine(Transition());
    }

    private IEnumerator Transition()
    {
        while ((nextContainer.transform.position - toCurrentPosition).magnitude > 0.1f)
        {
            currentContainer.position = Vector3.MoveTowards(currentContainer.transform.position, toNextPosition, Time.deltaTime * 10);

            nextContainer.position = Vector3.MoveTowards(nextContainer.transform.position, toCurrentPosition, Time.deltaTime * 10);
            yield return new WaitForEndOfFrame();
        }

        nextContainer.position = toCurrentPosition;

        Debug.Log("Done Transition");
        Debug.Log(nextContainer.position);

        currentContainer = nextContainer;
        currentRoom = nextRoom;
        yield return new WaitForEndOfFrame();
        currentRoom.StartAllEnemy();
    }
}