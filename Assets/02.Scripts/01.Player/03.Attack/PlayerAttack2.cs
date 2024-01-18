//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;

//public class PlayerAttack2 : MonoBehaviour
//{
//    private PlayerController _controller;
//    public Transform AttackRangeBox;
//    public Vector3 AttackRangeBoxSize;

//    public int minDamage = 10;
//    public int maxDamage = 20;
//    public int PlayerDMG { get; set; }
//    public bool IsAttacking;

//    public Collider PlayerRangeCollider;
//    //테스트용 적 체력
//    public EnemyHealth EHealth;
//    //애니메이션
//    //private Animator anim;

//    private void Awake()
//    {
//        _controller = GetComponent<PlayerController>();
//        EHealth = GetComponent<EnemyHealth>();
//        //애니메이션
//        //anim = transform.GetChild(0).GetComponent<Animator>();
//    }
//    private void Start()
//    {
//        _controller.OnAttackEvent += AttackCheck;
//    }
//    private void Update()
//    {
//        //공격 범위를 위한 콜라이더
//        Collider[] Playercollider = Physics.OverlapBox(AttackRangeBox.position, AttackRangeBoxSize);
//        //PlayerRangeCollider = SelectPlayerCollider(Collider);
//    }

//    private Collider SelectPlayerCollider(Collider[] Colliders)
//    {
//        foreach (var Range in Colliders)
//        {
//            if (Range.CompareTag("Enemy"))
//            {
//                return Range;
//            }
//        }
//        // 원하는 조건을 만족하는 콜라이더가 없을 경우 null 반환
//        return null;
//    }

//    private void AttackCheck()
//    {
//        if(!IsAttacking)
//        {
//            IsAttacking = true;
//            StartAttack();
//            PlayerDMG = Random.Range(minDamage, maxDamage + 1);
//            Debug.Log("공격 되는중 : " + PlayerDMG);
//        }
//    }

//    private void StartAttack()
//    {
//        if (PlayerRangeCollider != null)
//        {
//            if (PlayerRangeCollider.CompareTag("Enemy"))
//            {
//                EHealth.EnemyHit(PlayerDMG);
//                Debug.Log("공격 되는중2 : " + PlayerDMG);
//            }
//            //else if (PlayerRangeCollider.CompareTag("Resource"))
//            //{
//            //    //리소스에게 데미지 적용
//            //    //ResourceHit(PlayerDMG);
//            //}

//        }
//        Invoke("EndAttack", 0.3f);

//    }
//    //private void AttackRange()
//    //{
//    //    if (PlayerRangeCollider != null)
//    //    {
//    //        if (PlayerRangeCollider.CompareTag("Enemy"))
//    //        {
//    //            EHealth.EnemyHit(PlayerDMG);
//    //            Debug.Log("공격 되는중3 : " + PlayerDMG);
//    //        }
//    //        //else if (PlayerRangeCollider.CompareTag("Resource"))
//    //        //{
//    //        //    //리소스에게 데미지 적용
//    //        //    //ResourceHit(PlayerDMG);
//    //        //}

//    //    }
//    //    Invoke("EndAttack", 0.3f);

//    //}

//    private void OnDrawGizmos()
//    {
//        Gizmos.color = Color.yellow;
//        Gizmos.DrawWireCube(AttackRangeBox.position, AttackRangeBoxSize);
//    }

//    private void EndAttack()
//    {
//        IsAttacking = false;
//        //애니메이션
//        //anim.SetBool("IsAttack", false);
//    }
//}
