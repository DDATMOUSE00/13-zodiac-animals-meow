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
    private DropItem dropItem;

    private void Awake()
    {
        PAttack = GetComponent<PlayerAttack>();
        Anim = transform.GetChild(0).GetComponent<Animator>();
        dropItem = GetComponent<DropItem>();
    }
    private void Start()
    {
        EnemyHP = EnemyMaxHP;
        IsDead = false;
    }

    private void OnEnable()
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
            //Debug.Log("몬스터 현재체력 : " + EnemyHP);
            // HP가 0 이하일 경우 Die
            if (EnemyHP <= 0)
            {
                IsDead = true;
                Anim.SetBool("IsDead", true);
                Invoke("Die", 2f);
                //Debug.Log("몬스터 최대체력 : " + EnemyMaxHP);
                //Debug.Log("몬스터 현재체력 : " + EnemyHP);
            }
        }
    }
    private void Die()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
        dropItem.AllDropItems();
    }
}
