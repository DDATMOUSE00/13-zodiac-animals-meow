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
                StartCoroutine(GameManager.Instance.fadeManager.CLoadSceneFadeIn(1f, "Village_FINAL"));
                //GameManager.Instance.fadeManager.BeginFadeIn(1f);
                //SceneManager.LoadScene("Village_FINAL");
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

    IEnumerator CNextRoomFadeIn(float FadeTime)
    {
        //other.transform.position = targetPoint.transform.position;
        GameManager.Instance.fadeManager.BeginFadeIn(FadeTime);
        yield return YieldInstructionCache.WaitForSeconds(FadeTime);
        yield return YieldInstructionCache.WaitForSeconds(0.2f);
        StartCoroutine(CNextRoomFadeOut(FadeTime));
    }

    IEnumerator CNextRoomFadeOut(float FadeTime)
    {
        int _FadeTime = 1;
        GameManager.Instance.fadeManager.BeginFadeOut(_FadeTime);
        yield return null;
    }

}
