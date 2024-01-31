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
        //    Debug.Log("Inventory�� Slots�� ������ ������ ����!!");
        //}
    }

    private void GetInventory()
    {
        //items�� inventory�� List�� �ٶ󺸵��� ����!!
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
            //Debug.Log("Inventory�� Slots�� ������ ������ ����!!");
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
                Debug.Log("Shop_������ �߰�");
            }
            else
            {
                Debug.Log("Shop�� �������� �����մϴ�.");
            }
        }
        else
        {
            // �߰��Ǵ� �������� �����̸� ���Ӱ� ����!, 
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
                Debug.Log("ShopInventory_������ �߰�");
            }
            else
            {
                Debug.Log("Inventory�� �������� �����մϴ�.");
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
                    //��������.
                }
                else
                {
                    //item.count�� �Է��� ������ ������ Debug ȣ��
                    //item.count�� �Է��� ������ ũ�� --;
                    //������ Remove([i]);
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
        //�������� Ÿ���� ���� 
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
            Debug.Log("�Է� ĵ��");
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
