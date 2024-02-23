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
    public GameObject selectBuffChekUI;

    public BuffManager buffManager;

    private void Start()
    {
        totalvalue = resourcesNeededCount + (resourcesNeededCount * receivedCount);
        for (int i = 0; i < findItems.Count; i++)
        {
            findItems[i].findCount.text = totalvalue.ToString("##개");
        }
        selectBuffChekUI.SetActive(false);
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
            for (int i = 0; i < findItems.Count; i++)
            {
                ItemManager.I.RemoveItem(findItems[i].itemData.id, totalvalue);
            }
            buffManager.ApplyBuff();
            StartCoroutine(BuffChekUI());
            receivedCount++;
            totalvalue = resourcesNeededCount + (resourcesNeededCount * receivedCount);
            for (int i = 0; i < findItems.Count; i++)
            {
                findItems[i].findCount.text = totalvalue.ToString("##개");
            }
        }
    }

    public void NoButton()
    {
        Debug.Log("NoButton");
        temple.SetActive(false);
    }

    IEnumerator BuffChekUI()
    {
        var BuffChekUI = selectBuffChekUI.GetComponent<BuffChekUI>();
        var _selectBuff = buffManager.selectBuff.GetComponent<BaseBuff>();
        BuffChekUI.SelectBuffInfo(_selectBuff);
        selectBuffChekUI.SetActive(true);
        yield return YieldInstructionCache.WaitForSeconds(1.5f);
        selectBuffChekUI.SetActive(false);
    }


}
