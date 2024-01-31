using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //테스트용 적 데미지
    public int EnemyDMG = 10;
    //체력
    public int PlayerMaxHP;
    public int PlayerHP;

    public Slider slider;

    private void Awake()
    {
        PlayerMaxHP = 100;
    }

    private void Start()
    {
        PlayerHP = PlayerMaxHP;
        UIMaxHealth(PlayerHP);
    }

    private void Update()
    {
        if (slider != null)
        {
            UIHealth(PlayerHP);
        }

        //T버튼 누르면 피격 테스트
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayerHit();
            Debug.Log("Player HP: " + PlayerHP);
        }
    }

    public void UIMaxHealth(int PlayerHP)
    {
        //HP UI
        //Debug.Log(slider);
        slider.maxValue = PlayerHP;
        slider.value = PlayerHP;
    }
    public void UIHealth(int PlayerHP)
    {
        //HP UI
        slider.value = PlayerHP;
    }


    public void PlayerHit()
    {
        //플레이어가 맞았을 때 HP 닳는 양과 죽음처리
        if (PlayerHP > 0)
        {
            // HP 감소
            //EnemyDMG = 적 공격력 추가해야됨
            PlayerHP -= EnemyDMG;

            // HP가 0 이하일 경우 Die
            if (PlayerHP <= 0)
            {
                Die();
            }
        }
    }
    private void Die()
    {
        // 플레이어 사망
        Debug.Log("플레이어 사망");
    }
}
