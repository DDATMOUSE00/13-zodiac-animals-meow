using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossMouse : MonoBehaviour
{
    public enum MonsterState
    {
        Idle,
        Chase,
        Attack,
        Special1,
        Special2,
    }

    public MonsterState currentState = MonsterState.Idle;
    public Transform player;
    public Transform AttackStart;
    public Vector3 AttackRange;
    public Animator Anim;
    public EnemyHealth _Health;

    private Vector3 MovementDirection = Vector3.zero;
    private Rigidbody _Rigidbody;

    public bool IsAttack;
    public bool IsMoving;
    public bool IsSpecialAttack1;
    public bool IsSpecialAttack2;

    public float ChaseRange = 10f;
    public float ChaseMaxRange = 20f;
    public float MoveSpeed = 5f;
    public float SpecialAttackRange = 10f;

    //데미지
    public int MinDamage = 5;
    public int MaxDamage = 8;

    //공격선딜레이
    public float AttackTime = 2;

    //재공격 시간
    public float AttackDelay = 1f;

    //패턴숫자
    public int Minpattern = 0;
    public int Maxpattern = 100;

    public GameObject Bullet; //투사체
    public Transform BulletPoint; //투사체 발사 위치
    public Transform BulletPoint2; //투사체 발사 위치
    public float BulletSpeed = 10f; //투사체 속도

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _Rigidbody = GetComponent<Rigidbody>();
        currentState = MonsterState.Idle;
        Anim = transform.GetChild(0).GetComponent<Animator>();
        _Health = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        //몬스터 추적 사거리
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        //몬스터 공격 사거리
        Collider[] EnemyAttack = Physics.OverlapBox(AttackStart.position, AttackRange / 2f);


        //플레이어와의 거리에 따라 상태 변경
        if (distanceToPlayer <= SpecialAttackRange)
        {
            int RandomAttack = Random.Range(Minpattern, Maxpattern + 1);

            if (RandomAttack <= 100 && RandomAttack > 60 && !IsAttack &&!IsSpecialAttack2 &&!IsSpecialAttack1)
            {
                currentState = MonsterState.Special1;
            }
            else if (RandomAttack <= 60 && RandomAttack > 30 && !IsAttack && !IsSpecialAttack1 && !IsSpecialAttack2)
            {
                currentState = MonsterState.Special2;
            }
            else if (RandomAttack <= 30 && distanceToPlayer <= AttackRange.magnitude / 2f && !IsSpecialAttack1 && !IsSpecialAttack2 && !IsAttack)
            {
                //30%확률 + 일반공격범위에 있으면 Attack
                currentState = MonsterState.Attack;
            }
        }
        else if (distanceToPlayer <= ChaseRange && !IsAttack && !IsSpecialAttack1 && !IsSpecialAttack2)
        {
            //플레이어가 추적범위안에 들어오면 Chase상태
            currentState = MonsterState.Chase;
        }
        else if ((currentState == MonsterState.Chase || currentState == MonsterState.Attack || currentState == MonsterState.Special1 || currentState == MonsterState.Special2) && distanceToPlayer > ChaseMaxRange)
        {
            currentState = MonsterState.Idle;
        }

        //상태에 따른 행동 수행
        switch (currentState)
        {
            case MonsterState.Idle:
                //아무것도 안 함
                Anim.SetBool("IsMove", false);
                Anim.SetBool("IsAttack", false);
                IsAttack = false;
                IsMoving = false;
                break;
            case MonsterState.Chase:
                //플레이어를 향해 이동
                if (!IsAttack && !_Health.IsDead)
                {
                    Anim.SetBool("IsMove", true);
                    Vector3 directionToPlayer = (player.position - transform.position).normalized;
                    Move(directionToPlayer);
                }
                break;
            case MonsterState.Attack:
                //플레이어 공격
                if (!IsAttack && !_Health.IsDead)
                {
                    IsSpecialAttack1 = false;
                    IsSpecialAttack2 = false;
                    IsAttack = true;
                    IsMoving = false;
                    Anim.SetBool("IsMove", false);
                    Invoke("Attack", AttackTime);
                    StartCoroutine(DelayAnimation());
                }
                break;
            case MonsterState.Special1:
                if (!IsAttack && !_Health.IsDead && !IsSpecialAttack2 && !IsSpecialAttack1)
                {
                    IsSpecialAttack1 = true;
                    IsSpecialAttack2 = false;
                    IsAttack = false;
                    IsMoving = false;
                    Anim.SetBool("IsMove", false);
                    Anim.SetBool("IsSpecialAttack", true);
                    Invoke("SpecialAttack1", AttackTime);
                }
                break;
            case MonsterState.Special2:
                if (!IsAttack && !_Health.IsDead && !IsSpecialAttack1 && !IsSpecialAttack2)
                {
                    IsSpecialAttack2 = true;
                    IsSpecialAttack1 = false;
                    IsAttack = false;
                    IsMoving = false;
                    Anim.SetBool("IsMove", false);
                    Anim.SetBool("IsSpecialAttack", true);
                    Invoke("SpecialAttack2", AttackTime);
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
            transform.localScale = new Vector3(3, 3, 1);
        }
        else if (direction.x > 0 && !IsAttack)
        {
            transform.localScale = new Vector3(-3, 3, 1);
        }
    }

    private void Attack()
    {
        //공격
        if (IsAttack && !_Health.IsDead && !IsSpecialAttack1 && !IsSpecialAttack2)
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
            Anim.SetBool("IsAttack", false);
        }
        Invoke("EndAttack", AttackDelay);
    }

    private void SpecialAttack1()
    {
        Vector3 direction = player.position - transform.position;
        if (direction.x < 0)
        {
            transform.localScale = new Vector3(3, 3, 1);
        }
        else if (direction.x > 0)
        {
            transform.localScale = new Vector3(-3, 3, 1);
        }

        if (!_Health.IsDead && !IsAttack && IsSpecialAttack1 && !IsSpecialAttack2)
        {
            StartCoroutine(RealSpecialAttack1());
        }
        Invoke("EndAttack", AttackDelay);
    }

    //특수공격1
    private IEnumerator RealSpecialAttack1()
    {
        Debug.Log("특수공격1");
        GameObject bullet = Instantiate(Bullet, BulletPoint.position, BulletPoint.rotation);
        GameObject bullet2 = Instantiate(Bullet, BulletPoint2.position, BulletPoint2.rotation);
        Vector3 Playerdirection = player.position - BulletPoint.position;
        Vector3 Playerdirection2 = player.position - BulletPoint2.position;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        Rigidbody rb2 = bullet2.GetComponent<Rigidbody>();
        rb.AddForce(Playerdirection.normalized * BulletSpeed, ForceMode.Impulse);
        rb2.AddForce(Playerdirection2.normalized * BulletSpeed, ForceMode.Impulse);
        IsSpecialAttack1 = false;
        Anim.SetBool("IsSpecialAttack", false);
        yield return new WaitForSeconds(1f); //1초간 대기
    }
    private void SpecialAttack2()
    {
        Vector3 direction = player.position - transform.position;
        if (direction.x < 0)
        {
            transform.localScale = new Vector3(3, 3, 1);
        }
        else if (direction.x > 0)
        {
            transform.localScale = new Vector3(-3, 3, 1);
        }

        if (!_Health.IsDead && !IsAttack && IsSpecialAttack2 && !IsSpecialAttack1)
        {
            StartCoroutine(RealSpecialAttack2());
        }
        Invoke("EndAttack", AttackDelay);
    }

    //특수공격2
    private IEnumerator RealSpecialAttack2()
    {
        Debug.Log("특수공격2");
        IsSpecialAttack2 = false;
        Anim.SetBool("IsSpecialAttack", false);
        yield return new WaitForSeconds(1f); //1초간 대기
    }

    private void EndAttack()
    {
        //다시 공격 가능한 상태로 만들기
        IsAttack = false;
        IsSpecialAttack1 = false;
        IsSpecialAttack2 = false;
    }
    IEnumerator DelayAnimation()
    {
        //일반 공격 애니메이션 지연
        yield return YieldInstructionCache.WaitForSeconds(1f);
        Anim.SetBool("IsAttack", true);
    }

    //IEnumerator DelayAnimation2()
    //{
    //    //특수 공격 애니메이션 지연
    //    yield return YieldInstructionCache.WaitForSeconds(1f);
    //    Anim.SetBool("IsSpecialAttack", true);
    //}

    private void OnDrawGizmosSelected()
    {
        //chaseRange
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, ChaseRange);

        //attackRange
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ChaseMaxRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(AttackStart.position, AttackRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, SpecialAttackRange);
    }
}
