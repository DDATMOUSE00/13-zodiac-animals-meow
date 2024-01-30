using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEngine.Rendering.DebugUI;


public class ShopSlot : MonoBehaviour
{
    [Header("#ItemDataInfo")]
    public Item itemData; //아이템 데이터를 받는다!
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI itemCost;
    public Button shopSlotButton;

    [Header("#InputFeild")]
    [SerializeField] private GameObject inputField_Obj;
    [SerializeField] private TMP_InputField inputField;
    //[SerializeField] private TextMeshProUGUI textMeshProUGUI;

    [SerializeField] private Button InputFeild_ExitButton;

    private void Awake()
    {
        shopSlotButton = GetComponent<Button>();
    }
    private void OnEnable()
    {
        //inputField = inputField_Obj.gameObject.GetComponentInChildren<TMP_InputField>();
    }

    private void Start()
    {
        
        Setting();
        BuySet(); //test
        inputField_Obj = GameObject.Find("input_Num_UI_Panel");
        inputField = inputField_Obj.gameObject.GetComponentInChildren<TMP_InputField>();
    }


    void Setting()
    {
        itemName.text = itemData.name;
        itemIcon.sprite = itemData.icon;
        itemDescription.text = itemData.description;
        //itemCost = itemData.Cost;
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
    public void BuySet()
    {
        shopSlotButton.onClick.AddListener(ButtonBuy);
    }

    public void SellSet()
    {
        shopSlotButton.onClick.AddListener(ButtonSell);
    }

    public void ButtonBuy()
    {
        if (!ThisEquipItam())
        {
            //갯수 입력 UI 뽕!
            //inputFeild.SetActive(true);
            //ShopButtonUI.Instance.InputFeild();
            InputFeild();
            Debug.Log($"Item : {itemData.id} Buy");
        }
        else
        {
            //inventory에 쏙!
            //InventoryManager
            InveoryManager.Instance.AddItem(itemData);
            Debug.Log($"Item : {itemData.id} Buy");
        }
    }

    public void ButtonSell()
    {
        if (!ThisEquipItam())
        {
            //갯수 입력 UI 뽕!
        }
        else
        {
            //inventory에서 찾아서 버리기
            //InveoryManager.Instance.RemoveItem(itemData);
            Debug.Log($"Item : {itemData.id} Sell");
        }
    }


    public void InputFeild()
    {
        if (inputField_Obj == null)
        {
            inputField_Obj = GameObject.Find("input_Num_UI_Panel");
        }
        //아이템의 타입의 따라 
        inputField_Obj.SetActive(true);
        inputField.onEndEdit.AddListener(delegate { EndEditEvent(inputField); });
    }

    public void EndEditEvent(TMP_InputField inputField)
    {
        string _inputNum = inputField.text;
        Debug.Log($" itemData.ID : {itemData}.{itemData.id} x {_inputNum} ");
        //Debug.Log($" itemData,Quantity :{_inputNum}");

        //InveoryManager.Instance.AddItem(itemData, _inputNum);
        inputField_Obj.SetActive(false);
        if (!inputField_Obj.activeInHierarchy)
        {
            inputField.onEndEdit.RemoveAllListeners();
        }
    }

    public void ExitButton()
    {
        inputField.onEndEdit.RemoveAllListeners();
        inputField_Obj.SetActive(false);
    }

    //public void OnEndEditEvent(string _inputNum)
    //{
    //    //Item ThisItem = this.itemData;

    //    Debug.Log($" itemData.ID : {itemData}.{itemData.ID}");
    //    Debug.Log($" itemData,Quantity :{_inputNum}");

    //    //InveoryManager.Instance.AddItem(itemData, _inputNum);
    //}

}
