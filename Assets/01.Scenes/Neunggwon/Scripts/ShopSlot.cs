using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ShopSlot : MonoBehaviour
{
    [Header("#ItemDataInfo")]
    public Item itemData; //아이템 데이터를 받는다!
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI itemCost;

    [Header("#InputFeild")]
    [SerializeField] private GameObject inputFeild;
    public InputField inputField;
    public string inputNum = null;

    private void Start()
    {
        Setting();
    }

    public Item ItemData(Item _itemData)
    {
        itemData = _itemData;
        return itemData;
    }

    void Setting()
    {
        itemName.text = itemData.Name;
        itemIcon.sprite = itemData.Icon;
        itemDescription.text = itemData.Description;
        //itemCost = itemData.Cost;

        if (0 == 0) // itemType에 따라 세팅해야 할 거 추가 (임시임)
        {

        }
    }
    private bool ThisEquipItam() //test
    {
        //아이템이 무기인지 확인...
        return false;
    }

    public void ButtonBuy()
    {
        if (!ThisEquipItam())
        {
            //갯수 입력 UI 뽕!
            inputFeild.SetActive(true);
        }
        else
        {
            //inventory에 쏙!
            //InventoryManager
            //AddItem(this.itemData);
            Debug.Log($"Item : {itemName} Buy");
        }
    }

    //public void ButtonSell()
    //{
    //    if (!ThisEquipItam())
    //    {
    //        //갯수 입력 UI 뽕!
    //    }
    //    else
    //    {
    //        //inventory에 쏙!
    //    }
    //}
    

    public void InputNum()
    {
        int inputNum = int.Parse(inputField.text);
        ///<summary>
        ///임시..
        ///플레이어 골드 확인
        /// </summary>
        
        //if (PlayerStatus.Gold >= itemData.Cost * inputNum)
        //{
        //    PlayerStatus.Gold -= (itemData.Cost * inputNum)
        //    AddItem(this.itemData, inputNum)
        //}
        //else
        //{
        //    Debug.Log($"플레이어 골드 부족!!");
        //}

    }
}
