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

    //ü��UI
    //public TextMeshProUGUI MinHPTEXT;
    //public TextMeshProUGUI MaxHPTEXT;

    //�ǰ� ������UI
    public GameObject HitDamageText;
    public Transform HitDamagePoint;

    //ü��
    public int PlayerHP { get; set; }

    public int PlayerMaxHP { get; set; }

    public Slider slider;

    //����
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
            //�÷��̾ �¾��� �� HP ��� ��� ����ó��
            if (PlayerHP > 0)
            {
                // HP ����
                //EnemyDMG = �� ���ݷ� �߰��ؾߵ�
                PlayerHP -= EnemyDamage;
                if (!Anim.AnimationState.GetCurrent(0).Animation.Name.Equals("sd_damage"))
                {
                    Anim.AnimationState.SetAnimation(0, "sd_damage", false);
                    //����
                    Invisible();
                }
                // HP�� 0 ������ ��� Die
                if (PlayerHP <= 0)
                {
                    Die();
                }
            }
        }
    }

    public void Invisible()
    {
        //����
        //Debug.Log("����");
        IsInvincible = true;
    
        Invoke("DisInvisible", 0.5f);
    }
    public void DisInvisible()
    {
        //���� ����
        //Debug.Log("���� ����");
        IsInvincible = false;
    }

    private void Die()
    {
        // �÷��̾� ���
        Debug.Log("�÷��̾� ���");
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
