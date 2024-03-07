using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    public List<ShopSlotUI> shopSlots = new List<ShopSlotUI>();
    public List<int> inventorySlotKeyList = new List<int>();

    public Item selectItem;
    public ShopSlotUI selectShopSlot;

    [Header("#BuyInputField")]
    //[SerializeField] private GameObject inputField_UI;
    [SerializeField] private GameObject inputField_Obj;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject failedUI1;
    [SerializeField] private GameObject failedUI2;

    [HideInInspector] public bool cancel = false;


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
                var newShopSlot = Instantiate(uiPrefab, scrollRect.content).GetComponent<ShopSlotUI>();
                shopSlots.Add(newShopSlot);

                float y = 120f;

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

    public void SelectSlot(ShopSlotUI _slot)
    {
        selectShopSlot = _slot;
        selectItem = selectShopSlot.itemData;
    }

    public void BuyInputField()
    {
        inputField_Obj.SetActive(true);

        inputField.onSubmit.AddListener(delegate { Buy(inputField); });

        //inputField.onEndEdit.AddListener(delegate { Buy(inputField); });
        Debug.Log("delegate - Buy");

    }

    public void Buy(TMP_InputField inputField) //확인 버튼을 눌렀을떄 인벤토리 업데이트
    {
        int int_inputNum = int.Parse(inputField.text);
        if (int_inputNum <= 0)
        {
            StartCoroutine(ChekUI2());
            inputField.onSubmit.RemoveAllListeners();
        }
        else
        {
            var playerGold = GameManager.Instance.player.GetComponent<PlayerGold>();
            if (selectItem.price * int_inputNum <= playerGold.Gold)
            {
                if (!cancel)
                {

                    for (int i = 0; i < int_inputNum; i++)
                    {
                        ItemManager.I.AddItem(selectItem);
                    }
                    playerGold.RemoveGold(selectItem.price * int_inputNum);
                    ShopManager.Instance.ShopUpDate();
                    ShopManager.Instance.ShowInventorySlotManager();
                }
                else
                {
                    inputField.onSubmit.RemoveAllListeners();
                }
            }
            else
            {
                StartCoroutine(ChekUI1());
                Debug.Log($"플레이어 골드가 부족합니다.{playerGold.Gold}-{selectItem.price * int_inputNum}={selectItem.price * int_inputNum - playerGold.Gold}");
            }
        }
        
        inputField.onSubmit.RemoveAllListeners();
        ExitButton();
        ShopManager.Instance.ShowInventorySlotManager();
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

    IEnumerator ChekUI1()
    {
        failedUI1.SetActive(true);
        yield return YieldInstructionCache.WaitForSeconds(1.5f);
        failedUI1.SetActive(false);
        yield return null;
    }

    IEnumerator ChekUI2()
    {
        failedUI2.SetActive(true);
        yield return YieldInstructionCache.WaitForSeconds(1.5f);
        failedUI2.SetActive(false);
        yield return null;
    }
}
