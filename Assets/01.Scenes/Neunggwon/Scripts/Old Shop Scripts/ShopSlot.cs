using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public ShopButtonUI _ShopButton;

    [Header("#InputFeild")]
    [SerializeField] private GameObject inputFeild_Obj;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    [SerializeField] private Button InputFeild_ExitButton;

    private void OnEnable()
    {
        //Setting();
        if (inputFeild_Obj == null)
        {
            inputFeild_Obj = GameObject.Find("input_Num_UI_Panel");
        }
        inputField = inputFeild_Obj.gameObject.GetComponentInChildren<TMP_InputField>();
    }

    private void Start()
    {
        Setting();
        inputFeild_Obj.SetActive(false);
    }


    void Setting()
    {
        itemName.text = itemData.Name;
        itemIcon.sprite = itemData.Icon;
        itemDescription.text = itemData.Description;
        //itemCost = itemData.Cost;

        if (0 == 0) // itemType에 따라 세팅해야 할 거 추가 (임시임)
        {

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
        if (!ThisEquipItam())
        {
            //갯수 입력 UI 뽕!
            //inputFeild.SetActive(true);
            //_ShopButton.InputFeild();
            //ShopButtonUI.Instance.InputFeild();
            InputFeild();
            Debug.Log($"Item : {itemData.ID} Buy");
        }
        else
        {
            //inventory에 쏙!
            //InventoryManager
            //AddItem(this.itemData);
            Debug.Log($"Item : {itemName} Buy");
        }
    }

    //public void ButtonSell()
    //{
    //    if (!ThisEquipItam())
    //    {
    //        //갯수 입력 UI 뽕!
    //    }
    //    else
    //    {
    //        //inventory에 쏙!
    //    }
    //}
    

    public void InputFeild()
    {
        //아이템의 타입의 따라 
        inputFeild_Obj.SetActive(true);
        inputField.onEndEdit.AddListener(delegate { EndEditEvent(inputField); });
    }

    public void EndEditEvent(TMP_InputField inputField)
    {
        string _inputNum = inputField.text;
        Debug.Log($" itemData.ID : {itemData}.{itemData.ID}");
        Debug.Log($" itemData,Quantity :{_inputNum}");

        //InveoryManager.Instance.AddItem(itemData, _inputNum);
        if (!inputFeild_Obj.activeInHierarchy)
        {
            inputField.onEndEdit.RemoveAllListeners();
        }
        inputFeild_Obj.SetActive(false);
    }

    public void ExitButton()
    {
        inputField.onEndEdit.RemoveAllListeners();
        inputFeild_Obj.SetActive(false);
    }

    //public void OnEndEditEvent(string _inputNum)
    //{
    //    //Item ThisItem = this.itemData;

    //    Debug.Log($" itemData.ID : {itemData}.{itemData.ID}");
    //    Debug.Log($" itemData,Quantity :{_inputNum}");

    //    //InveoryManager.Instance.AddItem(itemData, _inputNum);
    //}

    public void Btn_InputNumUI_Exit()
    {
        inputFeild_Obj.SetActive(false);
    }
}
