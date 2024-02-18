using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{

    public RoomData roomData;
    public Vector2Int coordinates;
    public bool visited = false;
    public GameObject[] doors = new GameObject[4];
    public GameObject[] enemys;
    private GameObject selecteEnemy;
    public int enemysCount;

    public bool playerchecking; //플레이어가 있는지
    public bool claerchecking; //클리어 한 방인지
    public bool enemySpwan;

    private void Start()
    {
        //SetDoorDirection();
        if (roomData.roomType == RoomType.StartRoom)
        {
            Enter();
            enemySpwan = true;
        }
    }
    private void Update()
    {
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        enemysCount = enemys.Length;

        if (DungeonManager.Instance.playerLocation == coordinates)
        {
            playerchecking = true;
            if (playerchecking && !claerchecking) //플레이어 가 있고 클리어 한 방이 아니면 적 생성
            {
                if (!enemySpwan)
                {
                    Exit();
                    for (int i = 0; i < Random.Range(roomData.minEnemyCount, roomData.maxEnemyCount); i ++)
                    {
                        SpawnEnemy();
                    }
                    enemySpwan = true;
                }
                else
                {
                    if (enemysCount == 0)
                    {
                        if (!claerchecking)
                        {
                            Invoke("Enter", 2);
                            claerchecking = true;
                        }
                    }
                }
            }
            else if (playerchecking && claerchecking)
            {
                Invoke("Enter", 2);
            }
            //else
            //{
            //    //DungeonManager.Instance.playerLocation = coordinates;

            //}
        }
        else if (DungeonManager.Instance.playerLocation != coordinates)
        {
            Exit();
            playerchecking = false;
        }
    }

    public void SetDoorDirection()
    {
        Vector2Int[] doorDirections = new Vector2Int[4] {
        new Vector2Int(0, 1),   // 위쪽 문
        new Vector2Int(0, -1),  // 아래쪽 문
        new Vector2Int(-1, 0),   // 왼쪽 문
        new Vector2Int(1, 0)   // 오른쪽 문
    };
        for (int i = 0; i < doors.Length; i++)
        {
            GameObject doorObject = doors[i];
            if (doorObject != null)
            {
                Door door = doorObject.GetComponent<Door>();
                if (door != null)
                {
                    switch (i)
                    {
                        case 0:
                            door.SetDirection(doorDirections[i]);
                            break;
                        case 1:
                            door.SetDirection(doorDirections[i]);
                            break;
                        case 2:
                            door.SetDirection(doorDirections[i]);
                            break;
                        case 3:
                            door.SetDirection(doorDirections[i]);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    private void SpawnEnemy()
    {
        int randomX = Random.Range(-25, 25);
        int randomZ = Random.Range(-25, 25);
        //Vector3 spawnPoint = new Vector3(randomX, 0, randomZ);
        Vector3 spawnPoint = new Vector3(randomX, 0, randomZ);

        Debug.Log("적 소환");

        //Test
        //데이터의 있는 몬스터의 이름을 찾아 생성하도록 해보자!!
        if (roomData.roomType == RoomType.DungeonRoom)
        {
            GameObject selectEnemy1 = roomData.enemys[Random.Range(0,roomData.enemys.Length)];
            var selectEnemy2 = DungeonManager.Instance.enemyPool;

            for (int i = 0; i < selectEnemy2.prefabs.Length; i++)
            {
                if (selectEnemy1 == selectEnemy2.prefabs[i])
                {
                    GameObject enemy = DungeonManager.Instance.enemyPool.Get(i);
                    enemy.transform.position = this.transform.position + spawnPoint;
                }
            }
        }
        else if (roomData.roomType == RoomType.BossRoom)
        {
            GameObject bossEnemy = roomData.Boss;
            Instantiate(bossEnemy, this.transform.position, Quaternion.identity);
        }
        
    }
    // 이 방에 들어올 때 호출됩니다.
    public void Enter()
    {
        foreach (GameObject door in doors)
        {
            if (door != null)
            {
                door.SetActive(true);
            }
        }
    }

    // 이 방에서 나갈 때 호출됩니다.
    public void Exit()
    {
        foreach (GameObject door in doors)
        {
            if (door != null)
            {
                door.SetActive(false);
            }
        }
    }
}
