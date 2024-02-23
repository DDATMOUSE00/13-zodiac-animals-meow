using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class Test_Room : MonoBehaviour
{
    public RoomData roomData;
    public Vector2Int coordinates;
    public bool visited = false;
    public GameObject[] doors = new GameObject[4];
    public GameObject[] enemys;
    public int enemysCount;

    public bool playerchecking; //�÷��̾ �ִ���
    public bool claerchecking; //Ŭ���� �� ������
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
            if (playerchecking && !claerchecking) //�÷��̾� �� �ְ� Ŭ���� �� ���� �ƴϸ� �� ����
            {
                if (!enemySpwan)
                {
                    Exit();
                    for (int i = 0; i < Random.Range(roomData.minEnemyCount, roomData.maxEnemyCount); i++)
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
        new Vector2Int(0, 1),   // ���� ��
        new Vector2Int(0, -1),  // �Ʒ��� ��
        new Vector2Int(-1, 0),   // ���� ��
        new Vector2Int(1, 0)   // ������ ��
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
        int randomX = Random.Range(-30, 30);
        int randomZ = Random.Range(-30, 30);
        Vector3 spawnPoint = new Vector3(randomX, 0, randomZ);


        Debug.Log("�� ��ȯ");
        GameObject enemy = DungeonManager.Instance.enemyPool.Get(Random.Range(0, DungeonManager.Instance.enemyPool.prefabs.Length));
        
        //Test
        //�������� �ִ� ������ �̸��� ã�� �����ϵ��� �غ���!!
        if (roomData.roomType == RoomType.DungeonRoom)
        {
            for (int i = 0; i < roomData.enemys.Length; i++)
            {
                for (int j = 0; j < DungeonManager.Instance.enemyPool.prefabs.Length; j++)
                {
                    if (roomData.enemys[i].gameObject.name == DungeonManager.Instance.enemyPool.prefabs[j].name)
                    {
                        DungeonManager.Instance.enemyPool.Get(j);
                        enemy.transform.position = this.transform.position + spawnPoint;
                    }
                }
            }
        }
        else if (roomData.roomType == RoomType.BossRoom)
        {
            GameObject bossEnemy = roomData.Boss;
            Instantiate(bossEnemy, this.transform.position, Quaternion.identity);
        }
    }
    // �� �濡 ���� �� ȣ��˴ϴ�.
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

    // �� �濡�� ���� �� ȣ��˴ϴ�.
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
