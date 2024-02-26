using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyLongAttack : MonoBehaviour
{
    public enum MonsterState
    {
        Idle,
        Chase,
        Attack
    }

    public MonsterState currentState = MonsterState.Idle;
    public Transform Player;
    public Transform AttackStart;
    //public Vector3 AttackRange;
    public Vector3 AttackRangeSize;
    //public BoxCollider AttackRangeBox;
    public Animator Anim;
    public EnemyHealth _Health;

    private Vector3 MovementDirection = Vector3.zero;
    private Rigidbody _Rigidbody;

    public bool IsAttack;
    public bool IsMoving;

    public float ChaseRange = 10f;
    public float ChaseMaxRange = 20f;
    public float MoveSpeed = 5f;

    //투사체
    public GameObject Bullet; //투사체
    public Transform BulletPoint; //투사체 발사 위치
    public float BulletSpeed = 10f; //투사체 속도

    //데미지
    public int MinDamage = 5;
    public int MaxDamage = 8;

    //공격선딜레이
    public float AttackTime = 2;

    //재공격 시간
    public float AttackDelay = 1f;


    private void Awake()
    {
        //Player = GameObject.FindGameObjectWithTag("Player").transform;
        _Rigidbody = GetComponent<Rigidbody>();
        currentState = MonsterState.Idle;
        Anim = transform.GetChild(0).GetComponent<Animator>();
        _Health = GetComponent<EnemyHealth>();
        //GameObject PlayerGameObject = GameObject.FindWithTag("Player");
        //Transform PlayerTransform = PlayerGameObject.transform;
    }
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        GameObject PlayerGameObject = GameObject.FindWithTag("Player");
        Transform PlayerTransform = PlayerGameObject.transform;

    }
    private void Update()
    {
        //몬스터 추적 사거리
        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);

        //몬스터 공격 사거리
        Collider[] EnemyAttack = Physics.OverlapBox(AttackStart.position, AttackRangeSize / 2f);


        //플레이어 거리에 따라 상태 변경
        if (distanceToPlayer <= AttackRangeSize.magnitude / 2f)
        {
            //공격 사거리 안에 들어오면 공격
            currentState = MonsterState.Attack;
        }
        else if (distanceToPlayer <= ChaseRange)
        {
            //플레이어가 추적범위안에 들어오면 Chase상태
            currentState = MonsterState.Chase;
        }
        else if ((currentState == MonsterState.Chase || currentState == MonsterState.Attack) && distanceToPlayer > ChaseMaxRange)
        {
            currentState = MonsterState.Idle;
        }

        //상태변경
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
                    Vector3 directionToPlayer = (Player.position - transform.position).normalized;
                    Move(directionToPlayer);
                }
                break;
            case MonsterState.Attack:
                //플레이어 공격
                if (!IsAttack && !_Health.IsDead)
                {
                    IsAttack = true;
                    Anim.SetBool("IsAttack", true);
                    Invoke("Attack", AttackTime);
                    IsMoving = false;
                    Anim.SetBool("IsMove", false);
                    //StartCoroutine(DelayAnimation());
                }
                break;
        }
    }

    private void Move(Vector3 direction)
    {
        MovementDirection = direction;
        IsMoving = (direction != Vector3.zero);

        Vector3 movement = direction * MoveSpeed * Time.deltaTime;

        //몬스터 이동
        _Rigidbody.MovePosition(transform.position + movement);

        //좌우 반전
        if (direction.x < 0)
        {
            transform.localScale = new Vector3(3, 3, 1);
        }
        else if (direction.x > 0)
        {
            transform.localScale = new Vector3(-3, 3, 1);
        }
    }


    private void Attack()
    {
        Vector3 direction = Player.position - transform.position;
        if (direction.x < 0)
        {
            transform.localScale = new Vector3(3, 3, 1);
        }
        else if (direction.x > 0)
        {
            transform.localScale = new Vector3(-3, 3, 1);
        }

        if (!_Health.IsDead)
        {
            //공격
            {
                //Debug.Log("원거리 공격");
                //총알 프리팹사용
                GameObject bullet = Instantiate(Bullet, BulletPoint.position, BulletPoint.rotation);
                //player 좌표로 공격
                Vector3 Playerdirection = Player.position - BulletPoint.position;
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.AddForce(Playerdirection.normalized * BulletSpeed, ForceMode.Impulse);
            }
        }
        //StartCoroutine(MoveRandomDirection());

        Invoke("EndAttack", AttackDelay);
    }

    private void EndAttack()
    {
        //다시 공격 가능한 상태로 만들기
        IsAttack = false;
        IsMoving = true;
        //Anim.SetBool("IsMove", true);
    }

    //랜덤한 방향으로 이동
    //사용X
    //IEnumerator MoveRandomDirection()
    //{
    //    yield return new WaitForSeconds(0.1f); //0.1초 대기

    //    // 랜덤 방향 생성
    //    Vector3 direction = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized * 30;

    //    // 해당 방향으로 이동
    //    Move(direction);
    //    Debug.Log("정상작동");
    //}

    //IEnumerator DelayAnimation()
    //{
    //    //애니메이션
    //    yield return YieldInstructionCache.WaitForSeconds(1f);
    //    Anim.SetBool("IsAttack", true);
    //}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, ChaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ChaseMaxRange);


        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(AttackStart.position, AttackRangeSize);
    }
}


