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

    //���� ������ ���;; - �̰� ���� ��ü���� �˰� ������ �ɰ� ������..
    //���� ���⼭ ã�ƾ� �� ���� Ư�� �������� ����� ã�� ���� �͸� �ϸ� �ɵ�?
    //���� �������� ���� �� ��� �� ���ΰ�?
    //���� �������ϰ� ���� �������� �������� ������ bundle�� ���� ���� Count�� 0�̸� ItemData == Null -> Set���ñ� = false;

    //void Start()
    //{
    //}

    //void Update()
    //{
    //}
}
