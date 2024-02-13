using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DungeonGenerator_test2 : MonoBehaviour
{
    public GameObject StartRoom;
    public GameObject BossRoom;
    public GameObject[] RoomPrefabs;
    public GameObject Door;
    public int roomSize = 50;
    public int minRooms;
    public int maxRooms;
    public int dungeonWidth = 10;
    public int dungeonHeight = 10;
    [SerializeField] private int currentRoomCount;
    private Room[,] rooms;


    private void Start()
    {
        currentRoomCount = Random.Range(minRooms, maxRooms);
        rooms = new Room[dungeonWidth, dungeonHeight];
        GenerateDungeon();
    }
    void GenerateDungeon()
    {
        // 던전의 중앙에 StartRoom을 생성합니다.
        Vector2Int startCoordinate = new Vector2Int(dungeonWidth / 2, dungeonHeight / 2);
        CreateRoom(startCoordinate, StartRoom);

        // DFS 알고리즘을 사용하여 방을 생성합니다.
        Stack<Room> stack = new Stack<Room>();
        Room startRoom = rooms[startCoordinate.x, startCoordinate.y];
        startRoom.visited = true;
        stack.Push(startRoom);
        Room furthestRoom = startRoom;
        int furthestRoomDistance = 0;
        int roomCount = 1;

        while (stack.Count > 0 && roomCount < currentRoomCount)
        {
            Room currentRoom = stack.Peek();
            List<Vector2Int> unvisitedNeighbors = GetUnvisitedNeighborCoordinates(currentRoom);

            if (unvisitedNeighbors.Count > 0)
            {
                Vector2Int selectedNeighborCoordinate = unvisitedNeighbors[Random.Range(0, unvisitedNeighbors.Count)];
                CreateRoom(selectedNeighborCoordinate, RoomPrefabs[Random.Range(0, RoomPrefabs.Length)]);
                Room selectedNeighbor = rooms[selectedNeighborCoordinate.x, selectedNeighborCoordinate.y];
                selectedNeighbor.visited = true;
                stack.Push(selectedNeighbor);
                roomCount++;

                int currentDistance = stack.Count;
                if (currentDistance > furthestRoomDistance)
                {
                    furthestRoom = selectedNeighbor;
                    furthestRoomDistance = currentDistance;
                }
            }
            else
            {
                stack.Pop();
            }
        }

        // 가장 먼 방을 BossRoom으로 변경합니다.
        ChangeRoomToBossRoom(furthestRoom);
        AddDoorsToAllRooms();
    }

    void CreateRoom(Vector2Int coordinate, GameObject roomPrefab)
    {
        if (rooms[coordinate.x, coordinate.y] == null)
        {
            GameObject instance = Instantiate(roomPrefab, new Vector3(coordinate.x * roomSize, 0, coordinate.y * roomSize), Quaternion.identity);
            Room room = instance.GetComponent<Room>();
            room.coordinates = coordinate;
            room.visited = false;
            rooms[coordinate.x, coordinate.y] = room;
        }
    }

    List<Vector2Int> GetUnvisitedNeighborCoordinates(Room room)
    {
        Vector2Int[] directions = new Vector2Int[] { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
        List<Vector2Int> unvisitedNeighbors = new List<Vector2Int>();

        foreach (var direction in directions)
        {
            Vector2Int neighborCoordinate = room.coordinates + direction;
            if (IsCoordinateInDungeon(neighborCoordinate) && rooms[neighborCoordinate.x, neighborCoordinate.y] == null)
            {
                unvisitedNeighbors.Add(neighborCoordinate);
            }
        }

        return unvisitedNeighbors;
    }

    void ChangeRoomToBossRoom(Room bossRoom)
    {
        Vector2Int bossRoomCoordinate = bossRoom.coordinates;
        Destroy(bossRoom.gameObject);
        CreateBossRoom(bossRoomCoordinate, BossRoom);
    }

    void CreateBossRoom(Vector2Int coordinate, GameObject roomPrefab)
    {
        GameObject instance = Instantiate(roomPrefab, new Vector3(coordinate.x * roomSize, 0, coordinate.y * roomSize), Quaternion.identity);
        Room room = instance.GetComponent<Room>();
        room.coordinates = coordinate;
        room.visited = false;
        rooms[coordinate.x, coordinate.y] = room;

    }

    bool IsCoordinateInDungeon(Vector2Int coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < rooms.GetLength(0) && coordinate.y >= 0 && coordinate.y < rooms.GetLength(1);
    }


    ///<summary>
    ///Room마다 door 추가하는 함수 
    /// </summary>

    void AddDoorsToAllRooms()
    {
        for (int x = 0; x < dungeonWidth; x++)
        {
            for (int y = 0; y < dungeonHeight; y++)
            {
                if (rooms[x, y] != null)
                {
                    CheckAndAddNeighboringRooms(rooms[x, y]);
                }
            }
        }
    }

    void CheckAndAddNeighboringRooms(Room room)
    {
        Vector2Int xzCoordinate = room.coordinates;
        Vector2Int[] neighborCoordinates = new Vector2Int[]
        {
        new Vector2Int(xzCoordinate.x, xzCoordinate.y + 1), // 위
        new Vector2Int(xzCoordinate.x, xzCoordinate.y - 1), // 아래
        new Vector2Int(xzCoordinate.x - 1, xzCoordinate.y), // 왼쪽
        new Vector2Int(xzCoordinate.x + 1, xzCoordinate.y)  // 오른쪽
        };

        Vector3[] doorPositions = new Vector3[]
        {
        new Vector3(room.transform.position.x, 0, room.transform.position.z + roomSize / 3), // 위
        new Vector3(room.transform.position.x, 0, room.transform.position.z - roomSize / 3), // 아래
        new Vector3(room.transform.position.x - roomSize / 3, 0, room.transform.position.z), // 왼쪽
        new Vector3(room.transform.position.x + roomSize / 3, 0, room.transform.position.z)  // 오른쪽
        };

        for (int i = 0; i < neighborCoordinates.Length; i++)
        {
            if (IsCoordinateInDungeon(neighborCoordinates[i]) && rooms[neighborCoordinates[i].x, neighborCoordinates[i].y] != null)
            {
                // doors 배열에 Door 오브젝트를 추가합니다.
                room.doors[i] = Instantiate(Door, doorPositions[i], Quaternion.Euler(0, 90 * i, 0), room.transform);
            }
        }
    }


}
