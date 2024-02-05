using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private GameObject itemprefab;
    [SerializeField] private GameObject randomItemprefab;

    private bool isHit = false;

    private Color originalColor;

    [SerializeField] private int ResourceHP;
    private int maxResourceHP = 2;


    public void Hit()
    {
       // Debug.Log("ÀÚ¿ø ‹šÂî");
        if (!isHit)
        {
            if (ResourceHP > 0)
            {
                isHit = true;
                ResourceHP--;
                //GetComponent<Renderer>().material.color = hitColor;
             //   Debug.Log(ResourceHP);

                StartCoroutine(CoResetColorAfterDelay());
            }
            else
            {
                Die();
            }
        }
    }

    private void OnEnable()
    {
        ResourceHP = maxResourceHP;
    }

    private void Die()
    {
        gameObject.SetActive(false);
        DropItem();
        //RandomDropItem();
    }

    private IEnumerator CoResetColorAfterDelay()
    {
        yield return YieldInstructionCache.WaitForSeconds(0.2f);
        isHit = false;
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
