using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class RandomSelect : MonoBehaviour
{
    public List<Card> deck = new List<Card> ();
    public int total = 0;
    public List<Card> result = new List<Card>();

    public void ResultSelect()
    {
        result.Add(RandomCard());
    }
    // 카드 클래스 반환 : 가중치 랜덤 적용
    public Card RandomCard()
    {
        int weight = 0;
        int selectNum = 0;

        selectNum = Mathf.RoundToInt(total * Random.Range(0.0f, 1.0f));

        for(int i = 0; i < deck.Count; i++)
        {
            weight += deck[i]._weight;

            if(selectNum <= weight)
            {
                Card temp = new Card(deck[i]);
                return temp;
            }
        }
        return deck[Random.Range(0, deck.Count)];
    }

    private void Start()
    {
        for(int i = 0; i < deck.Count; i++) 
        {
            total += deck[i]._weight;
        }
    }
}
