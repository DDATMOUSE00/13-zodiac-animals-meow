using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonGenerator1 : MonoBehaviour
{
    public float width;
    public float height;

    public int minRooms;
    public int maxRooms;
    public int totalRooms;

    Vector3 roomCoordinate = new Vector3();
    Vector3 newRoomCoordinate = new Vector3();

    public List<Vector3> direction = new List<Vector3> { new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 0), new Vector2(-1, 0) };
    public List<Vector3> newRooms = new List<Vector3>();
    public List<GameObject> roomPrefabs = new List<GameObject> ();

    public GameObject startRoom;
    public GameObject BossRoom;

    bool doneGettingCoordinates = false;
    private void Start()
    {
        totalRooms = Random.Range(minRooms, maxRooms);

        roomCoordinate = new Vector3(0,0,0);
        newRooms.Add(roomCoordinate);

        RoomCoordinates();
    }

    private void Update()
    {
        if (doneGettingCoordinates)
        {
            CreateRooms();
            doneGettingCoordinates = false;
        }
    }

    void LoadRoom()
    {
        int dir = Random.Range(0,direction.Count);
        newRoomCoordinate = new Vector3(roomCoordinate.x + width * direction[dir].x, roomCoordinate.y, roomCoordinate.z + width * direction[dir].y);
        newRooms.Add(newRoomCoordinate);

        Vector2 lastDir = direction[dir];
        //direction.Add(lastDir);
    }

    void RoomCoordinates()
    {
        while(newRooms.Count < totalRooms)
        {
            for (int i = newRooms.Count; i < totalRooms; i++)
            {
                LoadRoom();

                roomCoordinate = newRoomCoordinate;
            }
            newRooms = newRooms.Distinct().ToList();
        }
        if (newRooms.Count == totalRooms)
        {
            doneGettingCoordinates = true;
        }
    }

    void CreateRooms()
    {
        GameObject newRoom;

        for (int i = 0; i < totalRooms; i++)
        {
            if(i == 0)
            {
                newRoom = Instantiate(startRoom, newRooms[i], Quaternion.identity);
                newRoom.transform.parent = transform;
            }
            //else if (i == Mathf.RoundToInt(totalRooms /2))
            else if (i == totalRooms - 1)
            {
                newRoom = Instantiate(BossRoom, newRooms[i], Quaternion.identity);
                newRoom.transform.parent = transform;
            }
            else
            {
                int rand = Random.Range(0, roomPrefabs.Count);
                newRoom = Instantiate(roomPrefabs[rand], newRooms[i], Quaternion.identity);
                newRoom.transform.parent = transform;
            }
        }
    }
}
