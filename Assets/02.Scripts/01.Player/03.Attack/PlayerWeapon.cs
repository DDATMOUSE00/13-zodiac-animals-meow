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

//    //���ݹ���
//    public Vector3 AttackRange;

//    //���� ���� ��ȯ�� ����
//    private Vector2 Aim = Vector2.zero;

//    private void Awake()
//    {
//        //Anim = GetComponent<SkeletonAnimation>();
//    }

//    private void Look(Vector3 PlayerAim)
//    {
//        //���� ����
//        Aim = PlayerAim;
//    }

//    public void AttackCheck()
//    {
//        // ���� �������� üũ
//        if (!IsAttack)
//        {
//            IsAttack = true;
//            Attack();
//            //animator.SetBool("IsAttack", true);
//        }
//    }

//    private void Attack()
//    {
//        //���� �����ϸ� ����
//        RealAttack();
//    }

//    private void RealAttack()
//    {
//        //���� ã��
//        Collider[] HitColliders = Physics.OverlapBox(AttackStart.position, AttackRange / 2f);
//        foreach (Collider collider in HitColliders)
//        {
//            if (collider.CompareTag("Enemy"))
//            {
//                //������ ���
//                EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
//                if (enemyHealth != null)
//                {
//                    int playerDamage = Random.Range(MinDamage, MaxDamage + 1);
//                    enemyHealth.EnemyHit(playerDamage);
//                }
//            }
//        }
//        Invoke("EndAttack", AttackDelay);

//        //���� ������ȯ �������
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
//        //�ٽ� ���� ������ ���·� �����
//        IsAttack = false;
//    }

//    private void OnDrawGizmosSelected()
//    {
//        //ũ��� ��ġ ǥ��
//        Gizmos.color = Color.green;
//        Gizmos.DrawWireCube(AttackStart.position, AttackRange);
//    }
//}