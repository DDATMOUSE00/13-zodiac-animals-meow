using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject UI;
    public TMP_Text gold;


    private void Start()
    {
        gold.text = (GameManager.Instance.player.GetComponent<PlayerGold>().Gold).ToString();
    }
    public void SaveInventoryData()
    {
        UI.SetActive(false);
      //  Debug.Log(ItemManager.I.itemDic);
     //   DataManager.I.SaveJsonData(ItemManager.I.itemDic, "ItemData");

    }

    public void LoadInventoryData()
    {
        gold.text = (GameManager.Instance.player.GetComponent<PlayerGold>().Gold).ToString();
        UI.SetActive(true);

        //ItemManager.I.itemDic = DataManager.I.LoadJsonData<Dictionary<int, int>>("ItemData");
    }
}
