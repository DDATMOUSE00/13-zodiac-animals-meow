using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //�׽�Ʈ�� �� ������
    public int EnemyDMG = 10;
    //ü��
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

        //T��ư ������ �ǰ� �׽�Ʈ
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
        //�÷��̾ �¾��� �� HP ��� ��� ����ó��
        if (PlayerHP > 0)
        {
            // HP ����
            //EnemyDMG = �� ���ݷ� �߰��ؾߵ�
            PlayerHP -= EnemyDMG;

            // HP�� 0 ������ ��� Die
            if (PlayerHP <= 0)
            {
                Die();
            }
        }
    }
    private void Die()
    {
        // �÷��̾� ���
        Debug.Log("�÷��̾� ���");
    }
}
