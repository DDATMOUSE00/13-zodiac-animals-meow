using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtonUI : MonoBehaviour
{
    public static ShopButtonUI Instance;

    [Header("#Button")]
    [SerializeField] private Button weaponShopButton;
    [SerializeField] private Button potionShopButton;
    [SerializeField] private Button ExitButton;

    [SerializeField] private Outline Outline_weaponShopButton;
    [SerializeField] private Outline Outline_potionShopButton;

    [Header("#Display_Shop")]
    [SerializeField] private GameObject Shop;
    [SerializeField] private GameObject Shop_WeaponDisplay;
    [SerializeField] private GameObject Shop_PotionDisplay;

    [Header("#InputFeild")]
    [SerializeField] private GameObject inputFeild_Obj;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    [SerializeField] private Button InputFeild_ExitButton;
    //public int inputNum;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            OnShopButton();
            //Debug.Log("OnShop");
        }
    }

    public void DisplayWeaponShop()
    {
        Shop_WeaponDisplay.SetActive(true);
        Outline_weaponShopButton.enabled = true;
        Shop_PotionDisplay.SetActive(false);
        Outline_potionShopButton.enabled = false;
        //Debug.Log("Shop_WeaponDisplay.SetActive(true)");
    }

    public void DisplayPotionShop()
    {
        Shop_WeaponDisplay.SetActive(false);
        Outline_weaponShopButton.enabled = false;
        Shop_PotionDisplay.SetActive(true);
        Outline_potionShopButton.enabled = true;
        //Debug.Log("hop_PotionDisplay.SetActive(true)");
    }

    public void OnShopButton()
    {
        Toggle();
    }

    public void Toggle()
    {
        if (Shop.activeInHierarchy)
        {
            Shop.SetActive(false);
        }
        else
        {
            Shop.SetActive(true);
            DisplayWeaponShop();
        }
    }

    /// <summary>
    /// InputFeild
    /// </summary>
    public void InputFeild()
    {
        //아이템의 타입의 따라 
        inputFeild_Obj.SetActive(true);
        inputField.onEndEdit.AddListener(delegate { ChangdInputField(inputField); });

    }

    public void ChangdInputField(TMP_InputField inputField)
    {
        Debug.Log(inputField.name + "UI");

    }

    //public void OnEndEditEvent(string _inputNum)
    //{
    //    textMeshProUGUI.text = _inputNum;
    //    Debug.Log($" {textMeshProUGUI.text}");
    //}

    public void InputNumUI_Exit()
    {
        inputFeild_Obj.SetActive(false);
        //textMeshProUGUI.text = "inputEnter";
    }
}
