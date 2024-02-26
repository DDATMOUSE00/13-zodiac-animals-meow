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

    // 상태변수
    private bool isActivated = false;

    [SerializeField]
    private GameObject go_BaseUI; // 기본 베이스 UI

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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            Window();

        if (Input.GetKeyDown(KeyCode.Escape))
            Cancel();
    }

    #region Tab window
    public void Window()
    {
        Debug.Log(isActivated);
        if (!isActivated)
        {
            OpenWindow();
        }
        else
        {
            CloseWindow();
        }
    }

    public void Cancel()
    {
        isActivated = false;

        go_BaseUI.SetActive(false);
    }

    public void OpenWindow()
    {
        isActivated = true;
        go_BaseUI.SetActive(true);
    }

    public void CloseWindow()
    {
        isActivated = false;
        go_BaseUI.SetActive(false);
    }
    #endregion

    public void ResultSelect()
    {
        for (int i = 0; i < 5; i++)
        {
            Card selectedCard = RandomCard();

            // 스토리 조각 업데이트
            UpdateStoryPieces(selectedCard);

            // 스킬 조각(?) 업데이트
            // 구현~~~

            result.Add(selectedCard);
            CardUI cardUI = Instantiate(cardprefab, parent).GetComponent<CardUI>();
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

    public void UpdateStoryPieces(Card card)
    {
        // 동물 카드인지 확인
        if (card._id < 99)
        {

            LibraryManager.I.AddBooks(card._id);
        }
    }
}
