using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class SellShop : MonoBehaviour
{
    public static SellShop Instance;

    [SerializeField] private GameObject uiPrefab;

    public ScrollRect scrollRect;

    private Item itemInInventory;
    public Item selectItem;

    public bool IsSellShop;
    public bool cancel = false;
    public string type = "sell";
    [Header("#SlotList")]
    public List<ShopSlot> shopSlots = new List<ShopSlot>();
    public List<int> inventorySlotKeyList = new List<int>();

    [Header("#BuyInputField")]
    //[SerializeField] private GameObject inputField_UI;
    [SerializeField] private GameObject inputField_Obj;
    [SerializeField] private TMP_InputField inputField;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        scrollRect = GetComponentInChildren<ScrollRect>();
    }

    public void OnEnable()
    {
        if (IsSellShop)
        {
            ShowInventorySlot();
        }
    }

    private Item FindItem(int key)
    {

        for (int i = 0; i < ItemManager.I.itemList.Count; i++)
        {
            if (ItemManager.I.itemList[i].id == key)
            {
                itemInInventory = ItemManager.I.itemList[i];
                break;
            }
        }
        return itemInInventory;
    }
    public void ShowInventorySlot()
    {
        List<int> keyList = new List<int>(ItemManager.I.itemDic.Keys);

        if (keyList.Count != 0)
        {
            for (int i = 0; i < keyList.Count; i++)
            {
                //Debug.Log(keyList.Count);
                Item item = FindItem(keyList[i]);
                int bundle = ItemManager.I.itemDic[keyList[i]];

                if (!inventorySlotKeyList.Contains(keyList[i]))
                {
                    var newShopSlot = Instantiate(uiPrefab, scrollRect.content).GetComponent<ShopSlot>();
                    newShopSlot.itemData = item;
                    newShopSlot.Int_Count = bundle;
                    shopSlots.Add(newShopSlot);
                    inventorySlotKeyList.Add(keyList[i]);
                    Debug.Log($"{item.itemName} - {bundle}");
                }
                else
                {
                    foreach (var shopSlot in shopSlots)
                    {
                        if (shopSlot.itemData == item)
                        {
                            shopSlot.itemCount.text = bundle.ToString();
                            break;
                        }

                    }
                    Debug.Log($"{item.itemName} - {bundle}");
                }
            }

        }
    }

    public void EndEditEvent(TMP_InputField inputField) //확인 버튼을 눌렀을떄 인벤토리 업데이트
    {
        int int_inputNum = int.Parse(inputField.text);
        if (!cancel)
        {
            ItemManager.I.UpdateBundle(selectItem.id, int_inputNum, type);
        }
        else
        {
            inputField.onEndEdit.RemoveAllListeners();
        }

        ExitButton();
    }

    public void ExitButton()
    {
        ShowInventorySlot();
        inputField.onEndEdit.RemoveAllListeners();
        cancel = true;
        Debug.Log("InputField_Exit");
        inputField_Obj.SetActive(false);
        if (!inputField_Obj.activeInHierarchy)
        {
            cancel = false;
        }
    }
}
