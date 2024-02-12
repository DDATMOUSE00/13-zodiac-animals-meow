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
    public BoxCollider AttackRangeBox;
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
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        _Rigidbody = GetComponent<Rigidbody>();
        currentState = MonsterState.Idle;
        Anim = transform.GetChild(0).GetComponent<Animator>();
        _Health = GetComponent<EnemyHealth>();
        GameObject PlayerGameObject = GameObject.FindWithTag("Player");
        Transform PlayerTransform = PlayerGameObject.transform;
    }

    private void Update()
    {
        //몬스터 추적 사거리
        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);

        //몬스터 공격 사거리
        Collider[] EnemyAttack = Physics.OverlapBox(AttackStart.position, AttackRangeSize / 2f);


        //플레이어와의 거리에 따라 상태 변경
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
                    Vector3 directionToPlayer = (Player.position - transform.position).normalized;
                    Move(directionToPlayer);
                }
                break;
            case MonsterState.Attack:
                //플레이어 공격
                if (!IsAttack && !_Health.IsDead)
                {
                    IsAttack = true;
                    IsMoving = false;
                    Anim.SetBool("IsMove", false);
                    Invoke("Attack", AttackTime);
                    StartCoroutine(DelayAnimation());
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
            transform.localScale = new Vector3(3, 3, 3);
        }
        else if (direction.x > 0 && !IsAttack)
        {
            transform.localScale = new Vector3(-3, 3, 3);
        }
    }


    private void Attack()
    {
        Vector3 direction = Player.position - transform.position;
        if (direction.x < 0)
        {
            transform.localScale = new Vector3(3, 3, 3);
        }
        else if (direction.x > 0)
        {
            transform.localScale = new Vector3(-3, 3, 3);
        }
        //Debug.Log(direction);

        if (!_Health.IsDead)
        {
            //공격
            {
                //Debug.Log("원거리 공격");
                //총알 프리팹사용
                GameObject bullet = Instantiate(Bullet, BulletPoint.position, BulletPoint.rotation);
                //Player의 좌표를 가져와서 총알을 발사 방향으로 사용
                Vector3 Playerdirection = Player.position - BulletPoint.position;
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.AddForce(Playerdirection.normalized * BulletSpeed, ForceMode.Impulse);
            }
        }
        Invoke("EndAttack", AttackDelay);
    }

    private void EndAttack()
    {
        currentState = MonsterState.Idle;
        //다시 공격 가능한 상태로 만들기
        IsAttack = false;
    }
    IEnumerator DelayAnimation()
    {
        //애니메이션
        yield return YieldInstructionCache.WaitForSeconds(1f);
        Anim.SetBool("IsAttack", true);
    }

    private void OnDrawGizmosSelected()
    {
        //chaseRange를 그립니다.
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, ChaseRange);

        //attackRange를 그립니다.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ChaseMaxRange);


        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(AttackStart.position, AttackRangeSize);
    }
}
