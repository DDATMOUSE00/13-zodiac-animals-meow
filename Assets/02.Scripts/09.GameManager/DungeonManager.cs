using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager Instance;
    //�÷��̾� ��ġ
    public Vector2Int playerLocation = new Vector2Int(10,10);
    public GameObject Player;

    public EnemyObjectPooling enemyPool;
    //������Ʈ Ǯ��
    private void Awake()
    {
        Instance = this;
        //Player = Instantiate(Player, new Vector3(800, -35, 800), Quaternion.identity);
        enemyPool = GetComponentInChildren<EnemyObjectPooling>();
    }
    private void Start()
    {
        //Player = Instantiate(Player, new Vector3(800,-35,800), Quaternion.identity);
    }
    
}
