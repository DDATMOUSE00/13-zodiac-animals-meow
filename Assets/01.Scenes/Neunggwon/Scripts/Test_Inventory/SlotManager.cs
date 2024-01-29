using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class SlotManager : MonoBehaviour
{
    //public Item itemData;
    public static SlotManager Instance;
    public List<Test_Slot> slots = new List<Test_Slot>();

    private void Awake()
    {
        Instance = this;
        //slots[1].Setting(itemData);
    }


    public int SelecteItemQuantity(Item _itemData)
    {
        int selecItem_Quantity = 0;

        for (int i = 0; i < slots.Count; i++)
        {
            if (_itemData.ID == slots[i].itemData.ID)
            {
                selecItem_Quantity += slots[i].bundle;
            }
        }
        return selecItem_Quantity;
    }

    //슬롯 마다의 계수;; - 이건 슬롯 자체에서 알고 있으면 될거 같은데..
    //내가 여기서 찾아야 할 것은 특정 아이템의 계수를 찾아 오는 것만 하면 될듯?
    //만약 아이템을 버릴 때 어떻게 할 것인가?
    //슬롯 뺑뺑이하고 버릴 아이템의 갯수에서 슬롯의 bundle을 빼고 만약 Count가 0이면 ItemData == Null -> Set뭐시기 = false;

    //void Start()
    //{
    //}

    //void Update()
    //{
    //}
}
