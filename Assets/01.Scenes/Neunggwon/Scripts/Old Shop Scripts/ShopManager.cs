using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    public BuyShop buyShop;
    public SellShop sellShop;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowInventorySlotManager()
    {
        sellShop.ShowInventorySlot();
    }
}
