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

    //����ü
    public GameObject Bullet; //����ü
    public Transform BulletPoint; //����ü �߻� ��ġ
    public float BulletSpeed = 10f; //����ü �ӵ�

    //������
    public int MinDamage = 5;
    public int MaxDamage = 8;

    //���ݼ�������
    public float AttackTime = 2;

    //����� �ð�
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
        //���� ���� ��Ÿ�
        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);

        //���� ���� ��Ÿ�
        Collider[] EnemyAttack = Physics.OverlapBox(AttackStart.position, AttackRangeSize / 2f);


        //�÷��̾� �Ÿ��� ���� ���� ����
        if (distanceToPlayer <= AttackRangeSize.magnitude / 2f)
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

        //���º���
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
                    Vector3 directionToPlayer = (Player.position - transform.position).normalized;
                    Move(directionToPlayer);
                }
                break;
            case MonsterState.Attack:
                //�÷��̾� ����
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

        //���� �̵�
        _Rigidbody.MovePosition(transform.position + movement);

        //�¿� ����
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
            //����
            {
                //Debug.Log("���Ÿ� ����");
                //�Ѿ� �����ջ��
                GameObject bullet = Instantiate(Bullet, BulletPoint.position, BulletPoint.rotation);
                //player ��ǥ�� ����
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
        //�ٽ� ���� ������ ���·� �����
        IsAttack = false;
        IsMoving = true;
        //Anim.SetBool("IsMove", true);
    }

    //������ �������� �̵�
    //���X
    //IEnumerator MoveRandomDirection()
    //{
    //    yield return new WaitForSeconds(0.1f); //0.1�� ���

    //    // ���� ���� ����
    //    Vector3 direction = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized * 30;

    //    // �ش� �������� �̵�
    //    Move(direction);
    //    Debug.Log("�����۵�");
    //}

    //IEnumerator DelayAnimation()
    //{
    //    //�ִϸ��̼�
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


