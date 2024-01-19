using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerHealth : MonoBehaviour
{
    //�׽�Ʈ�� �� ������
    public int EnemyDMG = 1;
    //ü��
    public int PlayerHP = 10;


    private void Update()
    {
        //T��ư ������ �ǰ� �׽�Ʈ
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayerHit();
            Debug.Log("Player HP: " + PlayerHP);
        }
    }
    private void Awake()
    {
        ;
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
