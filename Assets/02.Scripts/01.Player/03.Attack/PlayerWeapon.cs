using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerWeapon : MonoBehaviour
{
    public bool IsAttack { get; set; }

    public int MinDamage = 10;
    public int MaxDamage = 20;

    public float AttackDelay = 1f;

    private Animator animator;

    //���ݹ���
    public Vector3 AttackRange;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void AttackCheck()
    {
        // ���� �������� üũ
        if (!IsAttack)
        {
            IsAttack = true;
            Attack();
            animator.SetBool("IsAttack", true);
        }
    }

    private void Attack()
    {
        //���� �����ϸ� ����
        RealAttack();
    }

    private void RealAttack()
    {
        //���� ã��
        Collider[] HitColliders = Physics.OverlapBox(transform.position, AttackRange / 2f);
        foreach (Collider collider in HitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                //������ ���
                EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    int playerDamage = Random.Range(MinDamage, MaxDamage + 1);
                    enemyHealth.EnemyHit(playerDamage);
                }
            }
        }
        Invoke("EndAttack", AttackDelay);
    }

    private void EndAttack()
    {
        //�ٽ� ���� ������ ���·� �����
        IsAttack = false;
        animator.SetBool("IsAttack", false);
    }

    private void OnDrawGizmosSelected()
    {
        //ũ��� ��ġ ǥ��
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, AttackRange);
    }
}