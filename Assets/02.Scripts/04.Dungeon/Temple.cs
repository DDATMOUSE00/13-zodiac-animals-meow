using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temple : MonoBehaviour
{
    [SerializeField] private GameObject temple;
    //public List<Item> items = new List<Item>();
    public List<FindItem> findItems = new List<FindItem>();
    [SerializeField] private int receivedCount = 0;
    [SerializeField] private int resourcesNeededCount = 10;
    private int totalvalue;
    public bool cheking = false;
    public Button yesButton;
    public Button noButton;

    private void Start()
    {
        totalvalue = resourcesNeededCount + (resourcesNeededCount * receivedCount);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (temple.activeInHierarchy)
            {
                temple.SetActive(false);
            }
            else
            {
                temple.SetActive(true);
            }
        }
    }
    public void YesButton()
    {
        Debug.Log(totalvalue);
        cheking = true;
        for (int i = 0; i < findItems.Count; i++)
        {
            if (!ItemManager.I.ChekInventoryItem(findItems[i].itemData.id, totalvalue))
            {
                cheking = false;
                Debug.Log("아이템 부족");

                break;
            }
        }

        if (cheking)
        {
            Debug.Log("아이템 확인");
            for (int i = 0; i < findItems.Count; i++)
            {
                ItemManager.I.RemoveItem(findItems[i].itemData.id, totalvalue);
            }

            receivedCount++;
            totalvalue = resourcesNeededCount + (resourcesNeededCount * receivedCount);
            for (int i = 0; i < findItems.Count; i++)
            {
                findItems[i].findCount.text = totalvalue.ToString("##개");
                Debug.Log(findItems[i].findCount.text);

            }
        }
    }

    public void RamdomBuffe()
    {

    }


    public void NoButton()
    {
        Debug.Log("NoButton");
        temple.SetActive(false);
    }
    
}
