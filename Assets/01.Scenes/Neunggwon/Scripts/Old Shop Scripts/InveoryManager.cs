using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ �߰� �� ���� ��Ȱ.
/// </summary>
public class InveoryManager : MonoBehaviour
{
    public static InveoryManager Instance;
    //public Item _itemData;
    //inveotry�� �ִ� ������ ���� ����Ʈ�� ������ �´�.

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
        //�������� Ÿ���� Ȯ���Ѵ�.
        // - �ڿ� Ÿ��
        //���� �ִٸ� ����++;
        //���ٸ� �������� ������ ������ŭ Ȯ���Ͽ� ���Կ� ����ִ� ���� ��!
        ////////////////////////////////////////////////////////////////////////////////////

        // - ���� Ÿ��
        //�������� ������ ������ŭ Ȯ���Ͽ� ���Կ� ����ִ� ���� ��!
        for (int i = 0; i < ItemManager.I.slots.Length; i++)
        {
            if (ItemManager.I.slots[i] != itemData)
            {
                //������ �߰�
                Debug.Log("���� ������ �߰�!");
            }
        }
    }

    public void AddItem(Item itemData, string inputNum)
    {
        int _inputNum = int.Parse(inputNum);
        //Debug.Log($"{itemData},{inputNum}");
        Debug.Log($" itemData.ID : {itemData}.{itemData.id}, itemData,Quantity :{inputNum}");

        // - �ڿ� Ÿ��
        //���� �ִٸ� ����++;
        //���ٸ� �������� ������ ������ŭ Ȯ���Ͽ� ���Կ� ����ִ� ���� ��!
        for (int i = 0; i < ItemManager.I.slots.Length; i++)
        {
            if (ItemManager.I.slots[i] != itemData)
            {
                //������ �߰�
                Debug.Log("�ڿ� ������ �߰�!");
                Debug.Log($" itemData.ID : {itemData}.{itemData.id}, itemData,Quantity :{inputNum}");
                //ItemManager.Instance.slots[i] = itemData;
                //itemData.Quantity = inputNum.ToString();
                //AddItemAtEmptySlot()
                break;
            }
            else
            {
                for (int j = 0; j == _inputNum; j++)
                {
                    //ItemManager.Instance.slots[i].
                    Debug.Log("�ڿ� ������ ���ϱ�!!");
                }
            }
        }
    }

    public void DeleteItem(Item itemData)
    {
        // - ���� Ÿ��
        //�������� ������ ������ŭ Ȯ���Ͽ� ������ ���� �����Ͱ� �ִ��� Ȯ�� �� ����;
        for (int i = 0; i < ItemManager.I.slots.Length; i++)
        {
            if (ItemManager.I.slots[i] == itemData)
            {
                //������ ����
                Debug.Log("���� ������ ����!");
            }
        }
    }

    public void DeleteItem(Item itemData, int inputNum)
    {
        // - �ڿ� Ÿ��
        //�������� ������ ������ŭ Ȯ���Ͽ� ������ ���� �����Ͱ� �ִ��� Ȯ��, ���� Ȯ�� �� ���� ��, count--;

        for (int i = 0; i < ItemManager.I.slots.Length; i++)
        {
            if (ItemManager.I.slots[i] == itemData)
            {
                //������ ����
                Debug.Log("�ڿ� ������ �߰�!");
                //ItemManager.Instance.slots[i]. = itemData;
               // itemData.Quantity = inputNum.ToString();
            }
            else
            {
                for (int j = 0; j == inputNum; j++)
                {
                    //ItemManager.Instance.slots[i].
                    Debug.Log("�ڿ� ������ ���ϱ�!!");
                }
            }
        }
    }

    //�������� ������ ������ŭ Ȯ���Ͽ� ���Կ� ����ִ� ���� ��!
    //�޼���� ������!!
    //public void CheckedInventory(Item itemData, int inputNum)
    //{
    //    for (int i = 0; i < ItemManager.Instance.slots.Length; i++)
    //    {
    //        if (ItemManager.Instance.slots[i] != itemData)
    //        {

    //            //������ �߰�
    //            Debug.Log("������ �߰�!");
    //            //ItemManager.Instance.slots[i] = 
    //        }
    //        else
    //        {
    //            //����
    //        }
    //    }
    //}
    
}
