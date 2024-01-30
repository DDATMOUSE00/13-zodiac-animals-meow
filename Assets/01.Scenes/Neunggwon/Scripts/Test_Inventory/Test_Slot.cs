using Spine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Test_Slot : MonoBehaviour
{
    public Item itemData;
    public int bundle;

    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI bundleText;
    [SerializeField] private GameObject SlotUI;

    public int Index;
    
    private void Awake()
    {
        //icon = GetComponentInChildren<Image>();
        bundleText = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Start()
    {
        Setting(itemData);
    }
    private void Update()
    {
        if (itemData)
        {
            Setting(itemData);
        }
        else
        {
            Clear();
        }
    }
    public void Setting(Item _itemData)
    {

        SlotUI.SetActive(true);
        //_itemData.Icon = icon.sprite;
        bundleText.text = bundle.ToString();
    }

    public void Clear()
    {
        Debug.Log($"itemData = {itemData}");
        //_itemData.Icon = null;
        bundleText.text = string.Empty;
        SlotUI.SetActive(false);
    }
}
