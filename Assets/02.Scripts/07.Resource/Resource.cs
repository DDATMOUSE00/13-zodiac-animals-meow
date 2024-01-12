using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private GameObject itemprefab;
    [SerializeField] private GameObject randomItemprefab;

    private bool isHit = false;

    private Color originalColor;
    private Color hitColor = Color.red;


    private float ResourceHP = 2.0f;

    private void Start()
    {
        originalColor = GetComponent<Renderer>().material.color;
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("¶§·È´Ù.");
        //if (collision.gameObject.CompareTag("PlayerWeapon") && !isHit)
        //{
        //    Debug.Log("¶§·È´Ù.");
        //    if (ResourceHP > 0)
        //    {
        //        isHit = true;
        //        ResourceHP--;
        //        GetComponent<Renderer>().material.color = hitColor;
        //        Debug.Log(ResourceHP);

        //        StartCoroutine(CoResetColorAfterDelay(0.2f));
        //    }
        //    else
        //    {
        //        Die();
        //    }
        //}
    }

    private void Die()
    {
        gameObject.SetActive(false);
        ResourceHP = 2.0f;
        DropItem();
        RandomDropItem();
    }

    private IEnumerator CoResetColorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isHit = false;
        ResetResource();
    }

    public void ResetResource()
    {
        GetComponent<Renderer>().material.color = originalColor;
    }

    private void DropItem()
    {
        Instantiate(itemprefab, transform.position, Quaternion.identity);
    }

    private void RandomDropItem()
    {
        int RandomDrop = Random.Range(0, 3);
        if (RandomDrop == 0)
        {
            Instantiate(randomItemprefab, transform.position, Quaternion.identity);
        }
    }
}
