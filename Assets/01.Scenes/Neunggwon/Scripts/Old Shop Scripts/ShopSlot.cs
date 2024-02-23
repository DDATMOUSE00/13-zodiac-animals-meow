using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;
//using static UnityEngine.Rendering.DebugUI;


public class ShopSlot : MonoBehaviour
{
    [Header("#bool")]
    public bool IsSellShop;
    public string type;
    //public string String_sellCount;
    public int Int_Count = 1;
    [Header("#ItemDataInfo")]
    public Item itemData; //아이템 데이터를 받는다!
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI itemPrice;
    //public TextMeshProUGUI itemCount;
    public TMP_Text itemCount;
    public Button shopSlotButton;

    private void Start()
    {
        Setting();
    }


    public void Setting()
    {
        itemName.text = itemData.itemName;
        itemIcon.sprite = itemData.icon;
        itemDescription.text = itemData.itemDescription;
        if (IsSellShop) //판매
        {
            //판매 아이템은 Count 및 가격 조정
           // Debug.Log("IsSellShop_Setting");
            itemPrice.text = (itemData.price / 2).ToString("#,##0G");
            shopSlotButton.onClick.AddListener(ButtonSell);

            itemCount.text = Int_Count.ToString();
            type = "sell";
        }
        else  //구매
        {
            //구매 아이템은 아이템의 데이터 그대로 받기!
            itemPrice.text = itemData.price.ToString("#,##0G");
            shopSlotButton.onClick.AddListener(ButtonBuy);

            itemCount.text = string.Empty;
            type = "buy";
        }
        //Debug.Log($"{type}");
        bool textActive = Int_Count > 1;
        itemCount.gameObject.SetActive(textActive);
    }

    public void Clear()
    {
        itemPrice.text = string.Empty;
        itemCount.text = string.Empty;

        Destroy(gameObject);
    }

    private bool ThisEquipItam() //test
    {
        if (itemData.type == ItemType.Weapon)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ButtonBuy()
    {
        Shop.Instance.SelectItem(itemData);
        Shop.Instance.SelectSlot(this);
        if (!ThisEquipItam())
        {
            Shop.Instance.BuyInputField();
            //Debug.Log($"Item : {itemData.id} Buy {itemData.type}");
        }
        else
        {
            ItemManager.I.AddItem(itemData);
            //Debug.Log($"Item : {itemData.id} Buy {itemData.type}");
        }
    }

    public void ButtonSell()
    {
        Shop.Instance.SelectItem(itemData);
        Shop.Instance.SelectSlot(this);
        Debug.Log(Shop.Instance.selectShopSlot.itemData.id);
        if (!ThisEquipItam())
        {
            Shop.Instance.SellInputField();
            //갯수 입력 UI 뽕!
        }
        else
        {
            //inventory에서 찾아서 버리기
            //ItemManager.I.UpdateBundle(itemData.id, 1, type);
            //Shop.Instance.RemoveItem(itemData.id, 1);
            Shop.Instance.RemoveSlot1(itemData.id, 1);
            Debug.Log($"Item : {itemData.id} Sell");
        }

        if (Int_Count <= 0)
        {
            Destroy(gameObject);
        }
    }
}
