//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerAttack : MonoBehaviour
//{
//    private PlayerController _controller;
//    //private Animator animator;

//    public int MinDamage = 10;
//    public int MaxDamage = 20;
//    public int PlayerDMG;
//    public bool IsAttack { get; set; }

//    public float AttackDelay = 1f;

//    private void Update()
//    {
//        PlayerDMG = Random.Range(MinDamage, MaxDamage + 1);
//    }
//    private void Awake()
//    {
//        _controller = GetComponent<PlayerController>();
//        //animator = GetComponent<Animator>();
//    }
//    private void Start()
//    {
//        _controller.OnAttackEvent += AttackCheck;
//    }

//    private void AttackCheck()
//    {
//        if (!IsAttack)
//        {
//            IsAttack = true;
//            Attack();
//            //animator.SetBool("IsAttack", true);
//            Debug.Log("공격체크: " + PlayerDMG);
//        }
//    }
//        private void Attack()
//    {
//        Debug.Log("공격체크2: " + PlayerDMG);
//        Invoke("EndAttack", AttackDelay);
//    }

//    private void EndAttack()
//    {
//        IsAttack = false;
//        //animator.SetBool("IsAttack", false);
//    }

//    private void RealAttack()
//    {
//        // Ray를 생성
//        Ray ray = new Ray(transform.position, transform.forward);
//        RaycastHit HitRange;

//        // Raycast를 통해 어떤 물체와 충돌했는지 확인
//        if (Physics.Raycast(ray, out HitRange))
//        {
//            // 충돌한 물체의 GameObject 가져오기
//            GameObject HitObject = HitRange.collider.gameObject;

//            // 만약 충돌한 물체가 Enemy라면 데미지를 적용
//            EnemyHealth EnemyHealth = HitObject.GetComponent<EnemyHealth>();
//            if (EnemyHealth != null)
//            {
//                // 데미지 적용
//                EnemyHealth.EnemyHit();
//            }
//        }
//    }
//}
