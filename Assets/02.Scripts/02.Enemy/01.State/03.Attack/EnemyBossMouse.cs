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

    //������
    public int MinDamage = 5;
    public int MaxDamage = 8;

    //���ݼ�������
    public float AttackTime = 2;

    //����� �ð�
    public float AttackDelay = 1f;

    //���ϼ���
    public int Minpattern = 0;
    public int Maxpattern = 100;

    public GameObject Bullet; //����ü
    public Transform BulletPoint; //����ü �߻� ��ġ
    public Transform BulletPoint2; //����ü �߻� ��ġ
    public float BulletSpeed = 10f; //����ü �ӵ�

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
        //���� ���� ��Ÿ�
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        //���� ���� ��Ÿ�
        Collider[] EnemyAttack = Physics.OverlapBox(AttackStart.position, AttackRange / 2f);


        //�÷��̾���� �Ÿ��� ���� ���� ����
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
                //30%Ȯ�� + �Ϲݰ��ݹ����� ������ Attack
                currentState = MonsterState.Attack;
            }
        }
        else if (distanceToPlayer <= ChaseRange && !IsAttack && !IsSpecialAttack1 && !IsSpecialAttack2)
        {
            //�÷��̾ ���������ȿ� ������ Chase����
            currentState = MonsterState.Chase;
        }
        else if ((currentState == MonsterState.Chase || currentState == MonsterState.Attack || currentState == MonsterState.Special1 || currentState == MonsterState.Special2) && distanceToPlayer > ChaseMaxRange)
        {
            currentState = MonsterState.Idle;
        }

        //���¿� ���� �ൿ ����
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

        // �̵� ���� ���Ϳ� �̵� �ӵ��� ���Ͽ� ���� �̵����� ���
        Vector3 movement = direction * MoveSpeed * Time.deltaTime;

        // ���͸� �̵�����ŭ �̵���Ŵ
        _Rigidbody.MovePosition(transform.position + movement);

        // �̵� ���⿡ ���� ������ �¿� ������ ����
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
        if (IsAttack && !_Health.IsDead && !IsSpecialAttack1 && !IsSpecialAttack2)
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

    //Ư������1
    private IEnumerator RealSpecialAttack1()
    {
        Debug.Log("Ư������1");
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
        yield return new WaitForSeconds(1f); //1�ʰ� ���
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

    //Ư������2
    private IEnumerator RealSpecialAttack2()
    {
        Debug.Log("Ư������2");
        IsSpecialAttack2 = false;
        Anim.SetBool("IsSpecialAttack", false);
        yield return new WaitForSeconds(1f); //1�ʰ� ���
    }

    private void EndAttack()
    {
        //�ٽ� ���� ������ ���·� �����
        IsAttack = false;
        IsSpecialAttack1 = false;
        IsSpecialAttack2 = false;
    }
    IEnumerator DelayAnimation()
    {
        //�Ϲ� ���� �ִϸ��̼� ����
        yield return YieldInstructionCache.WaitForSeconds(1f);
        Anim.SetBool("IsAttack", true);
    }

    //IEnumerator DelayAnimation2()
    //{
    //    //Ư�� ���� �ִϸ��̼� ����
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
