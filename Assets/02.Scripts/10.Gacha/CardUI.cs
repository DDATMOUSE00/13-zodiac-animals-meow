using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CardUI : MonoBehaviour, IPointerDownHandler
{
    public Image card_Img;
    public TextMeshProUGUI cardName_Tex;
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // ī���� ������ �ʱ�ȭ
    public void CardUISet(Card card)
    {
        card_Img.sprite = card._cardImage;
        cardName_Tex.text = card._cardName;
    }

    // ī�尡 Ŭ���Ǹ� ������ �ִϸ��̼� ���
    public void OnPointerDown(PointerEventData eventData)
    {
        animator.SetTrigger("Flip");
    }
}
