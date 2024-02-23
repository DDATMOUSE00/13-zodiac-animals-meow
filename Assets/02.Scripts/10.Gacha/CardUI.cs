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

    // 카드의 정보를 초기화
    public void CardUISet(Card card)
    {
        card_Img.sprite = card._cardImage;
        cardName_Tex.text = card._cardName;
    }

    // 카드가 클릭되면 뒤집는 애니메이션 재생
    public void OnPointerDown(PointerEventData eventData)
    {
        animator.SetTrigger("Flip");
    }
}
