using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    public BuyShop buyShop;
    public SellShop sellShop;
    public TextMeshProUGUI playerGoldText;
    [SerializeField] private GameObject inputFeild_Obj;
    [SerializeField] private TMP_InputField inputField;
    public InputField inputFieldPrefab;

    public GameObject Shop;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ShopUpDate();
    }

    private void Update()
    {
        //юс╫ц
        //if (Shop.gameObject.activeInHierarchy)
        //{
        //    ShopUpDate();
        //}
        //else
        //{
        //    ShopUpDate();
        //}
    }

    public void ShopUpDate()
    {
        var playerGold = GameManager.Instance.player.GetComponent<PlayerGold>();
        Debug.Log(playerGold.Gold);
        playerGoldText.text = playerGold.Gold.ToString("#,##0G");
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
    public void Open()
    {
        Shop.SetActive(true);
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

    public void InputNumUI_Exit()
    {
        inputFieldPrefab.Cancle();
        inputField.onSubmit.RemoveAllListeners();
        inputFeild_Obj.SetActive(false);
    }
}
