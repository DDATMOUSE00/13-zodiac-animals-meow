using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot_Test : MonoBehaviour
{
    [Header("#bool")]
    public bool IsSellShop;
    public string type;
    //public string String_sellCount;
    public int Int_Count = 1;
    [Header("#ItemDataInfo")]
    public Item itemData; //������ �����͸� �޴´�!
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
        if (IsSellShop) //�Ǹ�
        {
            //�Ǹ� �������� Count �� ���� ����
            // Debug.Log("IsSellShop_Setting");
            itemPrice.text = (itemData.price / 2).ToString("#,##0G");
            shopSlotButton.onClick.AddListener(ButtonSell);

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
        else  //����
        {
            //���� �������� �������� ������ �״�� �ޱ�!
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
        ShopManager.Instance.buyShop.SelectSlot(this);
        if (!ThisEquipItam())
        {
            ShopManager.Instance.buyShop.BuyInputField();
            //Debug.Log($"Item : {itemData.id} Buy {itemData.type}");
        }
        else
        {
            ItemManager.I.AddItem(itemData);
            ShopManager.Instance.sellShop.ShowInventorySlot();
            //Debug.Log($"Item : {itemData.id} Buy {itemData.type}");
        }
    }

    public void ButtonSell()
    {
        ShopManager.Instance.sellShop.SelectSlot(this);
        if (!ThisEquipItam())
        {
            ShopManager.Instance.sellShop.SellInputField();
            //���� �Է� UI ��!
        }
        else
        {
            //inventory���� ã�Ƽ� ������
            //ItemManager.I.UpdateBundle(itemData.id, 1, type);
            //Shop.Instance.RemoveItem(itemData.id, 1);
            //Shop.Instance.RemoveSlot1(itemData.id, 1);
            ShopManager.Instance.sellShop.RemoveSlot2(1);
            Debug.Log($"Item : {itemData.id} Sell");
        }

        if (Int_Count <= 0)
        {
            Destroy(gameObject);
        }
    }
}
