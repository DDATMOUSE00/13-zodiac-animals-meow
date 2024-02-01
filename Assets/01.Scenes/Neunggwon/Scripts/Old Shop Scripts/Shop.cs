using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
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

    [Header("#Shop_InventoryList")]
    //public List<DraggableItem> draggedItems = new List<DraggableItem>();    //test
    public Dictionary<int, int> inventoryItems = new Dictionary<int, int>();
    public int inSlotCount;

    [Header("#SlotList")]
    public List<ShopSlot> shopSlots = new List<ShopSlot>();

    public Item selectItem;

    [Header("#InputField")]
    //[SerializeField] private GameObject inputField_UI;
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
        ShowInventorySlot();
        //GetInventorySlot();
    }

    private void Start()
    {
        if (!IsSellShop)
        {
            type = "buy";
            ShowInventorySlot();
            for (int i = 0; i < items.Count; i++)
            {
                AddNewUiObject();

                //shopSlots[i].ButtonBuy();
            }
        }
        else
        {
            type = "sell";
            // GetInventorySlot();
            ShowInventorySlot();
            //Debug.Log("Inventory의 Slots의 정보를 가지고 오자!!");
        }
        ExitButton();
    }
    

    private Item FindItem(int key)
    {

        for(int i =0; i < ItemManager.I.itemList.Count; i++)
        {
            if (ItemManager.I.itemList[i].id == key)
            {
                itemInInventory= ItemManager.I.itemList[i];
                break;
            }
        }
        return itemInInventory;
    }
    public void ShowInventorySlot()
    {
        List<int> keyList = new List<int>(ItemManager.I.itemDic.Keys);
        
        if (keyList.Count != 0 && shopSlots.Count < keyList.Count)
        {
            Debug.Log(keyList.Count);
            for (int i = 0; i < keyList.Count; i++)
            {
                Item item = FindItem(keyList[i]);

                //Debug.Log($"{item.name} - { keyList[i]}");
                int bundle = ItemManager.I.itemDic[keyList[i]];

                var newShopSlot = Instantiate(uiPrefab, scrollRect.content).GetComponent<ShopSlot>();
                newShopSlot.itemData = item;
                newShopSlot.itemCount.text = bundle.ToString();
                shopSlots.Add(newShopSlot);

            }
        }
     
    }
    public void GetInventorySlot() 
    {
        if (IsSellShop)
        {
            //인벤토리의 slots을 뺑이한다. DraggableItem을 찾는다.
            //for (int i = 0; i < ItemManager.I.slots.Length; i++)
            //{
            //    int inventoryInslotItemLenght = 0;
            //    if (ItemManager.I.slots[i].GetComponentInChildren<DraggableItem>())
            //    {
            //        inventoryInslotItemLenght++;
            //        if (shopSlots.Count < inventoryInslotItemLenght)
            //        {
            //            Debug.Log($"{inventoryInslotItemLenght}");
            //            var newShopSlot = Instantiate(uiPrefab, scrollRect.content).GetComponent<ShopSlot>();
            //            shopSlots.Add(newShopSlot);

            //            float y = 100f;

            //            for (int j = 0; j < inventoryInslotItemLenght; j++)
            //            {
            //                if (ItemManager.I.slots[j].GetComponentInChildren<DraggableItem>())
            //                shopSlots[j].itemData = ItemManager.I.slots[j].GetComponentInChildren<DraggableItem>().item;
            //                shopSlots[j].itemCount = ItemManager.I.slots[j].GetComponentInChildren<DraggableItem>().countText;
            //            }
            //            scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, (y + 20) * shopSlots.Count);
            //        }
            //        else
            //        {
            //            Debug.Log("Shop에 아이템이 부족합니다.");
            //        }
            //    }
            //}
            foreach (KeyValuePair<int, int> Test_inventoryItems in ItemManager.I.itemDic)
            {
                var newShopSlot = Instantiate(uiPrefab, scrollRect.content).GetComponent<ShopSlot>();
                shopSlots.Add(newShopSlot);
                float y = 100f;

                for (int j = 0; j < shopSlots.Count; j++)
                {
                    if (ItemManager.I.slots[j].GetComponentInChildren<DraggableItem>() != null)
                    {
                        shopSlots[j].itemData = ItemManager.I.slots[j].GetComponentInChildren<DraggableItem>().item;
                        shopSlots[j].itemCount = ItemManager.I.slots[j].GetComponentInChildren<DraggableItem>().countText;
                    }
                }
                scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, (y + 20) * shopSlots.Count);
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
        //else
        //{
        //    // 추가되는 아이템이 무기이면 새롭게 생성!, 

            

        //    if (shopSlots.Count < inSlotCount)
        //    {
        //        var newShopSlot = Instantiate(uiPrefab, scrollRect.content).GetComponent<ShopSlot>();
        //        shopSlots.Add(newShopSlot);

        //        float y = 100f;
        //        for (int i = 0; i < shopSlots.Count; i++)
        //        {
        //            shopSlots[i].itemData = ItemManager.I.slots[i].GetComponentInChildren<DraggableItem>().item;
        //        }
        //        scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, (y + 20) * shopSlots.Count);
        //        Debug.Log("ShopInventory_아이템 추가");
        //    }
        //    else
        //    {
        //        Debug.Log("Inventory에 아이템이 부족합니다.");
        //    }
        //}
    }

    public void RemoveItem()
    {
        for (int i = 0; i < shopSlots.Count; i++)
        {
            if (shopSlots[i].itemData == selectItem)
            {
                if (selectItem.type == ItemType.Weapon)
                {
                    //제거하자.
                }
                else
                {
                    //item.count가 입력한 값보다 작으면 Debug 호출
                    //item.count가 입력한 값보다 크면 --;
                    //같으면 Remove([i]);
                }
            }
        }
    }


    public void SelectItem(Item item)
    {
        selectItem = item;
        //Debug.Log($"SelectItem :{selectItem.id}");
    }

    public void InputField()
    {
        //아이템의 타입의 따라 
        inputField_Obj.SetActive(true);
        if (!IsSellShop)
        {
            inputField.onEndEdit.AddListener(delegate { EndEditEvent(inputField); });
        }
        else
        {

        }
    }

    public void EndEditEvent(TMP_InputField inputField) //확인 버튼을 눌렀을떄 인벤토리 업데이트
    {
        int int_inputNum = int.Parse(inputField.text);
        if (!cancel)
        {
            if (!IsSellShop)
            {
                for (int i = 0; i < int_inputNum; i++)
                {
                    ItemManager.I.AddItem(selectItem);
                }
            }
        }
        else
        {
            Debug.Log("입력 캔슬");
            inputField.onEndEdit.RemoveAllListeners();
        }

        Debug.Log($" itemData.ID : {selectItem}.{selectItem.id} x {int_inputNum} / {type} ");
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
