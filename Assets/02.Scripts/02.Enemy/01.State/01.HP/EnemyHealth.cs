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
        //���Ͱ� �¾��� �� HP ��� ��� ����ó��
        if (EnemyHP > 0)
        {
            // HP ����
            EnemyHP -= PlayerDamage;
            //Debug.Log("���� �ִ�ü�� : " + EnemyMaxHP);
            //Debug.Log("���� ����ü�� : " + EnemyHP);
            // HP�� 0 ������ ��� Die
            if (EnemyHP <= 0)
            {
                IsDead = true;
                Anim.SetBool("IsDead", true);
                Invoke("Die", 2f);
                //Debug.Log("���� �ִ�ü�� : " + EnemyMaxHP);
                //Debug.Log("���� ����ü�� : " + EnemyHP);
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
