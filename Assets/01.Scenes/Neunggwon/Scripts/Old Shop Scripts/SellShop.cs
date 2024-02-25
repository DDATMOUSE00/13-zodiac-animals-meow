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
    public List<ShopSlot> shopSlots = new List<ShopSlot>();
    public List<int> inventorySlotKeyList = new List<int>();

    public Item selectItem;
    public ShopSlot selectShopSlot;


    [Header("#BuyInputField")]
    [SerializeField] private GameObject inputField_Obj;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject failedUI;

    //[SerializeField] private Button InputFeild_ExitButton;
    [HideInInspector] public bool cancel = false;

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
                    var newShopSlot = Instantiate(uiPrefab, scrollRect.content).GetComponent<ShopSlot>();
                    newShopSlot.itemData = item;
                    newShopSlot.Int_Count = bundle;
                    shopSlots.Add(newShopSlot);
                    inventorySlotKeyList.Add(keyList[i]);
                    //Debug.Log($"{item.itemName} - {bundle}");
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
                }
                scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, (100 + 20) * shopSlots.Count);
            }

        }
        SlotUpDate();
    }

    public void SlotUpDate()
    {
        for (int i = 0; i < shopSlots.Count; i++)
        {
            shopSlots[i].Setting();
        }
    }

    public void RemoveSlot2(int key, int value)
    {
        key = selectItem.id;
        if (selectShopSlot.Int_Count > value)
        {
            for (int i = 0; i < ItemManager.I.itemDic.Count; i++)
            {
                if (ItemManager.I.itemDic.ContainsKey(selectShopSlot.itemData.id))
                {
                    ItemManager.I.itemDic[key] -= value;
                    selectShopSlot.Int_Count = ItemManager.I.itemDic[key];
                    selectShopSlot.Setting();
                    ItemManager.I.RefreshInventorySlot();
                    break;
                }
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
        for (int i = 0; i < shopSlots.Count; i++)
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


    public void SelectShopSlotUpdate()
    {
        for (int i = 0; i < shopSlots.Count; i++)
        {
            if (shopSlots.Contains(selectShopSlot))
            {
                selectShopSlot.Setting();
                if (selectShopSlot.Int_Count <= 0)
                {
                    shopSlots.RemoveAt(i);
                    selectShopSlot.Clear();
                    inventorySlotKeyList.RemoveAt(i);
                }
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

    public void SelectSlot(ShopSlot _slot)
    {
        selectShopSlot = _slot;
        selectItem = selectShopSlot.itemData;
    }

    public void SellInputField()
    {
        inputField_Obj.SetActive(true);

        inputField.onSubmit.AddListener(delegate { Sell(inputField); });

        //inputField.onEndEdit.AddListener(delegate { Sell(inputField); });
        Debug.Log($"delegate - Sell/{selectShopSlot.itemData}");
    }


    public void Sell(TMP_InputField inputField) //확인 버튼을 눌렀을떄 인벤토리 업데이트
    {
        int int_inputNum = int.Parse(inputField.text);
        if (ItemManager.I.ChekInventoryItem(selectItem.id, int_inputNum))
        {
            if (!cancel)
            {
                ItemManager.I.RemoveItem(selectItem.id, int_inputNum);
                SelectShopSlotUpdate();
                ShopManager.Instance.ShopUpDate();
                var playerGold = GameManager.Instance.player.GetComponent<PlayerGold>();
                playerGold.RemoveGold(selectShopSlot.Int_Count * int_inputNum);
            }
            else
            {
                inputField.onSubmit.RemoveAllListeners();
            }
        }
        else
        {
            StartCoroutine(BuffChekUI());
            Debug.Log($"판매할 아이템이 부족합니다.{selectItem.id}/{selectShopSlot.Int_Count}-{int_inputNum}={selectShopSlot.Int_Count - int_inputNum}");
        }
        inputField.onSubmit.RemoveAllListeners();
        ExitButton();
    }

    public void ExitButton()
    {

        inputField.onSubmit.RemoveAllListeners();
        cancel = true;
        Debug.Log("InputField_Exit");
        inputField_Obj.SetActive(false);
        if (!inputField_Obj.activeInHierarchy)
        {
            cancel = false;
        }
    }

    IEnumerator BuffChekUI()
    {
        failedUI.SetActive(true);
        yield return YieldInstructionCache.WaitForSeconds(1.5f);
        failedUI.SetActive(false);
        yield return null;
    }
}
