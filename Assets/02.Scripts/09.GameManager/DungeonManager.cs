using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager Instance;
    //플레이어 위치
    public Vector2Int playerLocation = new Vector2Int(10,10);
    public GameObject player;

    public EnemyObjectPooling enemyPool;
    //오브젝트 풀링
    private void Awake()
    {
        Instance = this;
        //player = Instantiate(player, new Vector3(800, -35, 800), Quaternion.identity);
        enemyPool = GetComponentInChildren<EnemyObjectPooling>();
    }
    private void Start()
    {
        player = GameObject.FindWithTag("player");
        player = Instantiate(player, new Vector3(800, -35, 800), Quaternion.identity);
    }
    
}
