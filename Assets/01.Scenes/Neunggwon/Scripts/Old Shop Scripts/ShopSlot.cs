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
    public string String_sellCount;
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


    void Setting()
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

            itemCount.text = Int_Count.ToString();
            
        }
        else  //����
        {
            //���� �������� �������� ������ �״�� �ޱ�!
            itemPrice.text = itemData.price.ToString("#,##0G");
            shopSlotButton.onClick.AddListener(ButtonBuy);

            itemCount.text = string.Empty;

        }
        bool textActive = Int_Count > 1;
        itemCount.gameObject.SetActive(textActive);
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
        if (!ThisEquipItam())
        {
            Shop.Instance.InputField();
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
        if (!ThisEquipItam())
        {
            Shop.Instance.InputField();
            //���� �Է� UI ��!
        }
        else
        {
            //inventory���� ã�Ƽ� ������
            ItemManager.I.UpdateBundle(itemData.id, 1, "sell");

            Debug.Log($"Item : {itemData.id} Sell");
        }
    }
}
