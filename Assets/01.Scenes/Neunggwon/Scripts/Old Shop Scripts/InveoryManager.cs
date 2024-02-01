using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 추가 및 제거 역활.
/// </summary>
public class InveoryManager : MonoBehaviour
{
    public static InveoryManager Instance;
    //public Item _itemData;
    //inveotry의 있는 아이템 슬롯 리스트를 가지고 온다.

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

    public void AddItem(Item itemData)
    {
        //아이템의 타입을 확인한다.
        // - 자원 타입
        //만약 있다면 갯수++;
        //없다면 아이템의 슬롯의 갯수만큼 확인하여 슬롯에 비어있는 곳에 쏙!
        ////////////////////////////////////////////////////////////////////////////////////

        // - 무기 타입
        //아이템의 슬롯의 갯수만큼 확인하여 슬롯에 비어있는 곳에 쏙!
        for (int i = 0; i < ItemManager.I.slots.Length; i++)
        {
            if (ItemManager.I.slots[i] != itemData)
            {
                //아이템 추가
                Debug.Log("무기 아이템 추가!");
            }
        }
    }

    public void AddItem(DraggableItem item, string inputNum)
    {
        int _inputNum = int.Parse(inputNum);
        //Debug.Log($"{item},{inputNum}");
        Debug.Log($" item.ID : {item}.{item.item.id}, item,Quantity :{inputNum}");

        // - 자원 타입
        //만약 있다면 갯수++;
        //없다면 아이템의 슬롯의 갯수만큼 확인하여 슬롯에 비어있는 곳에 쏙!
        for (int i = 0; i < ItemManager.I.slots.Length; i++)
        {
            if (ItemManager.I.slots[i].GetComponentInChildren<DraggableItem>() != item)
            {
                //아이템 추가
                Debug.Log("자원 아이템 추가!");
                Debug.Log($" item.ID : {item}.{item.item.id}, item,Quantity :{inputNum}");
                //ItemManager.Instance.slots[i] = item;
                //item.Quantity = inputNum.ToString();
                //AddItemAtEmptySlot()
                break;
            }
            else
            {
                for (int j = 0; j == _inputNum; j++)
                {
                    //ItemManager.Instance.slots[i].
                    Debug.Log("자원 아이템 더하기!!");
                }
            }
        }
    }

    public void DeleteItem(Item itemData)
    {
        // - 무기 타입
        //아이템의 슬롯의 갯수만큼 확인하여 슬롯중 같은 데이터가 있는지 확인 후 제거;
        for (int i = 0; i < ItemManager.I.slots.Length; i++)
        {
            if (ItemManager.I.slots[i] == itemData)
            {
                //아이템 제거
                Debug.Log("무기 아이템 제거!");
            }
        }
    }

    public void DeleteItem(Item itemData, int inputNum)
    {
        // - 자원 타입
        //아이템의 슬롯의 갯수만큼 확인하여 슬롯중 같은 데이터가 있는지 확인, 갯수 확인 후 제거 및, count--;

        for (int i = 0; i < ItemManager.I.slots.Length; i++)
        {
            if (ItemManager.I.slots[i] == itemData)
            {
                //아이템 제거
                Debug.Log("자원 아이템 추가!");
                //ItemManager.Instance.slots[i]. = item;
               // item.Quantity = inputNum.ToString();
            }
            else
            {
                for (int j = 0; j == inputNum; j++)
                {
                    //ItemManager.Instance.slots[i].
                    Debug.Log("자원 아이템 더하기!!");
                }
            }
        }
    }

    //아이템의 슬롯의 갯수만큼 확인하여 슬롯에 비어있는 곳에 쏙!
    //메서드로 만들자!!
    //public void CheckedInventory(Item item, int inputNum)
    //{
    //    for (int i = 0; i < ItemManager.Instance.slots.Length; i++)
    //    {
    //        if (ItemManager.Instance.slots[i] != item)
    //        {

    //            //아이템 추가
    //            Debug.Log("아이템 추가!");
    //            //ItemManager.Instance.slots[i] = 
    //        }
    //        else
    //        {
    //            //제거
    //        }
    //    }
    //}
    
}
