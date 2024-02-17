using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


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
        GenerateTestDungeon();

        AddDoorsToAllRooms();

        AllConnecteDoors();
    }

    void GenerateTestDungeon()
    {
        // ���� ���� �����մϴ�.
        CreateTestRoom(new Vector2Int(dungeonWidth / 2, dungeonHeight / 2), StartRoom);
        Room startRoom = rooms[dungeonWidth / 2, dungeonHeight / 2];

        // ���� ���� �������� 4���� �������� �������� ���� ����
        List<Vector2Int> directions = new List<Vector2Int> { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
        foreach (Vector2Int direction in directions)
        {
            Room currentRoom = startRoom;
            int currentRoomCount = Random.Range(minRooms, maxRooms + 1);

            for (int i = 0; i < currentRoomCount; i++)
            {
                List<Vector2Int> emptyDirections = GetEmptyDirections(currentRoom.coordinates);

                if (emptyDirections.Count == 0)
                {
                    break;
                }

                Vector2Int randomDirection = emptyDirections[Random.Range(0, emptyDirections.Count)];
                Vector2Int nextRoomCoordinate = currentRoom.coordinates + randomDirection;

                if (IsCoordinateInDungeon(nextRoomCoordinate) && rooms[nextRoomCoordinate.x, nextRoomCoordinate.y] == null)
                {
                    CreateTestRoom(nextRoomCoordinate);
                    currentRoom = rooms[nextRoomCoordinate.x, nextRoomCoordinate.y];
                }
                else
                {
                    break;
                }
            }
        }

        Room furthestRoomFromStart = FindFurthestRoom(startRoom);
        ChangeRoomToBossRoom(furthestRoomFromStart);
    }

    void CreateTestRoom(Vector2Int coordinates, GameObject roomPrefab = null)
    {
        if (roomPrefab == null)
        {
            int randomIndex = Random.Range(0, RoomPrefabs.Length);
            roomPrefab = RoomPrefabs[randomIndex];
        }

        GameObject roomInstance = Instantiate(roomPrefab, new Vector3(coordinates.x * roomSize, 0, coordinates.y * roomSize), Quaternion.identity, transform);
        Room room = roomInstance.GetComponent<Room>();
        room.coordinates = coordinates;
        rooms[coordinates.x, coordinates.y] = room;
    }


    List<Vector2Int> GetEmptyDirections(Vector2Int roomCoordinate)
    {
        List<Vector2Int> emptyDirections = new List<Vector2Int>();
        List<Vector2Int> directions = new List<Vector2Int> { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

        foreach (Vector2Int direction in directions)
        {
            Vector2Int nextCoordinate = roomCoordinate + direction;

            if (IsCoordinateInDungeon(nextCoordinate) && rooms[nextCoordinate.x, nextCoordinate.y] == null)
            {
                emptyDirections.Add(direction);
            }
        }

        return emptyDirections;
    }



    Room FindFurthestRoom(Room startRoom)
    {
        Stack<Room> stack = new Stack<Room>();
        Room furthestRoom = startRoom;
        int furthestRoomDistance = 0;
        Dictionary<Room, bool> visited = new Dictionary<Room, bool>();
        visited[startRoom] = true;
        stack.Push(startRoom);

        while (stack.Count > 0)
        {
            Room currentRoom = stack.Peek();
            List<Room> unvisitedNeighbors = GetUnvisitedNeighborRooms(currentRoom, visited);

            if (unvisitedNeighbors.Count > 0)
            {
                Room selectedNeighbor = unvisitedNeighbors[Random.Range(0, unvisitedNeighbors.Count)];
                visited[selectedNeighbor] = true;
                stack.Push(selectedNeighbor);

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

        return furthestRoom;
    }

    int GetManhattanDistance(Vector2Int a, Vector2Int b)
    {
        return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
    }


    List<Room> GetUnvisitedNeighborRooms(Room room, Dictionary<Room, bool> visited)
    {
        Vector2Int[] directions = new Vector2Int[] { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
        List<Room> unvisitedNeighbors = new List<Room>();

        foreach (var direction in directions)
        {
            Vector2Int neighborCoordinate = room.coordinates + direction;
            if (IsCoordinateInDungeon(neighborCoordinate) && rooms[neighborCoordinate.x, neighborCoordinate.y] != null && !visited.ContainsKey(rooms[neighborCoordinate.x, neighborCoordinate.y]))
            {
                unvisitedNeighbors.Add(rooms[neighborCoordinate.x, neighborCoordinate.y]);
            }
        }

        return unvisitedNeighbors;
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
        if (bossRoom == null)
        {
            Debug.LogError("bossRoom is null. Cannot change to BossRoom.");
            return;
        }

        Vector2Int bossRoomCoordinate = bossRoom.coordinates;
        Destroy(bossRoom.gameObject);
        CreateBossRoom(bossRoomCoordinate, BossRoom);
    }

    void CreateBossRoom(Vector2Int coordinate, GameObject roomPrefab)
    {
        GameObject instance = Instantiate(roomPrefab, new Vector3(coordinate.x * roomSize, 0, coordinate.y * roomSize), Quaternion.identity, transform);
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
    ///Room���� door �߰��ϴ� �Լ� 
    /// </summary>

    #region Room���� door �߰�
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
        new Vector2Int(xzCoordinate.x, xzCoordinate.y + 1), // ��
        new Vector2Int(xzCoordinate.x, xzCoordinate.y - 1), // �Ʒ�
        new Vector2Int(xzCoordinate.x - 1, xzCoordinate.y), // ����
        new Vector2Int(xzCoordinate.x + 1, xzCoordinate.y)  // ������
        };

        Vector3[] doorPositions = new Vector3[]
        {
        new Vector3(room.transform.position.x, -35, room.transform.position.z + roomSize / 3), // ��
        new Vector3(room.transform.position.x, -35, room.transform.position.z - roomSize / 3), // �Ʒ�
        new Vector3(room.transform.position.x - roomSize / 3, -35, room.transform.position.z), // ����
        new Vector3(room.transform.position.x + roomSize / 3, -35, room.transform.position.z)  // ������
        };

        for (int i = 0; i < neighborCoordinates.Length; i++)
        {
            if (IsCoordinateInDungeon(neighborCoordinates[i]) && rooms[neighborCoordinates[i].x, neighborCoordinates[i].y] != null)
            {
                // doors �迭�� Door ������Ʈ�� �߰��մϴ�.
                room.doors[i] = Instantiate(Door, doorPositions[i], Quaternion.Euler(0, 90 * i, 0), room.transform);
                //room.doors[i].SetActive(false);
            }
        }
    }
    #endregion

    ///<summary>
    /// ���� ������ �ִ� �Լ�..
    ///</summary>

    #region ������ �ڵ�
    void AllConnecteDoors()
    {
        for (int x = 0; x < dungeonWidth; x++)
        {
            for (int y = 0; y < dungeonHeight; y++)
            {
                if (rooms[x, y] != null)
                {
                    ConnectDoors(rooms[x, y]);
                    rooms[x, y].SetDoorDirection();
                    rooms[x, y].Exit();
                }
            }
        }
    }

    void ConnectDoors(Room room)
    {
        int x = room.coordinates.x;
        int y = room.coordinates.y;

        // �ֺ��� ���� Ȯ��
        Room[] neighbors = new Room[4] {
        rooms[x, y+1], // ��
        rooms[x, y-1], // �Ʒ�
        rooms[x-1, y], // ����
        rooms[x+1, y]  // ������
    };

        for (int i = 0; i < neighbors.Length; i++)
        {
            Room neighborRoom = neighbors[i];
            if (neighborRoom != null)
            {
                // �ֺ��� ���� �ִ��� Ȯ�κ���
                Door door1 = room.doors[i] != null ? room.doors[i].GetComponent<Door>() : null;
                Door door2 = null;

                // �ݴ� ������ ���� ������ ��
                switch (i)
                {
                    case 0: // ��
                        door2 = neighborRoom.doors[1] != null ? neighborRoom.doors[1].GetComponent<Door>() : null;
                        break;
                    case 1: // �Ʒ�
                        door2 = neighborRoom.doors[0] != null ? neighborRoom.doors[0].GetComponent<Door>() : null;
                        break;
                    case 2: // ����
                        door2 = neighborRoom.doors[3] != null ? neighborRoom.doors[3].GetComponent<Door>() : null;
                        break;
                    case 3: // ������
                        door2 = neighborRoom.doors[2] != null ? neighborRoom.doors[2].GetComponent<Door>() : null;
                        break;
                }

                // ���� targetPoint�� ���� ����
                if (door1 != null && door2 != null)
                {
                    door1.targetPoint = door2.gameObject;
                    door2.targetPoint = door1.gameObject;
                }
            }
        }
    }
    #endregion
}
