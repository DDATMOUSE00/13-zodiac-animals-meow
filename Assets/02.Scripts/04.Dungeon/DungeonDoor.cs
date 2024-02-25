using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonDoor : MonoBehaviour
{
    public string sceneName;
    private void Update()
    {
        sceneName = SceneName();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            SceneManager.LoadScene(SceneName());
        }
    }

    public string SceneName()
    {
        return sceneName = DungeonScrollView.Instance.selectDenguonSlot.sceneName;
    }
}
