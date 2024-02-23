using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Shrine : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public int total = 0;

    public List<Card> result = new List<Card>();  // 랜덤하게 선택된 카드를 담을 리스트
    public Transform parent;
    public GameObject cardprefab;

    void Start()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            // 스크립트가 활성화 되면 카드 덱의 모든 카드의 총 가중치를 구해줌
            total += deck[i]._weight;
        }
        // 실행
        ResultSelect();
    }

    public void ResultSelect()
    {
        for (int i = 0; i < 10; i++)
        {
            // 가중치 랜덤을 돌리면서 결과 리스트에 넣어쥼 ㅇㅇ
            result.Add(RandomCard());
            // 비어 있는 카드를 생성하고
            CardUI cardUI = Instantiate(cardprefab, parent).GetComponent<CardUI>();
            // 생성 된 카드에 결과 리스트의 정보를 넣어줌 ㅋ
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
