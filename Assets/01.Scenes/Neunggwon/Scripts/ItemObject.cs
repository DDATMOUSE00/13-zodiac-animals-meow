using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public Item item;

    [SerializeField] private Rigidbody rigid;
    [SerializeField] private BoxCollider collider;

    [SerializeField] private GameObject interactionText_And_ItemName;
    [SerializeField] private GameObject player;

    [SerializeField] private float distance;
    [SerializeField] private float range = 8f;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
        //interactionText_And_ItemName = GetComponentInChildren<GameObject>();
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        TextMeshPro TextUI = interactionText_And_ItemName.GetComponent<TextMeshPro>();
        TextUI.text = $"상호작용 [E] 키 \n {item.itemName}";
        Debug.Log(TextUI.text);
        interactionText_And_ItemName.SetActive(false);
    }

    void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < range)
        {
            interactionText_And_ItemName.SetActive(true);
        }
        else
        {
            interactionText_And_ItemName.SetActive(false);
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            ItemManager.I.AddItem(item);
            Destroy(gameObject);
        }
    }
}
