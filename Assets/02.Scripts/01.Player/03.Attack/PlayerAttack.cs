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
//            Debug.Log("����üũ: " + PlayerDMG);
//        }
//    }
//        private void Attack()
//    {
//        Debug.Log("����üũ2: " + PlayerDMG);
//        Invoke("EndAttack", AttackDelay);
//    }

//    private void EndAttack()
//    {
//        IsAttack = false;
//        //animator.SetBool("IsAttack", false);
//    }

//    private void RealAttack()
//    {
//        // Ray�� ����
//        Ray ray = new Ray(transform.position, transform.forward);
//        RaycastHit HitRange;

//        // Raycast�� ���� � ��ü�� �浹�ߴ��� Ȯ��
//        if (Physics.Raycast(ray, out HitRange))
//        {
//            // �浹�� ��ü�� GameObject ��������
//            GameObject HitObject = HitRange.collider.gameObject;

//            // ���� �浹�� ��ü�� Enemy��� �������� ����
//            EnemyHealth EnemyHealth = HitObject.GetComponent<EnemyHealth>();
//            if (EnemyHealth != null)
//            {
//                // ������ ����
//                EnemyHealth.EnemyHit();
//            }
//        }
//    }
//}
