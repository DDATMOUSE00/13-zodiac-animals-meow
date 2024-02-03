using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyHealth : MonoBehaviour
{
    public int EnemyMaxHP;
    public int EnemyHP;
    private PlayerAttack PAttack;
    public Animator Anim;
    public bool IsDead;

    private void Awake()
    {
        PAttack = GetComponent<PlayerAttack>();
        Anim = transform.GetChild(0).GetComponent<Animator>();
    }
    private void Start()
    {
        EnemyHP = EnemyMaxHP;
        IsDead = false;
    }
    public void EnemyHit(int PlayerDamage)
    {
        //몬스터가 맞았을 때 HP 닳는 양과 죽음처리
        if (EnemyHP > 0)
        {
            // HP 감소
            EnemyHP -= PlayerDamage;
            //Debug.Log("몬스터 최대체력 : " + EnemyMaxHP);
            Debug.Log("몬스터 현재체력 : " + EnemyHP);
            // HP가 0 이하일 경우 Die
            if (EnemyHP <= 0)
            {
                Die();
                //Debug.Log("몬스터 최대체력 : " + EnemyMaxHP);
                Debug.Log("몬스터 현재체력 : " + EnemyHP);
            }
        }
    }
    private void Die()
    {
        // 몬스터
        Anim.SetBool("IsDead", true);
        IsDead = true;
        Debug.Log("몬스터 사망");
        Invoke("RealDie", 2f);
    }
    private void RealDie()
    {
        Destroy(gameObject);
    }
}
