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


    //공격범위
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
            Debug.Log("공격체크: " + PlayerDMG);
        }
    }
    private void Attack()
    {
        Debug.Log("공격체크2: " + PlayerDMG);
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

        //디버그
        Vector3 rayEnd = rayOrigin + rayDirection * AttackDistance;
        Debug.DrawLine(rayOrigin, rayEnd, Color.red, 2.0f);

        RaycastHit HitRange;
        Debug.Log("리얼어택 : " + PlayerDMG);

        // Raycast를 통해 어떤 물체와 충돌했는지 확인
        if (Physics.Raycast(IsAttackRange, out HitRange, AttackDistance))
        {
            // 충돌한 물체의 GameObject 가져오기
            GameObject HitObject = HitRange.collider.gameObject;

            // 만약 충돌한 물체가 Enemy라면 데미지를 적용
            if (HitObject.CompareTag("Enemy"))
            {
                EnemyHealth EnemyHealth = HitObject.GetComponent<EnemyHealth>();
                if (EnemyHealth != null)
                {
                    // 데미지 적용
                    EnemyHealth.EnemyHit(PlayerDMG);
                }
            }
        }
    }
}