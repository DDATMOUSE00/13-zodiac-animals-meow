using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int PlayerHP = 10;

    //�׽�Ʈ�� �� ������
    public int EnemyDMG = 1;


    public void Playerhit()
    {
        //�÷��̾ �¾��� �� HP ��� ��� ����ó��
        if (PlayerHP > 0)
        {
            // HP ����
            //EnemyDMG = �� ���ݷ� �߰��ؾߵ�
            PlayerHP -= EnemyDMG;

            // HP�� 0 ������ ��� Die ���
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
