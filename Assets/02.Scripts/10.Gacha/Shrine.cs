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

    // ���º���
    private bool isActivated = false;

    [SerializeField]
    private GameObject go_BaseUI; // �⺻ ���̽� UI

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

            // ���丮 ���� ������Ʈ
            UpdateStoryPieces(selectedCard);

            // ��ų ����(?) ������Ʈ
            // ����~~~

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
        // ���� ī������ Ȯ��
        if (card._id < 99)
        {

            LibraryManager.I.AddBooks(card._id);
        }
    }
}
