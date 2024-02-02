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
    [SerializeField] private Button SellShopButton;
    [SerializeField] private Button ExitButton;

    [SerializeField] private Outline Outline_weaponShopButton;
    [SerializeField] private Outline Outline_potionShopButton;
    [SerializeField] private Outline Outline_SellShopButton;

    [Header("#Display_Shop")]
    [SerializeField] private GameObject Shop;
    [SerializeField] private GameObject Shop_WeaponDisplay;
    [SerializeField] private GameObject Shop_PotionDisplay;
    [SerializeField] private GameObject Shop_SellDisplay;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        //DisplayWeaponShop();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            OnShopButton();
        }
    }

    public void DisplayWeaponShop()
    {
        Shop_WeaponDisplay.SetActive(true);
        Outline_weaponShopButton.enabled = true;
        Shop_PotionDisplay.SetActive(false);
        Outline_potionShopButton.enabled = false;
        Shop_SellDisplay.SetActive(false);
        Outline_SellShopButton.enabled = false;
    }

    public void DisplayPotionShop()
    {
        Shop_WeaponDisplay.SetActive(false);
        Outline_weaponShopButton.enabled = false;
        Shop_PotionDisplay.SetActive(true);
        Outline_potionShopButton.enabled = true;
        Shop_SellDisplay.SetActive(false);
        Outline_SellShopButton.enabled = false;

    }

    public void DisplaySellShop()
    {
        Shop_PotionDisplay.SetActive(false);
        Outline_weaponShopButton.enabled = false;
        Shop_WeaponDisplay.SetActive(false);
        Outline_potionShopButton.enabled = false;
        Shop_SellDisplay.SetActive(true);
        Outline_SellShopButton.enabled = true;
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
}
