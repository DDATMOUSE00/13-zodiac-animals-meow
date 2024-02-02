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

    //���ݽ����ӵ�
    public float AttackTime = 2;

    //���ݼӵ�
    public float AttackDelay = 1f;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _Rigidbody = GetComponent<Rigidbody>();
        currentState = MonsterState.Idle;
    }

    private void Update()
    {
        //���� ���� ��Ÿ�
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        //���� ���� ��Ÿ�
        Collider[] EnemyAttack = Physics.OverlapBox(AttackStart.position, AttackRange / 2f);


        //�÷��̾���� �Ÿ��� ���� ���� ����
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
        else if (currentState == MonsterState.Chase && distanceToPlayer > chaseMaxRange)
        {
            currentState = MonsterState.Idle;
        }

        //���¿� ���� �ൿ ����
        switch (currentState)
        {
            case MonsterState.Idle:
                //�ƹ��͵� �� ��
                IsAttack = false;
                IsMoving = false;
                break;
            case MonsterState.Chase:
                //�÷��̾ ���� �̵�
                if (!IsAttack)
                {
                    Vector3 directionToPlayer = (player.position - transform.position).normalized;
                    Move(directionToPlayer);
                }
                break;
            case MonsterState.Attack:
                //�÷��̾� ����
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

        // �̵� ���� ���Ϳ� �̵� �ӵ��� ���Ͽ� ���� �̵����� ���
        Vector3 movement = direction * MoveSpeed * Time.deltaTime;

        // ���͸� �̵�����ŭ �̵���Ŵ
        _Rigidbody.MovePosition(transform.position + movement);

        // �̵� ���⿡ ���� ������ �¿� ������ ����
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
        //����
        if (IsAttack)
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
        }
        Invoke("EndAttack", AttackDelay);
    }

    private void EndAttack()
    {
        //�ٽ� ���� ������ ���·� �����
        IsAttack = false;
    }

    private void OnDrawGizmosSelected()
    {
        //chaseRange�� �׸��ϴ�.
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, ChaseRange);

        //attackRange�� �׸��ϴ�.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseMaxRange);


        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(AttackStart.position, AttackRange);
    }
}