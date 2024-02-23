using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEngine.Rendering.DebugUI;

public class SellShop : MonoBehaviour
{
    [Header("#Bool")]
    public bool IsSellShop;
    public string type;
    private ScrollRect scrollRect;
    private Item itemInInventory;
    [SerializeField] private GameObject uiPrefab;

    [Header("#Shop_List")]
    public List<Item> items = new List<Item>();

    [Header("#SlotList")]
    public List<ShopSlot_Test> shopSlots = new List<ShopSlot_Test>();
    public List<int> inventorySlotKeyList = new List<int>();

    public Item selectItem;
    public ShopSlot_Test selectShopSlot;


    [Header("#BuyInputField")]
    [SerializeField] private GameObject inputField_Obj;
    [SerializeField] private TMP_InputField inputField;

    [SerializeField] private Button InputFeild_ExitButton;
    private bool cancel = false;

    private void Awake()
    {
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
            type = "sell";
            ShowInventorySlot();
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
    //public void ShowInventorySlot()
    //{
    //    List<int> keyList = new List<int>(ItemManager.I.itemDic.Keys);

    //    if (keyList.Count != 0)
    //    {
    //        for (int j = 0; j < keyList.Count; j++)
    //        {
    //            //Debug.Log(keyList.Count);
    //            Item item = FindItem(keyList[j]);
    //            int bundle = ItemManager.I.itemDic[keyList[j]];

    //            if (!inventorySlotKeyList.Contains(keyList[j]))
    //            {
    //                var newShopSlot = Instantiate(uiPrefab, scrollRect.content).GetComponent<ShopSlot_Test>();
    //                newShopSlot.itemData = item;
    //                newShopSlot.Int_Count = bundle;
    //                shopSlots.Add(newShopSlot);
    //                inventorySlotKeyList.Add(keyList[j]);
    //                Debug.Log($"{item.itemName} - {bundle}");
    //            }
    //            else
    //            {
    //                foreach (var shopSlot in shopSlots)
    //                {
    //                    if (shopSlot.itemData == item)
    //                    {
    //                        shopSlot.itemCount.text = bundle.ToString();
    //                        break;
    //                    }

    //                }
    //                Debug.Log($"{item.itemName} - {bundle}");
    //            }
    //            //selectShopSlot.Setting();
    //            scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, (100 + 20) * shopSlots.Count);
    //        }

    //    }
    //}
    //test

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

                if (!inventorySlotKeyList.Contains(keyList[i]) && bundle != 0)
                {
                    var newShopSlot = Instantiate(uiPrefab, scrollRect.content).GetComponent<ShopSlot_Test>();
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
        //SlotUpDate();
    }

    public void SlotUpDate()
    {
        for (int i = 0; i < shopSlots.Count; i ++)
        {
            shopSlots[i].Setting();
        }
    }

    //test


    //public void RemoveSlot(int id, int value)
    //{
    //    if (selectShopSlot.Int_Count >= value)
    //    {
    //        for (int i = 0; i < ItemManager.I.slots.Length; i++)
    //        {
    //            var inventorySlotItem = ItemManager.I.slots[i].GetComponentInChildren<DraggableItem>();

    //            if (inventorySlotItem.item == selectShopSlot.itemData)
    //            {
    //                inventorySlotItem.bundle -= value;
    //                selectShopSlot.Int_Count = inventorySlotItem.bundle;
    //                selectShopSlot.Setting();
    //                inventorySlotItem.RefreshCount();
    //                if (inventorySlotItem.bundle == 0)
    //                {
    //                    Debug.Log("제거");
    //                    //shopSlots.Remove(i);
    //                    selectShopSlot.Clear();
    //                    //ItemManager.I.UseSelectedItem();
    //                }
    //                Debug.Log(inventorySlotItem.bundle);
    //                Debug.Log($"{inventorySlotItem.item.itemName} - {inventorySlotItem.bundle}");
    //                break;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("다시 입력해 주세요.");
    //    }

    //}


    //Test
    //public void RemoveSlot1(int id, int value)
    //{
    //    Debug.Log($"selectShopSlot.itemData.id {selectShopSlot.itemData.id}");
    //    if (selectShopSlot.Int_Count >= value)
    //    {
    //        for (int i = 0; i < ItemManager.I.itemDic.Count; i++)
    //        {
    //            var inventoryItem = ItemManager.I.itemDic[i];
    //            if (ItemManager.I.itemDic.ContainsKey(selectShopSlot.itemData.id))
    //            {
    //                Debug.Log($"selectShopSlot.id,count {selectShopSlot.itemData.id},{inventoryItem}");
    //                inventoryItem -= value;
    //                selectShopSlot.Int_Count = inventoryItem;
    //                selectShopSlot.Setting();

    //            }
    //            Debug.Log(inventoryItem);
    //            //Debug.Log($"{inventoryItem} - {inventoryItem}");
    //        }

    //        for (int i = 0; i < ItemManager.I.slots.Length;  i++)
    //        {
    //            var findItem = ItemManager.I.slots[i].GetComponentInChildren<DraggableItem>();
    //            if (findItem.item.id == selectShopSlot.itemData.id )
    //            {
    //                findItem.RefreshCount();
    //            }
    //        }
    //        //InventroySetting();
    //    }
    //    else
    //    {
    //        Debug.Log("갯수가 부족합니다.");
    //    }
    //}


    //test-test

    public void RemoveSlot2(int value)
    {
        //Debug.Log($"selectShopSlot.itemData.id {selectShopSlot.itemData.id}");
        if (selectShopSlot.Int_Count > value)
        {
            for (int i = 0; i < ItemManager.I.itemDic.Count; i++)
            {
                if (ItemManager.I.itemDic.ContainsKey(selectShopSlot.itemData.id))
                {
                    ItemManager.I.itemDic[selectShopSlot.itemData.id] -= value;
                    selectShopSlot.Int_Count = ItemManager.I.itemDic[selectShopSlot.itemData.id];
                    //Debug.Log(ItemManager.I.itemDic[selectShopSlot.itemData.id]);
                    selectShopSlot.Setting();
                    ItemManager.I.RefreshInventorySlot();
                    break;
                }
                //Debug.Log(ItemManager.I.itemDic);
            }
        }
        else if (selectShopSlot.Int_Count == value)
        {
            for (int i = 0; i < ItemManager.I.itemDic.Count; i++)
            {
                if (ItemManager.I.itemDic.ContainsKey(selectShopSlot.itemData.id))
                {
                    ItemManager.I.itemDic[selectShopSlot.itemData.id] -= value;
                    selectShopSlot.Int_Count = ItemManager.I.itemDic[selectShopSlot.itemData.id];
                    selectShopSlot.Setting();
                    ItemManager.I.RefreshInventorySlot();
                    DestorySlot();
                    break;
                }
            }
        }
        else
        {
            Debug.Log("갯수가 부족합니다.");
        }
    }

    public void DestorySlot()
    {
        for ( int i = 0; i < shopSlots.Count; i++)
        {
            if (shopSlots.Contains(selectShopSlot))
            {
                print("DestorySlot");
                shopSlots.RemoveAt(i);
                selectShopSlot.Clear();
                inventorySlotKeyList.RemoveAt(i);
            }
        }
    }

    //test-test

    public void InventroySetting()
    {
        for (int i = 0; i < ItemManager.I.slots.Length; i++)
        {
            var inventorySlotItem = ItemManager.I.slots[i].GetComponentInChildren<DraggableItem>();
            if (inventorySlotItem.item == selectShopSlot.itemData)
            {
                inventorySlotItem.RefreshCount();
            }
        }
    }


    //Test

    public void SelectSlot(ShopSlot_Test _slot)
    {
        selectShopSlot = _slot;
        selectItem = selectShopSlot.itemData;
    }

    public void SellInputField()
    {
        inputField_Obj.SetActive(true);

        inputField.onEndEdit.AddListener(delegate { EndEditSell(inputField); });
        Debug.Log($"delegate - EndEditSell/{selectShopSlot.itemData}");
    }


    public void EndEditSell(TMP_InputField inputField) //확인 버튼을 눌렀을떄 인벤토리 업데이트
    {
        int int_inputNum = int.Parse(inputField.text);
        if (!cancel)
        {
            RemoveSlot2(int_inputNum);
            Debug.Log($" RemoveItem({selectShopSlot.itemData.id}, {int_inputNum})");
        }
        else
        {
            inputField.onEndEdit.RemoveAllListeners();
        }
        inputField.onEndEdit.RemoveAllListeners();
        ExitButton();
    }

    public void ExitButton()
    {

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
