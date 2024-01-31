//using Spine.Unity;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UIElements;

//public class PlayerWeapon : MonoBehaviour
//{
//    //public SkeletonAnimation Anim;
//    public Transform AttackStart;

//    public bool IsAttack { get; set; }

//    public int MinDamage = 10;
//    public int MaxDamage = 20;

//    public float AttackDelay = 1f;

//    //공격범위
//    public Vector3 AttackRange;

//    //공격 방향 전환을 위해
//    private Vector2 Aim = Vector2.zero;

//    private void Awake()
//    {
//        //Anim = GetComponent<SkeletonAnimation>();
//    }

//    private void Look(Vector3 PlayerAim)
//    {
//        //공격 방향
//        Aim = PlayerAim;
//    }

//    public void AttackCheck()
//    {
//        // 공격 가능한지 체크
//        if (!IsAttack)
//        {
//            IsAttack = true;
//            Attack();
//            //animator.SetBool("IsAttack", true);
//        }
//    }

//    private void Attack()
//    {
//        //공격 가능하면 공격
//        RealAttack();
//    }

//    private void RealAttack()
//    {
//        //범위 찾기
//        Collider[] HitColliders = Physics.OverlapBox(AttackStart.position, AttackRange / 2f);
//        foreach (Collider collider in HitColliders)
//        {
//            if (collider.CompareTag("Enemy"))
//            {
//                //데미지 계산
//                EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
//                if (enemyHealth != null)
//                {
//                    int playerDamage = Random.Range(MinDamage, MaxDamage + 1);
//                    enemyHealth.EnemyHit(playerDamage);
//                }
//            }
//        }
//        Invoke("EndAttack", AttackDelay);

//        //공격 방향전환 만드는중
//        //if (direction.x < 0)
//        //{
//        //    transform.localScale = new Vector3(1, 1, 1);
//        //}
//        //else if (direction.x > 0)
//        //{
//        //    transform.localScale = new Vector3(-1, 1, 1);
//        //}
//    }

//    private void EndAttack()
//    {
//        //다시 공격 가능한 상태로 만들기
//        IsAttack = false;
//    }

//    private void OnDrawGizmosSelected()
//    {
//        //크기와 위치 표시
//        Gizmos.color = Color.green;
//        Gizmos.DrawWireCube(AttackStart.position, AttackRange);
//    }
//}