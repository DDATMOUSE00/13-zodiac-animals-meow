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
    public BuffManager buffManager;
    //오브젝트 풀링
    private void Awake()
    {
        Instance = this;
        enemyPool = GetComponentInChildren<EnemyObjectPooling>();
    }
    private void Start()
    {
        //GameManager.Instance.fadeManager.BeginFadeOut(5f);
        StartCoroutine(CLoadScene());
        player = GameObject.FindWithTag("Player");
        player.transform.position = new Vector3(800, -35, 800);
        var playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.PlayerSM = playerMovement.PlayerMaxSM;
        //player = Instantiate(player, new Vector3(800, -35, 800), Quaternion.identity);
    }
    
    IEnumerator CLoadScene()
    {
        yield return YieldInstructionCache.WaitForSeconds(1.5f);
        GameManager.Instance.fadeManager.BeginFadeOut(2f);
    }
}
