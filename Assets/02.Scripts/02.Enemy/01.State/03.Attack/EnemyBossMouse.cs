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

    //지진
    public GameObject Earthquake; //지진
    public Transform Earthquakepoint1; //지진 위치
    public Transform Earthquakepoint2;
    public Transform Earthquakepoint3;
    public Transform Earthquakepoint4;
    public Transform Earthquakepoint5;
    public Transform Earthquakepoint6;
    public float EarthquakeSpeed = 15f; //지진속도

    //public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        _Rigidbody = GetComponent<Rigidbody>();
        currentState = MonsterState.Idle;
        Anim = transform.GetChild(0).GetComponent<Animator>();
        _Health = GetComponent<EnemyHealth>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        //몬스터 추적 사거리
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        //몬스터 공격 사거리
        Collider[] EnemyAttack = Physics.OverlapBox(AttackStart.position, AttackRange / 2f);

        //플레이어 거리에 따라 상태 변경
        if (distanceToPlayer <= SpecialAttackRange && !IsAttack && !IsSpecialAttack1 && !IsSpecialAttack2)
        {
            Invoke("AttackRandom", 2f);
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

        //상태 변경
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
                if (!IsAttack && !_Health.IsDead && !IsSpecialAttack1 && !IsSpecialAttack2)
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
                    Anim.SetBool("IsSpecialAttack", false);
                    Anim.SetBool("IsAttack", false);
                    //spriteRenderer.color = Color.yellow;
                    StartCoroutine(DelayAnimation2());
                    Invoke("SpecialAttack2", AttackTime);
                }
                break;
        }
    }
    private void AttackRandom()
    {
        int RandomAttack = Random.Range(Minpattern, Maxpattern + 1);
        StartCoroutine(RealAttackRandom(RandomAttack));
    }

    private IEnumerator RealAttackRandom(int RandomAttack)
    {
        //Debug.Log(RandomAttack);
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (RandomAttack <= 100 && RandomAttack > 40 && !IsAttack && !IsSpecialAttack2 && !IsSpecialAttack1)
        {
            currentState = MonsterState.Special1;
        }
        else if (RandomAttack <= 40 && RandomAttack > 30 && !IsAttack && !IsSpecialAttack1 && !IsSpecialAttack2)
        {
            currentState = MonsterState.Special2;
        }
        else if (RandomAttack <= 30 && distanceToPlayer <= AttackRange.magnitude / 2f && !IsSpecialAttack1 && !IsSpecialAttack2 && !IsAttack)
        {
            //30%확률 + 일반공격범위에 있으면 Attack
            currentState = MonsterState.Attack;
        }
        yield return new WaitForSeconds(4f); //4초간 대기
    }

    private void Move(Vector3 direction)
    {
        MovementDirection = direction;
        IsMoving = (direction != Vector3.zero);

        Vector3 movement = direction * MoveSpeed * Time.deltaTime;

        //몬스터 이동
        _Rigidbody.MovePosition(transform.position + movement);

        //좌우 반전
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
        //Invoke("EndAttack", AttackDelay);
    }

    //특수공격1
    private IEnumerator RealSpecialAttack1()
    {
        //Debug.Log("특수공격1");
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
        currentState = MonsterState.Idle;
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
        //Invoke("EndAttack", AttackDelay);
    }

    //특수공격2
    private IEnumerator RealSpecialAttack2()
    {
        //Debug.Log("특수공격2");

        Transform[] points = { Earthquakepoint1, Earthquakepoint2, Earthquakepoint3, Earthquakepoint4, Earthquakepoint5, Earthquakepoint6 };

        foreach (Transform point in points)
        {
            GameObject earthquake = Instantiate(Earthquake, point.position, point.rotation);
            Rigidbody rb = earthquake.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * EarthquakeSpeed, ForceMode.Impulse);
        }

        IsSpecialAttack2 = false;
        Anim.SetBool("IsSpecialAttack", false);
        currentState = MonsterState.Idle;
        //spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(1f);
    }

    private void EndAttack()
    {
        //다시 공격 가능한 상태로 만들기
        IsAttack = false;
        IsSpecialAttack1 = false;
        IsSpecialAttack2 = false;
        currentState = MonsterState.Idle;
    }
    IEnumerator DelayAnimation()
    {
        //일반 공격 애니메이션 지연
        yield return YieldInstructionCache.WaitForSeconds(1f);
        Anim.SetBool("IsAttack", true);
    }

    IEnumerator DelayAnimation2()
    {
        //지진 공격 애니메이션 지연
        yield return YieldInstructionCache.WaitForSeconds(3f);
        Anim.SetBool("IsSpecialAttack", true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, ChaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ChaseMaxRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(AttackStart.position, AttackRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, SpecialAttackRange);
    }
}
