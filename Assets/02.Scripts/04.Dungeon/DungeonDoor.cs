using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonDoor : MonoBehaviour
{
    public string sceneName;
    private void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(GameManager.Instance.fadeManager.CLoadSceneFadeIn(1f, sceneName));
            StartCoroutine(CLoadScene());
            //GameManager.Instance.fadeManager.BeginFadeIn(1f);
            //SceneManager.LoadScene(sceneName);
        }
    }
    IEnumerator CLoadScene()
    {
        GameManager.Instance.fadeManager.BeginFadeIn(1f);
        yield return YieldInstructionCache.WaitForSeconds(1f);
        yield return YieldInstructionCache.WaitForSeconds(0.2f);
        SceneManager.LoadScene(sceneName);
    }

    public string SceneName()
    {
        return sceneName = DungeonScrollView.Instance.selectDenguonSlot.sceneName;
    }
}
