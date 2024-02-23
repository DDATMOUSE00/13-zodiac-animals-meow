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
    public int _weight; // 표라고 생각하면 댐 ㅇㅇ
    public int _id;

    // 깊은 복사
    public Card(Card card)
    {
        _cardName = card._cardName;
        _cardImage = card._cardImage;
        _cardGrade = card._cardGrade;
        _weight = card._weight;
        _id = card._id;
    }

}
