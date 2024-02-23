using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    public BuyShop buyShop;
    public SellShop sellShop;

    public GameObject Shop;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowInventorySlotManager()
    {
        sellShop.ShowInventorySlot();
    }

    public void OnShopButton()
    {
        Debug.Log(Shop.activeInHierarchy);
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
}
