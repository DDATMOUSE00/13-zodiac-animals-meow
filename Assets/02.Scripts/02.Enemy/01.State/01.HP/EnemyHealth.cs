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
        //몬스터가 맞았을 때 HP 닳는 양과 죽음처리
        if (EnemyHP > 0)
        {
            // HP 감소
            EnemyHP -= Damage;
            Debug.Log("Enemy HP: " + EnemyHP);
            // HP가 0 이하일 경우 Die
            if (EnemyHP <= 0)
            {
                Die();
                Debug.Log("Enemy HP: " + EnemyHP);
            }
        }
    }
    private void Die()
    {
        // 몬스터
        Debug.Log("몬스터 사망");
    }
}
