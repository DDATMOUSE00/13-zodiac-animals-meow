using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestNPC : MonoBehaviour
{
    [SerializeField] private GameObject interactionText;
    [SerializeField] private GameObject player;

    [SerializeField] private float distance;
    [SerializeField] private float range = 8f;

    //[Header("#Shop")]
    //[SerializeField] private
    private void Start()
    {
        interactionText.SetActive(false);
    }
    void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < range)
        {
            interactionText.SetActive(true);
        }
        else
        {
            interactionText.SetActive(false);
        }
    }
}
