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
                SceneManager.LoadScene("Village-NeunggwonScene");
            }
            else
            {
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
}
