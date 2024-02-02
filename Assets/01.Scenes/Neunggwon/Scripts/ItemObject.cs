using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public Item item;

    [SerializeField] private Rigidbody rigid;
    [SerializeField] private BoxCollider collider;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ItemManager.I.AddItem(item);
            Destroy(gameObject);
        }
    }
}
