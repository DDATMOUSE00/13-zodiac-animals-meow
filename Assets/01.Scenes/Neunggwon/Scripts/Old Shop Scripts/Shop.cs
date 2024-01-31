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
    
    private ScrollRect scrollRect;
    [SerializeField] private GameObject uiPrefab;

    public List<Item> items = new List<Item>();
    public List<ShopSlot> shopSlots = new List<ShopSlot>();
    //public List<Item> inventoryItems = ItemManager.I.itemList;
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
    
      
    private void OnEnable()
    {
        if (IsSellShop)
        {
            GetInventorySlot();
        }
        //if (IsSellShop)
        //{
        //    //ItemManager itemManager = new ItemManager();
        //    //items = itemManager.itemList;
        //    //items = ItemManager.I.itemList;
        //    //for (int i = 0; i < items.Count; i++)
        //    //{
        //    //    AddNewUiObject();
        //    //    //shopSlots[i].ButtonBuy();
        //    //}
        //    Debug.Log("Inventory의 Slots의 정보를 가지고 오자!!");
        //}
    }

    private void GetInventory()
    {
        //items가 inventory의 List를 바라보도록 하자!!
        ItemManager itemManager = new ItemManager();
        items = itemManager.itemList;
    }


    private void Start()
    {
        if (!IsSellShop)
        {
            for (int i = 0; i < items.Count; i++)
            {
                AddNewUiObject();
                //shopSlots[i].ButtonBuy();
            }
        }
        else
        {
            GetInventorySlot();
            //Debug.Log("Inventory의 Slots의 정보를 가지고 오자!!");
        }
        ExitButton();
    }

    public void GetInventorySlot()
    {
        
        items = ItemManager.I.itemList;
        for (int i = 0; i < items.Count; i++)
        {
            AddNewUiObject();
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
                Debug.Log("Shop_아이템 추가");
            }
            else
            {
                Debug.Log("Shop에 아이템이 부족합니다.");
            }
        }
        else
        {
            // 추가되는 아이템이 무기이면 새롭게 생성!, 
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
                Debug.Log("ShopInventory_아이템 추가");
            }
            else
            {
                Debug.Log("Inventory에 아이템이 부족합니다.");
            }
        }
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

    public int SelecItemCount()
    {

        return 0;
    }
    
    public void SelectItem(Item item)
    {
        selectItem = item;
        Debug.Log($"SelectItem :{selectItem.id}");
    }

    public void InputField()
    {
        //if (inputField_Obj == null)
        //{
        //    inputField_Obj = GameObject.Find("input_Num_UI_Panel");
        //}
        //아이템의 타입의 따라 
        inputField_Obj.SetActive(true);
        inputField.onEndEdit.AddListener(delegate { EndEditEvent(inputField); });
    }

    public void EndEditEvent(TMP_InputField inputField)
    {
        string _inputNum = inputField.text;
        int int_inputNum = int.Parse(_inputNum);
        if (!cancel)
        {
            for (int i = 0; i < int_inputNum; i++)
            {
                ItemManager.I.AddItem(selectItem);
            }
        }
        else
        {
            Debug.Log("입력 캔슬");
            inputField.onEndEdit.RemoveAllListeners();
        }

        Debug.Log($" itemData.ID : {selectItem}.{selectItem.id} x {_inputNum} ");
        ExitButton();
    }

    public void ExitButton()
    {
        GetInventorySlot();
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
