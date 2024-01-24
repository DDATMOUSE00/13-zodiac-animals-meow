using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtonUI : MonoBehaviour
{
    [Header("#Button")]
    [SerializeField] private Button weaponShopButton;
    [SerializeField] private Button potionShopButton;
    [SerializeField] private Button ExitButton;

    [Header("#Display_Shop")]
    [SerializeField] private GameObject Shop;
    [SerializeField] private GameObject Shop_WeaponDisplay;
    [SerializeField] private GameObject Shop_PotionDisplay;

    [Header("#InputFeild")]
    [SerializeField] private GameObject inputFeild;
    public InputField inputField;
    public string inputNum = null;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            OnShopButton();
            Debug.Log("OnShop");
        }
    }

    public void DisplayWeaponShop()
    {
        Shop_WeaponDisplay.SetActive(true);
        Shop_PotionDisplay.SetActive(false);
        Debug.Log("Shop_WeaponDisplay.SetActive(true)");
    }

    public void DisplayPotionShop()
    {
        Shop_WeaponDisplay.SetActive(false);
        Shop_PotionDisplay.SetActive(true);
        Debug.Log("hop_PotionDisplay.SetActive(true)");
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

    public void InputFeild()
    {
        //아이템의 타입의 따라 
        inputFeild.SetActive(true);
    }

    public void InputNum()
    {
        inputNum = inputField.text;
        //플레이어가 가지고 있는 Gold와 비교

    }
}
