using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ �߰� �� ���� ��Ȱ.
/// </summary>
public class InveoryManager : MonoBehaviour
{
    //public Item _itemData;
    //inveotry�� �ִ� ������ ���� ����Ʈ�� ������ �´�.

    public void AddItem(Item itemData)
    {
        //�������� Ÿ���� Ȯ���Ѵ�.
        // - �ڿ� Ÿ��
        //���� �ִٸ� ����++;
        //���ٸ� �������� ������ ������ŭ Ȯ���Ͽ� ���Կ� ����ִ� ���� ��!
        ////////////////////////////////////////////////////////////////////////////////////

        // - ���� Ÿ��
        //�������� ������ ������ŭ Ȯ���Ͽ� ���Կ� ����ִ� ���� ��!
        for (int i = 0; i < ItemManager.Instance.slots.Length; i++)
        {
            if (ItemManager.Instance.slots[i] != itemData)
            {
                //������ �߰�
                Debug.Log("���� ������ �߰�!");
            }
        }
    }

    public void AddItem(Item itemData, int inputNum)
    {
        // - �ڿ� Ÿ��
        //���� �ִٸ� ����++;
        //���ٸ� �������� ������ ������ŭ Ȯ���Ͽ� ���Կ� ����ִ� ���� ��!
        for (int i = 0; i < ItemManager.Instance.slots.Length; i++)
        {
            if (ItemManager.Instance.slots[i] != itemData)
            {
                //������ �߰�
                Debug.Log("�ڿ� ������ �߰�!");
                //ItemManager.Instance.slots[i]. = itemData;
                itemData.Quantity = inputNum.ToString();
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

    public void DeleteItem(Item itemData)
    {
        // - ���� Ÿ��
        //�������� ������ ������ŭ Ȯ���Ͽ� ������ ���� �����Ͱ� �ִ��� Ȯ�� �� ����;
        for (int i = 0; i < ItemManager.Instance.slots.Length; i++)
        {
            if (ItemManager.Instance.slots[i] != itemData)
            {
                //������ �߰�
                Debug.Log("�ڿ� ������ �߰�!");
                //ItemManager.Instance.slots[i]. = itemData;
            }
        }
    }

    public void DeleteItem(Item itemData, int inputNum)
    {
        // - �ڿ� Ÿ��
        //�������� ������ ������ŭ Ȯ���Ͽ� ������ ���� �����Ͱ� �ִ��� Ȯ��, ���� Ȯ�� �� ���� ��, count--;

        for (int i = 0; i < ItemManager.Instance.slots.Length; i++)
        {
            if (ItemManager.Instance.slots[i] != itemData)
            {
                //������ �߰�
                Debug.Log("�ڿ� ������ �߰�!");
                //ItemManager.Instance.slots[i]. = itemData;
                itemData.Quantity = inputNum.ToString();
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
