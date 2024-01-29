using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyHealth : MonoBehaviour
{
    public int EnemyMaxHP;
    public int EnemyHP;
    private PlayerWeapon PAttack;

    private void Awake()
    {
        PAttack = GetComponent<PlayerWeapon>();
        EnemyMaxHP = 100;
    }
    private void Start()
    {
        EnemyHP = EnemyMaxHP;
    }
    public void EnemyHit(int Damage)
    {
        //���Ͱ� �¾��� �� HP ��� ��� ����ó��
        if (EnemyHP > 0)
        {
            // HP ����
            EnemyHP -= Damage;
            //Debug.Log("���� �ִ�ü�� : " + EnemyMaxHP);
            Debug.Log("���� ����ü�� : " + EnemyHP);
            // HP�� 0 ������ ��� Die
            if (EnemyHP <= 0)
            {
                Die();
                //Debug.Log("���� �ִ�ü�� : " + EnemyMaxHP);
                Debug.Log("���� ����ü�� : " + EnemyHP);
            }
        }
    }
    private void Die()
    {
        // ����
        Destroy(gameObject);
        Debug.Log("���� ���");
    }
}
