using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public enum MonsterState
    {
        Idle,
        Chase,
        Attack
    }

    public MonsterState currentState = MonsterState.Idle;
    public Transform player;
    public Transform AttackStart;
    public Vector3 AttackRange;
    public BoxCollider AttackRangeBox;

    private Vector3 MovementDirection = Vector3.zero;
    private Rigidbody _Rigidbody;

    public bool IsAttack;
    public bool IsMoving;

    public float ChaseRange = 20f;
    public float chaseMaxRange = 20f;
    public float MoveSpeed = 5f;

    public int MinDamage = 5;
    public int MaxDamage = 8;

    //공격시전속도
    public float AttackTime = 2;

    //공격속도
    public float AttackDelay = 1f;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _Rigidbody = GetComponent<Rigidbody>();
        currentState = MonsterState.Idle;
    }

    private void Update()
    {
        //몬스터 추적 사거리
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        //몬스터 공격 사거리
        Collider[] EnemyAttack = Physics.OverlapBox(AttackStart.position, AttackRange / 2f);


        //플레이어와의 거리에 따라 상태 변경
        if (distanceToPlayer <= AttackRange.magnitude / 2f)
        {
            //공격 사거리 안에 들어오면 공격
            currentState = MonsterState.Attack;
        }
        else if (distanceToPlayer <= ChaseRange)
        {
            //플레이어가 추적범위안에 들어오면 Chase상태
            currentState = MonsterState.Chase;
        }
        else if (currentState == MonsterState.Chase && distanceToPlayer > chaseMaxRange)
        {
            currentState = MonsterState.Idle;
        }

        //상태에 따른 행동 수행
        switch (currentState)
        {
            case MonsterState.Idle:
                //아무것도 안 함
                IsAttack = false;
                IsMoving = false;
                break;
            case MonsterState.Chase:
                //플레이어를 향해 이동
                if (!IsAttack)
                {
                    Vector3 directionToPlayer = (player.position - transform.position).normalized;
                    Move(directionToPlayer);
                }
                break;
            case MonsterState.Attack:
                //플레이어 공격
                if (!IsAttack)
                {
                    IsAttack = true;
                    Invoke("Attack", AttackTime);
                }
                break;
        }
    }

    private void Move(Vector3 direction)
    {
        MovementDirection = direction;
        IsMoving = (direction != Vector3.zero);

        // 이동 방향 벡터에 이동 속도를 곱하여 실제 이동량을 계산
        Vector3 movement = direction * MoveSpeed * Time.deltaTime;

        // 몬스터를 이동량만큼 이동시킴
        _Rigidbody.MovePosition(transform.position + movement);

        // 이동 방향에 따라 몬스터의 좌우 방향을 조정
        if (direction.x < 0 && !IsAttack)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction.x > 0 && !IsAttack)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }


    private void Attack()
    {
        //공격
        if (IsAttack)
        {
            //몬스터 공격 사거리
            Collider[] EnemyAttack = Physics.OverlapBox(AttackStart.position, AttackRange / 2f);
            foreach (Collider collider in EnemyAttack)
            {
                if (collider.CompareTag("Player"))
                {
                    //데미지 계산
                    PlayerHealth PlayerHealth = collider.GetComponent<PlayerHealth>();
                    if (PlayerHealth != null)
                    {
                        int EnemyDamage = Random.Range(MinDamage, MaxDamage + 1);
                        PlayerHealth.PlayerHit(EnemyDamage);
                    }
                }
            }
        }
        Invoke("EndAttack", AttackDelay);
    }

    private void EndAttack()
    {
        //다시 공격 가능한 상태로 만들기
        IsAttack = false;
    }

    private void OnDrawGizmosSelected()
    {
        //chaseRange를 그립니다.
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, ChaseRange);

        //attackRange를 그립니다.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseMaxRange);


        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(AttackStart.position, AttackRange);
    }
}