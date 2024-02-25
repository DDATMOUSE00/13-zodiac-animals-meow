using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;
//using static UnityEngine.Rendering.DebugUI;

public class Shop : MonoBehaviour
{
    public static Shop Instance;
    [Header("#Bool")]
    public bool IsSellShop;
    public string type;
    private ScrollRect scrollRect;
    private Item itemInInventory;
    [SerializeField] private GameObject uiPrefab;

    [Header("#Shop_List")]
    public List<Item> items = new List<Item>();

    [Header("#SlotList")]
    public List<ShopSlot> shopSlots = new List<ShopSlot>();
    public List<int> inventorySlotKeyList = new List<int>();

    public Item selectItem;
    public ShopSlot selectShopSlot;

    [Header("#BuyInputField")]
    [SerializeField] private GameObject inputField_Obj;
    [SerializeField] private TMP_InputField inputField;

    [SerializeField] private Button InputFeild_ExitButton;
    public bool cancel = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        scrollRect = GetComponent<ScrollRect>();
    }

    public void OnEnable()
    {
        if (IsSellShop)
        {
            ShowInventorySlot();
        }
    }

    private void Start()
    {
        if (!IsSellShop)
        {
            type = "buy";
            for (int i = 0; i < items.Count; i++)
            {
                AddNewUiObject();
            }
        }
        else
        {
            type = "sell";
            ShowInventorySlot();
        }
        ExitButton();
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
                //selectShopSlot.Setting();
                scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, (100 + 20) * shopSlots.Count);
            }

        }
    }

    public void AddNewUiObject()
    {
        if (!IsSellShop)
        {
            if (shopSlots.Count < items.Count)
            {
                var newShopSlot = Instantiate(uiPrefab, scrollRect.content).GetComponent<ShopSlot>();
                shopSlots.Add(newShopSlot);

                float y = 100f;

                for (int i = 0; i < shopSlots.Count; i++)
                {
                    shopSlots[i].itemData = items[i];
                }
                scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, (y + 20) * shopSlots.Count);
            }
            else
            {
                Debug.Log("Shop에 아이템이 부족합니다.");
            }
        }
    }

    //public void RemoveItem(int ID, int Value)
    //{
    //    //
    //    Debug.Log($"ID : {ID}, Value : {Value}");
    //    List<int> keyList = new List<int>(ItemManager.I.itemDic.Keys);
    //    for (int i = 0; i < keyList.Count; i++)
    //    {
    //        if (keyList[i] == ID)
    //        {
    //            ItemManager.I.itemDic[ID] -= Value;
    //            if (ItemManager.I.itemDic[ID] == 0)
    //            {
    //                //ItemManager.I.itemDic.Remove(ID);
    //                Debug.Log("ItemManager.I.itemDic_RemoveAt");
    //            }
    //        }
    //    }
    //    Debug.Log($"{ItemManager.I.itemDic[ID]}");
    //    //슬롯의 ItemData가 없으면 없어짐.
    //    for (int i = 0; i < shopSlots.Count; i++)
    //    {
    //        if (shopSlots[i].Int_Count <= 0)
    //        {
    //            shopSlots.RemoveAt(i);
    //            Debug.Log("shopSlots_RemoveAt");
    //        }
    //    }
    //}

    public void RemoveSlot(int id, int value)
    {
        if (selectShopSlot.Int_Count >= value)
        {
            for (int i =0;  i < ItemManager.I.slots.Length; i++)
            {
                var inventorySlotItem = ItemManager.I.slots[i].GetComponentInChildren<DraggableItem>();

                if (inventorySlotItem.item == selectItem)
                {
                    inventorySlotItem.bundle -= value;
                    selectShopSlot.Int_Count = inventorySlotItem.bundle;
                    selectShopSlot.Setting();
                    inventorySlotItem.RefreshCount();
                    if (inventorySlotItem.bundle == 0)
                    {
                        Debug.Log("제거");
                        selectShopSlot.Clear();
                        //ItemManager.I.UseSelectedItem();
                    }
                    Debug.Log(inventorySlotItem.bundle);
                    Debug.Log($"{inventorySlotItem.item.itemName} - {inventorySlotItem.bundle}");
                    break;
                }
            }
        }
        else
        {
            Debug.Log("다시 입력해 주세요.");
        }

    }


    //Test
    public void RemoveSlot1(int id, int value)
    {
        Debug.Log($"selectShopSlot.itemData.id {selectShopSlot.itemData.id}");
        if (selectShopSlot.Int_Count >= value)
        {
            for (int i = 0; i < ItemManager.I.itemDic.Count; i++)
            {
                var inventoryItem = ItemManager.I.itemDic[i];
                if (ItemManager.I.itemDic.ContainsKey(selectShopSlot.itemData.id))
                {
                    Debug.Log($"{selectShopSlot.itemData.id},{inventoryItem}");
                    inventoryItem -= value;
                    selectShopSlot.Int_Count = inventoryItem;
                    selectShopSlot.Setting();

                }
                Debug.Log(inventoryItem);
                //Debug.Log($"{inventoryItem} - {inventoryItem}");
            }
            InventroySetting();
        }
        else
        {
            Debug.Log("갯수가 부족합니다.");
        }
    }

    public void InventroySetting()
    {
        for ( int i = 0; i < ItemManager.I.slots.Length; i++)
        {
            var inventorySlotItem = ItemManager.I.slots[i].GetComponentInChildren<DraggableItem>();
            if (inventorySlotItem.item.id == selectShopSlot.itemData.id)
            {
                inventorySlotItem.RefreshCount();
            }
        }
    }


    //Test




    public void SelectItem(Item item)
    {
        selectItem = item;
        //Debug.Log($"SelectItem :{selectItem.id}");
    }

    public void SelectSlot(ShopSlot _slot)
    {
        selectShopSlot = _slot;
    }

    public void BuyInputField()
    {
        inputField_Obj.SetActive(true);

        inputField.onEndEdit.AddListener(delegate { EndEditBuy(inputField); });
        Debug.Log("delegate - Buy");

    }

    public void SellInputField()
    {
        inputField_Obj.SetActive(true);

        inputField.onEndEdit.AddListener(delegate { EndEditSell(inputField); });
        Debug.Log("delegate - Sell");
    }

    public void EndEditBuy(TMP_InputField inputField) //확인 버튼을 눌렀을떄 인벤토리 업데이트
    {
        int int_inputNum = int.Parse(inputField.text);
        if (!cancel)
        {

            for (int i = 0; i < int_inputNum; i++)
            {
                ItemManager.I.AddItem(selectItem);
            }
            Debug.Log($" itemData.ID : {selectItem}.{selectItem.id} x {int_inputNum} / {type} ");
        }
        else
        {
            inputField.onEndEdit.RemoveAllListeners();
        }
        inputField.onEndEdit.RemoveAllListeners();
        ExitButton();
    }

    public void EndEditSell(TMP_InputField inputField) //확인 버튼을 눌렀을떄 인벤토리 업데이트
    {
        int int_inputNum = int.Parse(inputField.text);
        if (!cancel)
        {
            RemoveSlot1(selectShopSlot.itemData.id, int_inputNum);
            Debug.Log($" RemoveItem({selectShopSlot.itemData.id}, {int_inputNum})");
            //Debug.Log($" itemData.ID : {selectItem}.{selectItem.id} x {int_inputNum} / {type} ");
        }
        else
        {
            inputField.onEndEdit.RemoveAllListeners();
        }
        ExitButton();
    }

    public void ExitButton()
    {

        inputField.onEndEdit.RemoveAllListeners();
        cancel = true;
        //Debug.Log("InputField_Exit");
        inputField_Obj.SetActive(false);
        if (!inputField_Obj.activeInHierarchy)
        {
            cancel = false;
        }
    }
}
