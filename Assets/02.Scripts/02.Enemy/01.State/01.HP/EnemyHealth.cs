using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyHealth : MonoBehaviour
{
    public int EnemyHP;
    private PlayerWeapon PAttack;

    private void Awake()
    {
        PAttack = GetComponent<PlayerWeapon>();
        EnemyHP = 100;
    }

    public void EnemyHit(int Damage)
    {
        //���Ͱ� �¾��� �� HP ��� ��� ����ó��
        if (EnemyHP > 0)
        {
            // HP ����
            EnemyHP -= Damage;
            Debug.Log("Enemy HP: " + EnemyHP);
            // HP�� 0 ������ ��� Die
            if (EnemyHP <= 0)
            {
                Die();
                Debug.Log("Enemy HP: " + EnemyHP);
            }
        }
    }
    private void Die()
    {
        // ����
        Debug.Log("���� ���");
    }
}
