using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [Range(0f, 1f)]
    public float coinDropProbability = 0.5f;
    public GameObject coinDrop;

    [Range(0f, 1f)]
    public float itemDropProbability = 0.5f;
    public GameObject itemDrop;

    [Range(0f, 1f)]
    public float questItemDropProbability = 0.5f;
    public GameObject questItemDrop;

    private float randomPoint = 2;

    public void AllDropItems()
    {
        RandomDropCoin();
        RandomDropItem();
        RandomDropQuestItem();
    }

    public void RandomDropCoin()
    {
        float roll = Random.Range(0f, 1f);
        float randomX = Random.Range(-randomPoint, randomPoint);
        float randomZ = Random.Range(-randomPoint, randomPoint);

        Vector3 spawnPoint = new Vector3(randomX, 0, randomZ);

        if (roll < coinDropProbability)
        {
            Debug.Log("코인 드롭!");
            Instantiate(coinDrop, transform.position + spawnPoint, Quaternion.identity);
        }
    }

    public void RandomDropItem()
    {
        float roll = Random.Range(0f, 1f);
        float randomX = Random.Range(-randomPoint, randomPoint);
        float randomZ = Random.Range(-randomPoint, randomPoint);

        Vector3 spawnPoint = new Vector3(randomX, 0, randomZ);
        if (roll < itemDropProbability)
        {
            Debug.Log("아이템 드롭!");
            Instantiate(itemDrop, transform.position + spawnPoint, Quaternion.identity);
        }
    }
    public void RandomDropQuestItem()
    {
        float roll = Random.Range(0f, 1f);
        float randomX = Random.Range(-randomPoint, randomPoint);
        float randomZ = Random.Range(-randomPoint, randomPoint);

        Vector3 spawnPoint = new Vector3(randomX, 0, randomZ);
        if (roll < questItemDropProbability)
        {
            Debug.Log("퀘스트 아이템 드롭!");
            Instantiate(questItemDrop, transform.position + spawnPoint, Quaternion.identity);
        }
    }
}
