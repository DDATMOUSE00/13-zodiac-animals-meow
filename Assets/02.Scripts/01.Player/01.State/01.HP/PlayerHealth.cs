using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private SkeletonAnimation Anim;


    //테스트용 적 데미지
    public int EnemyDMG = 10;
    //체력
    public int PlayerMaxHP;
    public int PlayerHP;

    public Slider slider;

    //무적
    public bool IsInvincible;

    private void Awake()
    {
        PlayerMaxHP = 100;
        Anim = GetComponentInChildren<SkeletonAnimation>();
    }

    private void Start()
    {
        PlayerHP = PlayerMaxHP;
        UIMaxHealth(PlayerHP);
        IsInvincible = false;
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


    public void PlayerHit(int EnemyDamage)
    {
        if (!IsInvincible)
        {
            //플레이어가 맞았을 때 HP 닳는 양과 죽음처리
            if (PlayerHP > 0)
            {
                // HP 감소
                //EnemyDMG = 적 공격력 추가해야됨
                PlayerHP -= EnemyDMG;
                if (!Anim.AnimationState.GetCurrent(0).Animation.Name.Equals("sd_damage"))
                {
                    Anim.AnimationState.SetAnimation(0, "sd_damage", false);
                    //무적
                    Invisible();
                }
                // HP가 0 이하일 경우 Die
                if (PlayerHP <= 0)
                {
                    Die();
                }
            }
        }
    }
    public void Invisible()
    {
        //무적
        Debug.Log("무적");
        IsInvincible = true;
    
        Invoke("DisInvisible", 0.5f);
    }
    public void DisInvisible()
    {
        //무적 해제
        Debug.Log("무적 해제");
        IsInvincible = false;
    }

    private void Die()
    {
        // 플레이어 사망
        Debug.Log("플레이어 사망");
    }
}
