using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
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
    public Animator Anim;
    public EnemyHealth _Health;

    private Vector3 MovementDirection = Vector3.zero;
    private Rigidbody _Rigidbody;

    public bool IsAttack;
    public bool IsMoving;

    public float ChaseRange = 10f;
    public float ChaseMaxRange = 20f;
    public float MoveSpeed = 5f;

    //������
    public int MinDamage = 5;
    public int MaxDamage = 8;

    //���ݼ�������
    public float AttackTime = 2;

    //����� �ð�
    public float AttackDelay = 1f;


    private void Awake()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        _Rigidbody = GetComponent<Rigidbody>();
        currentState = MonsterState.Idle;
        Anim = transform.GetChild(0).GetComponent<Animator>();
        _Health = GetComponent<EnemyHealth>();
    }

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        //���� ���� ��Ÿ�
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        //���� ���� ��Ÿ�
        Collider[] EnemyAttack = Physics.OverlapBox(AttackStart.position, AttackRange / 2f);


        //�÷��̾� �Ÿ��� ���� ���� ����
        if (distanceToPlayer <= AttackRange.magnitude / 2f)
        {
            //���� ��Ÿ� �ȿ� ������ ����
            currentState = MonsterState.Attack;
        }
        else if (distanceToPlayer <= ChaseRange)
        {
            //�÷��̾ ���������ȿ� ������ Chase����
            currentState = MonsterState.Chase;
        }
        else if ((currentState == MonsterState.Chase || currentState == MonsterState.Attack) && distanceToPlayer > ChaseMaxRange)
        {
            currentState = MonsterState.Idle;
        }

        //���� ����
        switch (currentState)
        {
            case MonsterState.Idle:
                //�ƹ��͵� �� ��
                Anim.SetBool("IsMove", false);
                Anim.SetBool("IsAttack", false);
                IsAttack = false;
                IsMoving = false;

                break;
            case MonsterState.Chase:
                //�÷��̾ ���� �̵�
                if (!IsAttack && !_Health.IsDead)
                {
                    Anim.SetBool("IsMove", true);
                    Vector3 directionToPlayer = (player.position - transform.position).normalized;
                    Move(directionToPlayer);
                }
                break;
            case MonsterState.Attack:
                //�÷��̾� ����
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

        Vector3 movement = direction * MoveSpeed * Time.deltaTime;

        //���� �̵�
        _Rigidbody.MovePosition(transform.position + movement);

        //�¿� ����
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
        //����
        if (IsAttack && !_Health.IsDead)
        {
            //���� ���� ��Ÿ�
            Collider[] EnemyAttack = Physics.OverlapBox(AttackStart.position, AttackRange / 2f);
            foreach (Collider collider in EnemyAttack)
            {
                if (collider.CompareTag("Player"))
                {
                    //������ ���
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

    private void EndAttack()
    {
        //�ٽ� ���� ������ ���·� �����
        IsAttack = false;
    }
    IEnumerator DelayAnimation()
    {
        //�ִϸ��̼� ������
        yield return YieldInstructionCache.WaitForSeconds(1f);
        Anim.SetBool("IsAttack", true);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, ChaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ChaseMaxRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(AttackStart.position, AttackRange);

    }
}