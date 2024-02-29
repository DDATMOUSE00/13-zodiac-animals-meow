using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public Vector2Int direction;
    public GameObject targetPoint; //이동 할 위치
    public BuffManager buffManager;

    [Header("#VilligeDoor")]
    //public GameObject villageDoor;
    public bool villageDoorChicking;

    private void Start()
    {
        buffManager = GameObject.FindAnyObjectByType<BuffManager>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (villageDoorChicking)
            {
                buffManager.AllDestroyBuff();
                var playerHealth = GameManager.Instance.player.GetComponent<PlayerHealth>();
                var playerSM = GameManager.Instance.player.GetComponent<PlayerMovement>();
                //StartCoroutine(GameManager.Instance.fadeManager.CLoadSceneFadeIn(1f, "Village_FINAL"));
                //GameManager.Instance.fadeManager.BeginFadeIn(1f);
                //SceneManager.LoadScene("Village_FINAL");
                //GameManager.Instance.player.transform.position = new Vector3(0, 0, -20);
                StartCoroutine(CLoadScene());
                playerHealth.ApplyHealthBuff(0);
                playerSM.ApplyMovementBuff(0);
            }
            else
            {
                //StartCoroutine(CNextRoomFadeIn(1f));
                other.transform.position = targetPoint.transform.position;
                DungeonManager.Instance.playerLocation += direction;
                
                Debug.Log(DungeonManager.Instance.playerLocation);
            }
        }
    }

    public void SetDirection(Vector2Int dir)
    {
        direction = dir;
    }

    IEnumerator CLoadScene()
    {
        //StartCoroutine(GameManager.Instance.fadeManager.CLoadSceneFadeIn(1f, "Village_FINAL"));
        GameManager.Instance.fadeManager.BeginFadeIn(1f);
        yield return YieldInstructionCache.WaitForSeconds(1f);
        //yield return YieldInstructionCache.WaitForSeconds(2f);
        SceneManager.LoadScene("Village_FINAL");
        GameManager.Instance.player.transform.position = new Vector3(0, 0, -20);
        GameManager.Instance.fadeManager.BeginFadeOut(1f);
        yield return null;

    }

}
