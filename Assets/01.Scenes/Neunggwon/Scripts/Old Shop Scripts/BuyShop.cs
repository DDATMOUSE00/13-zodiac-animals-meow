using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyShop : MonoBehaviour
{
    public ScrollRect scrollRect;
    public bool weaponShop;
    public string type;
    [SerializeField] private GameObject uiPrefab;

    [Header("#Shop_List")]
    public List<Item> items = new List<Item>();

    [Header("#Shop_InventoryList")]

    [Header("#SlotList")]
    public List<ShopSlot> shopSlots = new List<ShopSlot>();
    public List<int> inventorySlotKeyList = new List<int>();

    public Item selectItem;

    [Header("#BuyInputField")]
    //[SerializeField] private GameObject inputField_UI;
    [SerializeField] private GameObject inputField_Obj;
    [SerializeField] private TMP_InputField inputField;

    public bool cancel = false;


    private void Awake()
    {
        scrollRect = GetComponentInChildren<ScrollRect>();
    }
    private void Start()
    {
        for (int i = 0; i < items.Count; i++)
        {
            AddNewUiObject();
        }
        
    }
    

    public void AddNewUiObject()
    {
        if (!weaponShop)
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

    public void SelectItem(Item item)
    {
        selectItem = item;
        //Debug.Log($"SelectItem :{selectItem.id}");
    }

    public void InputField()
    {
        //아이템의 타입의 따라 
        inputField_Obj.SetActive(true);
        if (!weaponShop)
        {
            inputField.onEndEdit.AddListener(delegate { EndEditEvent(inputField); });
        }
    }

    public void EndEditEvent(TMP_InputField inputField) //확인 버튼을 눌렀을떄 인벤토리 업데이트
    {
        int int_inputNum = int.Parse(inputField.text);
        if (!cancel)
        {
            if (!weaponShop)
            {
                for (int i = 0; i < int_inputNum; i++)
                {
                    ItemManager.I.AddItem(selectItem);
                }
            }
        }
        else
        {
            inputField.onEndEdit.RemoveAllListeners();
        }

        Debug.Log($" itemData.ID : {selectItem}.{selectItem.id} x {int_inputNum} / {type} ");
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
