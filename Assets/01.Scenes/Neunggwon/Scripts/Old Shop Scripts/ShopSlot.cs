using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEngine.Rendering.DebugUI;


public class ShopSlot : MonoBehaviour
{
    [Header("#ItemDataInfo")]
    public Item itemData; //������ �����͸� �޴´�!
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

        if (0 == 0) // itemType�� ���� �����ؾ� �� �� �߰� (�ӽ���)
        {

        }
    }
    private bool ThisEquipItam() //test
    {
        //Debug.Log($"ItemType : {itemData.type}");
        //�������� �������� Ȯ��...
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
            //���� �Է� UI ��!
            //inputFeild.SetActive(true);
            //_ShopButton.InputFeild();
            //ShopButtonUI.Instance.InputFeild();
            InputFeild();
            Debug.Log($"Item : {itemData.ID} Buy");
        }
        else
        {
            //inventory�� ��!
            //InventoryManager
            //AddItem(this.itemData);
            Debug.Log($"Item : {itemName} Buy");
        }
    }

    //public void ButtonSell()
    //{
    //    if (!ThisEquipItam())
    //    {
    //        //���� �Է� UI ��!
    //    }
    //    else
    //    {
    //        //inventory�� ��!
    //    }
    //}
    

    public void InputFeild()
    {
        //�������� Ÿ���� ���� 
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
