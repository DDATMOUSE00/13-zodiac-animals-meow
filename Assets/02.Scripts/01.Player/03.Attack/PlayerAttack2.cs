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
//    //�׽�Ʈ�� �� ü��
//    public EnemyHealth EHealth;
//    //�ִϸ��̼�
//    //private Animator anim;

//    private void Awake()
//    {
//        _controller = GetComponent<PlayerController>();
//        EHealth = GetComponent<EnemyHealth>();
//        //�ִϸ��̼�
//        //anim = transform.GetChild(0).GetComponent<Animator>();
//    }
//    private void Start()
//    {
//        _controller.OnAttackEvent += AttackCheck;
//    }
//    private void Update()
//    {
//        //���� ������ ���� �ݶ��̴�
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
//        // ���ϴ� ������ �����ϴ� �ݶ��̴��� ���� ��� null ��ȯ
//        return null;
//    }

//    private void AttackCheck()
//    {
//        if(!IsAttacking)
//        {
//            IsAttacking = true;
//            StartAttack();
//            PlayerDMG = Random.Range(minDamage, maxDamage + 1);
//            Debug.Log("���� �Ǵ��� : " + PlayerDMG);
//        }
//    }

//    private void StartAttack()
//    {
//        if (PlayerRangeCollider != null)
//        {
//            if (PlayerRangeCollider.CompareTag("Enemy"))
//            {
//                EHealth.EnemyHit(PlayerDMG);
//                Debug.Log("���� �Ǵ���2 : " + PlayerDMG);
//            }
//            //else if (PlayerRangeCollider.CompareTag("Resource"))
//            //{
//            //    //���ҽ����� ������ ����
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
//    //            Debug.Log("���� �Ǵ���3 : " + PlayerDMG);
//    //        }
//    //        //else if (PlayerRangeCollider.CompareTag("Resource"))
//    //        //{
//    //        //    //���ҽ����� ������ ����
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
//        //�ִϸ��̼�
//        //anim.SetBool("IsAttack", false);
//    }
//}
