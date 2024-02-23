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
    //public List<Item> selectItems = new List<Item>();
    public List<Item> selectItems = new List<Item>();

    public int maxShopSloatCount = 3;

    [Header("#Shop_InventoryList")]

    [Header("#SlotList")]
    public List<ShopSlot_Test> shopSlots = new List<ShopSlot_Test>();
    public List<int> inventorySlotKeyList = new List<int>();

    public Item selectItem;
    public ShopSlot_Test selectShopSlot;

    [Header("#BuyInputField")]
    //[SerializeField] private GameObject inputField_UI;
    [SerializeField] private GameObject inputField_Obj;
    [SerializeField] private TMP_InputField inputField;

    private bool cancel = false;


    private void Awake()
    {
        scrollRect = GetComponentInChildren<ScrollRect>();
    }
    private void Start()
    {
        SetSelecItems();
    }

    public void SelecItems()
    {
        // 셔플 알고리즘
        for (int i = items.Count - 1; i > 0; i--)
        {
            int randIndex = Random.Range(0, i + 1);
            Item temp = items[i];
            items[i] = items[randIndex];
            items[randIndex] = temp;
        }

        // 셔플된 리스트에서 첫 3개의 원소를 가져옴
        for (int i = 0; i < maxShopSloatCount; i++)
        {
            selectItems.Add(items[i]);
        }
    }

    //Reroll
    #region Reroll
    public void SetSelecItems()
    {
        ClearSelecItems();
        SelecItems();
        for (int i = 0; i < maxShopSloatCount; i++)
        {
            AddNewUiObject();
        }
    }

    public void ClearSelecItems()
    {
        selectItems.Clear();
        ClearShopSlots();
    }

    public void ClearShopSlots()
    {
        for (int i = 0; i < shopSlots.Count; i++)
        {
            Destroy(shopSlots[i].gameObject);
        }
        shopSlots.Clear();
    }
    #endregion

    public void AddNewUiObject()
    {
        if (!weaponShop)
        {
            if (shopSlots.Count < maxShopSloatCount)
            {
                var newShopSlot = Instantiate(uiPrefab, scrollRect.content).GetComponent<ShopSlot_Test>();
                shopSlots.Add(newShopSlot);

                float y = 100f;

                for (int i = 0; i < shopSlots.Count; i++)
                {
                    shopSlots[i].itemData = selectItems[i];
                }
                scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, (y + 20) * shopSlots.Count);
            }
            else
            {
                Debug.Log("Shop에 아이템이 부족합니다.");
            }
        }
    }

    public void SelectSlot(ShopSlot_Test _slot)
    {
        selectShopSlot = _slot;
        selectItem = selectShopSlot.itemData;
    }

    public void BuyInputField()
    {
        inputField_Obj.SetActive(true);

        inputField.onEndEdit.AddListener(delegate { EndEditBuy(inputField); });
        Debug.Log("delegate - EndEditBuy");

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
        ShopManager.Instance.ShowInventorySlotManager();
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
