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
        //GenerateDungeon();
        GenerateTestDungeon();

        AddDoorsToAllRooms();
    }

    void GenerateTestDungeon()
    {
        // 시작 방을 생성합니다.
        CreateTestRoom(new Vector2Int(dungeonWidth / 2, dungeonHeight / 2), StartRoom);
        Room startRoom = rooms[dungeonWidth / 2, dungeonHeight / 2];

        // 시작 방을 기준으로 4가지 방향으로 무작위로 방을 생성
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

    // 맨해튼 거리 계산 함수
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














    //void GenerateDungeon()
    //{
    //    // 던전의 중앙에 StartRoom을 생성합니다.
    //    Vector2Int startCoordinate = new Vector2Int(dungeonWidth / 2, dungeonHeight / 2);
    //    CreateRoom(startCoordinate, StartRoom);

    //    // DFS 알고리즘을 사용하여 방을 생성합니다.
    //    Stack<Room> stack = new Stack<Room>();
    //    Room startRoom = rooms[startCoordinate.x, startCoordinate.y];
    //    startRoom.visited = true;
    //    stack.Push(startRoom);
    //    Room furthestRoom = startRoom;
    //    int furthestRoomDistance = 0;
    //    int roomCount = 1;

    //    while (stack.Count > 0 && roomCount < currentRoomCount)
    //    {
    //        Room currentRoom = stack.Peek();
    //        List<Vector2Int> unvisitedNeighbors = GetUnvisitedNeighborCoordinates(currentRoom);

    //        if (unvisitedNeighbors.Count > 0)
    //        {
    //            Vector2Int selectedNeighborCoordinate = unvisitedNeighbors[Random.Range(0, unvisitedNeighbors.Count)];
    //            CreateRoom(selectedNeighborCoordinate, RoomPrefabs[Random.Range(0, RoomPrefabs.Length)]);
    //            Room selectedNeighbor = rooms[selectedNeighborCoordinate.x, selectedNeighborCoordinate.y];
    //            selectedNeighbor.visited = true;
    //            stack.Push(selectedNeighbor);
    //            roomCount++;

    //            int currentDistance = stack.Count;
    //            if (currentDistance > furthestRoomDistance)
    //            {
    //                furthestRoom = selectedNeighbor;
    //                furthestRoomDistance = currentDistance;
    //            }
    //        }
    //        else
    //        {
    //            stack.Pop();
    //        }
    //    }

    //    // 가장 먼 방을 BossRoom으로 변경합니다.
    //    ChangeRoomToBossRoom(furthestRoom);
    //    AddDoorsToAllRooms();
    //}

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
        GameObject instance = Instantiate(roomPrefab, new Vector3(coordinate.x * roomSize, 0, coordinate.y * roomSize), Quaternion.identity,transform);
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
    ///<summary>
    /// 문을 연결해 주는 함수..
    ///</summary>

}
