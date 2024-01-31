using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEngine.Rendering.DebugUI;


public class ShopSlot : MonoBehaviour
{
    [Header("#bool")]
    public bool IsSellShop;
    public string String_sellCount;
    public int Int_sellCount;
    [Header("#ItemDataInfo")]
    public Item itemData; //아이템 데이터를 받는다!
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI itemPrice;
    [SerializeField] private TextMeshProUGUI itemCount = null;
    public Button shopSlotButton;

    [Header("#InputField")]
    //[SerializeField] private GameObject inputField_UI;
    //[SerializeField] private GameObject inputField_Obj;
    //[SerializeField] private TMP_InputField inputField;
    //[SerializeField] private TextMeshProUGUI textMeshProUGUI;

    [SerializeField] private Button InputFeild_ExitButton;

    private void Awake()
    {
        //shopSlotButton = GetComponent<Button>();

    }
    private void OnEnable()
    {
        //inputField = inputField_Obj.gameObject.GetComponentInChildren<TMP_InputField>();
    }

    private void Start()
    {

        Setting();
        //inputField_UI = GameObject.Find("Input_Field_UI");
        ////inputField_Obj = GameObject.Find("input_Num_UI_Panel");
        //inputField_Obj = inputField_UI.gameObject.GetComponentInChildren<GameObject>();
        //inputField = inputField_UI.gameObject.GetComponentInChildren<TMP_InputField>();
    }


    void Setting()
    {
        itemName.text = itemData.name;
        itemIcon.sprite = itemData.icon;
        itemDescription.text = itemData.description;
        if (IsSellShop) //판매
        {
            //판매 아이템은 Count 및 가격 조정
            itemPrice.text = (itemData.price / 2).ToString("#,##0G");
            shopSlotButton.onClick.AddListener(ButtonSell);

            itemCount.text = itemCount.ToString();
            for (int i = 0; i < ItemManager.I.items.Count; i++)
            {

                if (itemData == ItemManager.I.items[i].item)
                {
                    if (ItemManager.I.items[i].item.type != ItemType.Weapon)
                    {
                        itemCount.text = ItemManager.I.items[i].countText.ToString();
                        //sellCount = int.Parse(itemCount.ToString());
                        String_sellCount = ItemManager.I.items[i].countText.ToString();
                        Int_sellCount = int.Parse(String_sellCount);
                        Debug.Log($"itemCount.text : {ItemManager.I.items[i].countText.ToString()}");
                        Debug.Log($"itemCount.text : {itemCount.text}");
                        Debug.Log($"itemCount.text : {String_sellCount}");
                        Debug.Log($"itemCount.text : {Int_sellCount}");

                    }
                    else
                    {
                        itemCount.text = string.Empty;
                    }
                }
            }
            //SellSet();
        }
        else  //구매
        {
            //구매 아이템은 아이템의 데이터 그대로 받기!
            itemPrice.text = itemData.price.ToString("#,##0G");
            shopSlotButton.onClick.AddListener(ButtonBuy);

            itemCount.text = string.Empty;

            //BuySet();
        }

    }
    private bool ThisEquipItam() //test
    {
        //Debug.Log($"ItemType : {itemData.type}");
        //아이템이 무기인지 확인...
        if (itemData.type == ItemType.Weapon)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ButtonBuy()
    {
        Shop.Instance.SelectItem(itemData);
        if (!ThisEquipItam())
        {
            //inputFeild.SetActive(true);
            //ShopButtonUI.Instance.InputField();
            //InputField();
            Shop.Instance.InputField();
            //Debug.Log($"Item : {itemData.id} Buy {itemData.type}");
        }
        else
        {
            //inventory에 쏙!
            //InventoryManager
            //Shop.Instance.SelectItem(itemData);
            ItemManager.I.AddItem(itemData);
            Shop.Instance.GetInventorySlot();
            //Debug.Log($"Item : {itemData.id} Buy {itemData.type}");
        }
    }

    public void ButtonSell()
    {
        Shop.Instance.SelectItem(itemData);
        if (!ThisEquipItam())
        {
            //갯수 입력 UI 뽕!
        }
        else
        {
            //inventory에서 찾아서 버리기
            Debug.Log($"Item : {itemData.id} Sell");
        }
    }


    //public void InputField()
    //{
    //    if (inputField_Obj == null)
    //    {
    //        //inputField_Obj = GameObject.Find("input_Num_UI_Panel");
    //    }
    //    //아이템의 타입의 따라 
    //    //inputField_Obj.SetActive(true);
    //    inputField.onEndEdit.AddListener(delegate { EndEditEvent(inputField); });
    //}

    //public void EndEditEvent(TMP_InputField inputField)
    //{
    //    string _inputNum = inputField.text;
    //    int int_inputNum = int.Parse(_inputNum);
    //    Debug.Log($" itemData.ID : {itemData}.{itemData.id} x {_inputNum} ");

    //    //InveoryManager.Instance.AddItem(itemData, _inputNum);
    //    for (int i = 0; i < int_inputNum; i++)
    //    {
    //        ItemManager.I.AddItem(itemData);
    //    }

    //    //inputField_Obj.SetActive(false);
    //    //if (!inputField_Obj.activeInHierarchy)
    //    //{
    //    //    inputField.onEndEdit.RemoveAllListeners();
    //    //}
    //}

    //public void ExitButton()
    //{
    //    inputField.onEndEdit.RemoveAllListeners();
    //    //inputField_Obj.SetActive(false);
    //}

    //public void OnEndEditEvent(string _inputNum)
    //{
    //    //Item ThisItem = this.itemData;

    //    Debug.Log($" itemData.ID : {itemData}.{itemData.ID}");
    //    Debug.Log($" itemData,Quantity :{_inputNum}");

    //    //InveoryManager.Instance.AddItem(itemData, _inputNum);
    //}

}
