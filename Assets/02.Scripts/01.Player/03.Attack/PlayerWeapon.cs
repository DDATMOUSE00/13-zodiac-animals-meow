using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerWeapon : MonoBehaviour 
{
    public bool IsAttack { get; set; }
    public float AttackDistance;

    public int MinDamage = 10;
    public int MaxDamage = 20;
    public int PlayerDMG { get; set; }

    public float AttackDelay = 1f;

    private Animator animator;


    //���ݹ���
    public GameObject AttackRange;

    //[Header("Resource")]
    //public bool DoResources;

    //[Header("Enemy")]
    //public bool DoDamage;
    //public int damage;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        ;
    }

    public void AttackCheck()
    {
        if (!IsAttack)
        {
            PlayerDMG = Random.Range(MinDamage, MaxDamage + 1);
            IsAttack = true;
            Attack();
            animator.SetBool("IsAttack", true);
            Debug.Log("����üũ: " + PlayerDMG);
        }
    }
    private void Attack()
    {
        Debug.Log("����üũ2: " + PlayerDMG);
        Invoke("EndAttack", AttackDelay);
    }

    private void EndAttack()
    {
        IsAttack = false;
        animator.SetBool("IsAttack", false);
    }

    public void RealAttack()
    {
        Vector3 rayOrigin = AttackRange.transform.position;
        Vector3 rayDirection = transform.forward;

        Ray IsAttackRange = new Ray(rayOrigin, rayDirection);

        //�����
        Vector3 rayEnd = rayOrigin + rayDirection * AttackDistance;
        Debug.DrawLine(rayOrigin, rayEnd, Color.red, 2.0f);

        RaycastHit HitRange;
        Debug.Log("������� : " + PlayerDMG);

        // Raycast�� ���� � ��ü�� �浹�ߴ��� Ȯ��
        if (Physics.Raycast(IsAttackRange, out HitRange, AttackDistance))
        {
            // �浹�� ��ü�� GameObject ��������
            GameObject HitObject = HitRange.collider.gameObject;

            // ���� �浹�� ��ü�� Enemy��� �������� ����
            if (HitObject.CompareTag("Enemy"))
            {
                EnemyHealth EnemyHealth = HitObject.GetComponent<EnemyHealth>();
                if (EnemyHealth != null)
                {
                    // ������ ����
                    EnemyHealth.EnemyHit(PlayerDMG);
                }
            }
        }
    }
}