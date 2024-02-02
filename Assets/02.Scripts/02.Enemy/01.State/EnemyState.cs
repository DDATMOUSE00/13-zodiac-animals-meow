using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public enum MonsterState
    {
        Idle,
        Chase,
        Attack
    }

    public class MonsterAI : MonoBehaviour
    {
        public float chaseRange = 10f;
        public float attackRange = 2f;
        public float moveSpeed = 5f;

        private Transform player;
        private MonsterState currentState = MonsterState.Idle;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        void Update()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // �÷��̾���� �Ÿ��� ���� ���� ��ȯ
            if (distanceToPlayer <= attackRange)
            {
                currentState = MonsterState.Attack;
            }
            else if (distanceToPlayer <= chaseRange)
            {
                currentState = MonsterState.Chase;
            }
            else
            {
                currentState = MonsterState.Idle;
            }

            // ���¿� ���� �ൿ ����
            switch (currentState)
            {
                case MonsterState.Idle:
                    // �ƹ��͵� �� ��
                    break;
                case MonsterState.Chase:
                    // �÷��̾ ���� �̵�
                    transform.LookAt(player);
                    transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                    break;
                case MonsterState.Attack:
                    // �÷��̾� ����
                    Attack();
                    break;
            }
        }

        void Attack()
        {
            // ���� ���� ����
            Debug.Log("Monster attacks!");
        }
    }
}
