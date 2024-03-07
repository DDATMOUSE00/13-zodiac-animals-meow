using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlotUI : MonoBehaviour
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
        GetComponents();
        Setting();
        if (IsSellShop) //판매
        {
            shopSlotButton.onClick.AddListener(ButtonSell);
        }
        else
        {
            shopSlotButton.onClick.AddListener(ButtonBuy);
        }
    }

    public void GetComponents()
    {
        var getChild0 = transform.GetChild(0).gameObject;
        var getChild1 = transform.GetChild(1).gameObject;

        var getChild00 = getChild0.transform.GetChild(0).gameObject;
        var getChild10 = getChild1.transform.GetChild(0).gameObject;

        shopSlotButton = GetComponent<Button>();
        itemIcon = getChild0.transform.GetChild(0).GetComponent<Image>();
        itemName = getChild10.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        itemDescription = getChild10.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        itemPrice = getChild10.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        itemCount = getChild00.transform.GetChild(0).GetComponent<TMP_Text>();
    }


    public void Setting()
    {
        if (itemName == null)
        {
            GetComponents();
        }
        itemName.text = itemData.itemName;
        itemIcon.sprite = itemData.icon;
        itemDescription.text = itemData.itemDescription;
        if (IsSellShop) //판매
        {
            //판매 아이템은 Count 및 가격 조정
            // Debug.Log("IsSellShop_Setting");
            itemPrice.text = (itemData.price / 2).ToString("#,##0G");
            //shopSlotButton.onClick.AddListener(ButtonSell);

            for (int i = 0; i < ItemManager.I.itemDic.Count; i ++)
            {
                if (ItemManager.I.itemDic.ContainsKey(itemData.id))
                {
                    Int_Count = ItemManager.I.itemDic[itemData.id];
                }
            }

            itemCount.text = Int_Count.ToString();
            type = "sell";
        }
        else  //구매
        {
            //구매 아이템은 아이템의 데이터 그대로 받기!
            itemPrice.text = itemData.price.ToString("#,##0G");
            //shopSlotButton.onClick.AddListener(ButtonBuy);

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
        ShopManager.Instance.buyShop.SelectSlot(this);
        if (!ThisEquipItam())
        {
            ShopManager.Instance.buyShop.BuyInputField();
            Debug.Log($"Item : {itemData.id} Buy {itemData.type}");
        }
        else
        {
            if (!ShopManager.Instance.buyShop.cancel)
            {
                ItemManager.I.AddItem(itemData);
                ShopManager.Instance.sellShop.ShowInventorySlot();
                Debug.Log($"Item : {itemData.id} Buy {itemData.type}");
            }
           
        }
    }

    public void ButtonSell()
    {
        ShopManager.Instance.sellShop.SelectSlot(this);
        if (!ThisEquipItam())
        {
            ShopManager.Instance.sellShop.SellInputField();
            //갯수 입력 UI 뽕!
        }
        else
        {
            //inventory에서 찾아서 버리기
            ShopManager.Instance.sellShop.RemoveSlot2(itemData.id,1);
            Debug.Log($"Item : {itemData.id} Sell");
        }

        if (Int_Count <= 0)
        {
            Destroy(gameObject);
        }
    }
}
