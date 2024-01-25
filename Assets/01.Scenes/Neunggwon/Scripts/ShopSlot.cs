using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


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

    private void Start()
    {
        Setting();
    }

    //public Item ItemData(Item _itemData)
    //{
    //    itemData = _itemData;
    //    return itemData;
    //}

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
        Debug.Log($"ItemType : {itemData.type}");
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
            ShopButtonUI.Instance.InputFeild();
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


    public void InputNum()
    {
        //int inputNum = int.Parse(inputField.text);
        ///<summary>
        ///�ӽ�..
        ///�÷��̾� ��� Ȯ��
        /// </summary>

        //if (PlayerStatus.Gold >= itemData.Cost * inputNum)
        //{
        //    PlayerStatus.Gold -= (itemData.Cost * inputNum)
        //    AddItem(this.itemData, inputNum)
        //}
        //else
        //{
        //    Debug.Log($"�÷��̾� ��� ����!!");
        //}

    }

    public void InputFeild()
    {
        //�������� Ÿ���� ���� 
        inputFeild_Obj.SetActive(true);
    }

    public void OnEndEditEvent(string _inputNum)
    {
        //textMeshProUGUI.text = _inputNum;
        //int inputNum = int.Parse(textMeshProUGUI.text);
        int inputNum = int.Parse(_inputNum);
        Debug.Log($" {itemData.ID},{inputNum}");

        InveoryManager.Instance.AddItem(itemData, inputNum);
    }

    public void Btn_InputNumUI_Exit()
    {
        inputFeild_Obj.SetActive(false);
    }
}
