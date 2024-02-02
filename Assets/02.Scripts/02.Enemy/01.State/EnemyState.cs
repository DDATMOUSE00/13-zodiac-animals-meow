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

            // 플레이어와의 거리에 따라 상태 전환
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

            // 상태에 따른 행동 수행
            switch (currentState)
            {
                case MonsterState.Idle:
                    // 아무것도 안 함
                    break;
                case MonsterState.Chase:
                    // 플레이어를 향해 이동
                    transform.LookAt(player);
                    transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                    break;
                case MonsterState.Attack:
                    // 플레이어 공격
                    Attack();
                    break;
            }
        }

        void Attack()
        {
            // 공격 로직 구현
            Debug.Log("Monster attacks!");
        }
    }
}
