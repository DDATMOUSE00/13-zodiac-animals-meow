using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot1 : MonoBehaviour
{
    [Header("#ItemDataInfo")]
    public int itemData_ID;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI itemCost;

    [Header("#InputFeild")]
    [SerializeField] private GameObject inputFeild_Obj;
    [SerializeField] private TMP_InputField inputField;

    [SerializeField] private Button InputFeild_ExitButton;
    void Start()
    {
        
    }

    //private bool ThisEquipItem()
    //{
    //    ///<summary
    //    ///장착하는 아이템인지 확인
    //    ///</summary>

    //}

    //public void ButtonBuy()
    //{
    //    if (!ThisEquipItam())
    //    {
    //        //갯수 입력 UI 뽕!
    //        //inputFeild.SetActive(true);
    //        //_ShopButton.InputFeild();
    //        //ShopButtonUI.Instance.InputFeild();
    //        InputFeild();
    //        Debug.Log($"Item : {itemData.ID} Buy");
    //    }
    //    else
    //    {
    //        //inventory에 쏙!
    //        //InventoryManager
    //        //AddItem(this.itemData);
    //        Debug.Log($"Item : {itemName} Buy");
    //    }
    //}


    public void InputFeild()
    {
        //아이템의 타입의 따라 
        inputFeild_Obj.SetActive(true);
        //inputField.onEndEdit.AddListener(delegate { EndEditEvent(inputField); });

    }

    //public void EndEditEvent(TMP_InputField inputField)
    //{
    //    string _inputNum = inputField.text;
    //    Debug.Log($" itemData.ID : {itemData}.{itemData.ID}");
    //    Debug.Log($" itemData,Quantity :{_inputNum}");

    //    //InveoryManager.Instance.AddItem(itemData, _inputNum);
    //    inputField.onEndEdit.RemoveAllListeners();
    //    inputFeild_Obj.SetActive(false);
    //}

    //public void OnEndEditEvent(string _inputNum)
    //{
    //    //Item ThisItem = this.itemData;

    //    Debug.Log($" itemData.ID : {itemData}.{itemData.ID}");
    //    Debug.Log($" itemData,Quantity :{_inputNum}");

    //    //InveoryManager.Instance.AddItem(itemData, _inputNum);
    //}

    //public void Btn_InputNumUI_Exit()
    //{
    //    inputFeild_Obj.SetActive(false);
    //}
}
