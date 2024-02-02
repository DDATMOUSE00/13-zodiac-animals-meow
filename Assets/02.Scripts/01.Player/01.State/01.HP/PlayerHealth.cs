using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private SkeletonAnimation Anim;


    //�׽�Ʈ�� �� ������
    public int EnemyDMG = 10;
    //ü��
    public int PlayerMaxHP;
    public int PlayerHP;

    public Slider slider;

    //����
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

        //T��ư ������ �ǰ� �׽�Ʈ
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
            //�÷��̾ �¾��� �� HP ��� ��� ����ó��
            if (PlayerHP > 0)
            {
                // HP ����
                //EnemyDMG = �� ���ݷ� �߰��ؾߵ�
                PlayerHP -= EnemyDMG;
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
        Debug.Log("����");
        IsInvincible = true;
    
        Invoke("DisInvisible", 0.5f);
    }
    public void DisInvisible()
    {
        //���� ����
        Debug.Log("���� ����");
        IsInvincible = false;
    }

    private void Die()
    {
        // �÷��̾� ���
        Debug.Log("�÷��̾� ���");
    }
}
