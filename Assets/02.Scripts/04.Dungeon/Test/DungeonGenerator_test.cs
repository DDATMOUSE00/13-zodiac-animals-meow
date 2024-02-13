//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UIElements.Experimental;

//public class DungeonGenerator_test : MonoBehaviour
//{
//    public GameObject startRoomPrefab;
//    public GameObject bossRoomPrefab;
//    public GameObject[] roomPrefabs;
//    public GameObject doorPrefab;
//    public Vector2Int dungeonSize;
//    public int roomSize = 50;

//    private Room[,] rooms;

//    public int minRooms;
//    public int maxRooms;
//    [SerializeField] private int currentRoomCount;

//    private void Start()
//    {
//        GenerateDungeon();
//    }

//    private void GenerateDungeon()
//    {
//        rooms = new Room[dungeonSize.x, dungeonSize.y];

//        System.Random rand = new System.Random();
//        currentRoomCount = rand.Next(minRooms, maxRooms + 1);
//        Debug.Log("Current Room Count: " + currentRoomCount);

//        Vector2Int startPos = new Vector2Int(UnityEngine.Random.Range(0, dungeonSize.x), UnityEngine.Random.Range(0, dungeonSize.y));
//        CreateRoom(new Vector2Int(startPos.x, startPos.y), startRoomPrefab);
//        rooms[startPos.x, startPos.y].visited = true;
//        int generatedRoomCount = 1;

//        Stack<Room> stack = new Stack<Room>();
//        stack.Push(rooms[startPos.x, startPos.y]);
//        Debug.Log("Pushed room at " + rooms[startPos.x, startPos.y].position + ", stack count: " + stack.Count);

//        while (stack.Count > 0 && generatedRoomCount < currentRoomCount)
//        {
//            Room room = stack.Pop();
//            Debug.Log("Popped room at " + room.position + ", stack count: " + stack.Count);
//            foreach (Room adjacentRoom in GetAdjacentRooms(room))
//            {
//                if (!adjacentRoom.visited)
//                {
//                    GameObject randomRoomPrefab = roomPrefabs[UnityEngine.Random.Range(0, roomPrefabs.Length)];
//                    CreateRoom(new Vector2Int(adjacentRoom.position.x, adjacentRoom.position.y), randomRoomPrefab);
//                    rooms[adjacentRoom.position.x, adjacentRoom.position.y].visited = true;
//                    stack.Push(rooms[adjacentRoom.position.x, adjacentRoom.position.y]);
//                    Debug.Log("Pushed room at " + adjacentRoom.position + ", stack count: " + stack.Count);
//                    generatedRoomCount++;
//                    Debug.Log("Created room at " + adjacentRoom.position + ", total rooms: " + generatedRoomCount);
//                }
//            }
//        }

//        if (generatedRoomCount < minRooms)
//        {
//            Debug.Log(generatedRoomCount);
//            return;
//        }

//        Room furthestRoom = GetFurthestRoom(rooms[startPos.x, startPos.y]);
//        Vector2Int bossRoomPosition = new Vector2Int(furthestRoom.position.x, furthestRoom.position.z);
//        Destroy(furthestRoom.gameObject);
//        CreateRoom(bossRoomPosition, bossRoomPrefab);
//        generatedRoomCount++;
//        Debug.Log("Created boss room at " + bossRoomPosition + ", total rooms: " + generatedRoomCount);
//    }

//    private void CreateRoom(Vector2Int position, GameObject roomPrefab)
//    {
//        GameObject roomInstance = Instantiate(roomPrefab, new Vector3(position.x * roomSize, 0, position.y * roomSize), Quaternion.identity);
//        Room room = roomInstance.GetComponent<Room>();
//        room.position = new Vector3Int(position.x, 0, position.y);
//        CreateDoors(room);
//        rooms[position.x, position.y] = room;

//        // 추가: 방이 생성된 좌표를 로그로 출력
//        Debug.Log("Room created at position: " + position);
//    }

//    private void CreateDoors(Room room)
//    {
//        room.doors = new GameObject[4];

//        for (int i = 0; i < room.doors.Length; i++)
//        {
//            room.doors[i] = Instantiate(doorPrefab, room.transform);
//        }

//        room.doors[0].transform.localPosition = new Vector3(0, 0, roomSize / 2);
//        room.doors[1].transform.localPosition = new Vector3(0, 0, -roomSize / 2);
//        room.doors[2].transform.localPosition = new Vector3(-roomSize / 2, 0, 0);
//        room.doors[3].transform.localPosition = new Vector3(roomSize / 2, 0, 0);

//        room.doors[0].transform.localRotation = Quaternion.Euler(0, 0, 0);
//        room.doors[1].transform.localRotation = Quaternion.Euler(0, 180, 0);
//        room.doors[2].transform.localRotation = Quaternion.Euler(0, -90, 0);
//        room.doors[3].transform.localRotation = Quaternion.Euler(0, 90, 0);
//    }

//    private Room[] GetAdjacentRooms(Room room)
//    {
//        List<Room> adjacentRooms = new List<Room>();

//        Vector2Int position = new Vector2Int(room.position.x, room.position.z);

//        if (position.y + 1 < dungeonSize.y && rooms[position.x, position.y + 1] != null)
//        {
//            adjacentRooms.Add(rooms[position.x, position.y + 1]);
//        }

//        if (position.y - 1 >= 0 && rooms[position.x, position.y - 1] != null)
//        {
//            adjacentRooms.Add(rooms[position.x, position.y - 1]);
//        }

//        if (position.x - 1 >= 0 && rooms[position.x - 1, position.y] != null)
//        {
//            adjacentRooms.Add(rooms[position.x - 1, position.y]);
//        }

//        if (position.x + 1 < dungeonSize.x && rooms[position.x + 1, position.y] != null)
//        {
//            adjacentRooms.Add(rooms[position.x + 1, position.y]);
//        }

//        return adjacentRooms.ToArray();
//    }

//    private Room GetFurthestRoom(Room startRoom)
//    {
//        Dictionary<Room, int> distance = new Dictionary<Room, int>();
//        Queue<Room> queue = new Queue<Room>();

//        foreach (Room room in rooms)
//        {
//            if (room != null)
//            {
//                distance[room] = int.MaxValue;
//            }
//        }

//        distance[startRoom] = 0;
//        queue.Enqueue(startRoom);

//        while (queue.Count > 0)
//        {
//            Room room = queue.Dequeue();
//            foreach (Room adjacentRoom in GetAdjacentRooms(room))
//            {
//                if (distance[adjacentRoom] > distance[room] + 1)
//                {
//                    distance[adjacentRoom] = distance[room] + 1;
//                    queue.Enqueue(adjacentRoom);
//                }
//            }
//        }

//        Room furthestRoom = startRoom;
//        int maxDistance = 0;
//        foreach (KeyValuePair<Room, int> pair in distance)
//        {
//            if (pair.Value > maxDistance)
//            {
//                maxDistance = pair.Value;
//                furthestRoom = pair.Key;
//            }
//        }

//        return furthestRoom;
//    }
//}
