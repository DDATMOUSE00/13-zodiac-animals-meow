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
    private PlayerAttack _PlayerAttack;

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
    public bool IsHit;

    //죽음
    public bool IsDead;

    private void Awake()
    {
        PlayerMaxHP = 100;
        Anim = GetComponentInChildren<SkeletonAnimation>();
        _PlayerMovement = GetComponent<PlayerMovement>();
        _PlayerAttack = GetComponent<PlayerAttack>();
    }

    private void Start()
    {
        PlayerHP = PlayerMaxHP;
        UIMaxHealth(PlayerMaxHP);
        IsHit = false;
        IsDead = false;
        //MinHPTEXT.text = PlayerHP.ToString();
        //MaxHPTEXT.text = PlayerMaxHP.ToString();
    }

    private void Update()
    {
        if (slider != null)
        {
            UIHealth(PlayerHP);
        }

        // HP가 0 이하일 경우 Die
        if (PlayerHP <= 0)
        {
            IsDead = true;
            Die();
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
        if (!IsHit && !_PlayerMovement.IsRolling)
        {
            GameObject HitUI = Instantiate(HitDamageText);
            HitUI.transform.position = HitDamagePoint.position;
            HitUI.GetComponent<HitDamageUI>().EnemyDamage = EnemyDamage;
            //플레이어가 맞았을 때 HP 닳는 양과 죽음처리
            if (PlayerHP > 0)
            {
                // HP 감소
                PlayerHP -= EnemyDamage;

                //무적
                Invisible();
            }
        }
    }

    public void Invisible()
    {
        //무적
        //Debug.Log("무적");
        IsHit = true;
        _PlayerAttack.ComboCount = 0;
        Invoke("DisInvisible", 0.3f);
    }
    public void DisInvisible()
    {
        //무적 해제
        //Debug.Log("무적 해제");
        IsHit = false;
    }

    private void Die()
    {
        // 플레이어 사망
        Debug.Log("플레이어 사망");
    }
}
