using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class PlayerHealth : MonoBehaviour
{
    private SkeletonAnimation Anim;
    private PlayerMovement _PlayerMovement;

    //체력UI
    //public TextMeshProUGUI MinHPTEXT;
    //public TextMeshProUGUI MaxHPTEXT;

    //피격 데미지UI
    public GameObject HitDamageText;
    public Transform HitDamagePoint;

    //체력
    public int PlayerHP { get; set; }

    public int PlayerMaxHP { get; set; }

    public Slider slider;

    //무적
    public bool IsInvincible;

    private void Awake()
    {
        PlayerMaxHP = 100;
        Anim = GetComponentInChildren<SkeletonAnimation>();
        _PlayerMovement = GetComponentInChildren<PlayerMovement>();
    }

    private void Start()
    {
        PlayerHP = PlayerMaxHP;
        UIMaxHealth(PlayerMaxHP);
        IsInvincible = false;
        //MinHPTEXT.text = PlayerHP.ToString();
        //MaxHPTEXT.text = PlayerMaxHP.ToString();
    }

    private void Update()
    {
        if (slider != null)
        {
            UIHealth(PlayerHP);
        }

    }

    public void UIMaxHealth(int PlayerHP)
    {
        //HP UI
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
        if (!IsInvincible && !_PlayerMovement.IsRolling)
        {
            GameObject HitUI = Instantiate(HitDamageText);
            HitUI.transform.position = HitDamagePoint.position;
            HitUI.GetComponent<HitDamageUI>().EnemyDamage = EnemyDamage;
            //플레이어가 맞았을 때 HP 닳는 양과 죽음처리
            if (PlayerHP > 0)
            {
                // HP 감소
                //EnemyDMG = 적 공격력 추가해야됨
                PlayerHP -= EnemyDamage;
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
        //Debug.Log("무적");
        IsInvincible = true;
    
        Invoke("DisInvisible", 0.5f);
    }
    public void DisInvisible()
    {
        //무적 해제
        //Debug.Log("무적 해제");
        IsInvincible = false;
    }

    private void Die()
    {
        // 플레이어 사망
        Debug.Log("플레이어 사망");
    }

    public void ApplyHealthBuff(int buffAmount)
    {
        PlayerMaxHP += buffAmount;
        PlayerHP = PlayerMaxHP;
        UIMaxHealth(PlayerHP);
    }

    public void RemoveHealthBuff(int buffAmount)
    {
        PlayerMaxHP -= buffAmount;
        PlayerHP = PlayerMaxHP;
        UIMaxHealth(PlayerHP);
    }
}
