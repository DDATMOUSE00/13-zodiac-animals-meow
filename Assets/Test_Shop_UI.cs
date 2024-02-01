using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Test_Shop_UI : MonoBehaviour
{
    [Header("#Display_Shop")]
    [SerializeField] private GameObject Shop;
    [SerializeField] private GameObject Shop_WeaponDisplay;
    [SerializeField] private GameObject Shop_PotionDisplay;
    //[SerializeField] private Button Shop_ExitButton;

    [Header("#InputField")]
    [SerializeField] private GameObject inputFeild_Obj;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button InputFeild_ExitButton;
    // Start is called before the first frame update
    void Start()
    {
        //InputNumUI_Exit();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            OnShopButton();
            //Debug.Log("OnShop");
        }
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
        }
    }

    public void DisplayWeaponShop()
    {
        Shop_WeaponDisplay.SetActive(true);
        //Outline_weaponShopButton.enabled = true;
        Shop_PotionDisplay.SetActive(false);
        //Outline_potionShopButton.enabled = false;
    }

    public void DisplayPotionShop()
    {
        Shop_WeaponDisplay.SetActive(false);
        //Outline_weaponShopButton.enabled = false;
        Shop_PotionDisplay.SetActive(true);
        //Outline_potionShopButton.enabled = true;
    }

    //public void InputNumUI_Exit()
    //{
    //    Debug.Log("ExitEvent!!");
    //    inputField.onEndEdit.RemoveAllListeners();
    //    inputFeild_Obj.SetActive(false);
    //}
}
