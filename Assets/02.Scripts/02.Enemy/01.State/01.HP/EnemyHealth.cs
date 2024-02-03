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
        //���Ͱ� �¾��� �� HP ��� ��� ����ó��
        if (EnemyHP > 0)
        {
            // HP ����
            EnemyHP -= PlayerDamage;
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
        Anim.SetBool("IsDead", true);
        IsDead = true;
        Debug.Log("���� ���");
        Invoke("RealDie", 2f);
    }
    private void RealDie()
    {
        Destroy(gameObject);
    }
}
