using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardGrade
{
    SSS,
    S,
    A,
    B,
    F
}

[System.Serializable]
public class Card
{
    public string _cardName;
    public Sprite _cardImage;
    public CardGrade _cardGrade;
    public int _weight; // ǥ��� �����ϸ� �� ����
    public int _id;

    // ���� ����
    public Card(Card card)
    {
        _cardName = card._cardName;
        _cardImage = card._cardImage;
        _cardGrade = card._cardGrade;
        _weight = card._weight;
        _id = card._id;
    }

}
