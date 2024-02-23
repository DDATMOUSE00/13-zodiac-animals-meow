using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Shrine : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public int total = 0;

    public List<Card> result = new List<Card>();  // �����ϰ� ���õ� ī�带 ���� ����Ʈ
    public Transform parent;
    public GameObject cardprefab;

    void Start()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            // ��ũ��Ʈ�� Ȱ��ȭ �Ǹ� ī�� ���� ��� ī���� �� ����ġ�� ������
            total += deck[i]._weight;
        }
        // ����
        ResultSelect();
    }

    public void ResultSelect()
    {
        for (int i = 0; i < 10; i++)
        {
            // ����ġ ������ �����鼭 ��� ����Ʈ�� �־��� ����
            result.Add(RandomCard());
            // ��� �ִ� ī�带 �����ϰ�
            CardUI cardUI = Instantiate(cardprefab, parent).GetComponent<CardUI>();
            // ���� �� ī�忡 ��� ����Ʈ�� ������ �־��� ��
            cardUI.CardUISet(result[i]);
        }
    }

    public Card RandomCard()
    {
        int weight = 0;
        int selectNum = 0;

        selectNum = Mathf.RoundToInt(total * Random.Range(0.0f, 1.0f));

        for (int i = 0; i < deck.Count; i++)
        {
            weight += deck[i]._weight;
            if (selectNum <= weight)
            {
                Card temp = new Card(deck[i]);
                return temp;
            }
        }
        return null;
    }
}
