using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class PlayerAttack : MonoBehaviour
{
    public Transform AttackStart;
    private PlayerController _controller;
    private PlayerMovement _movement;
    private PlayerHealth _Health;
    private Rigidbody _rigidbody;
    public bool IsAttack { get; set; }
    public bool IsSkill;
    public int ComboCount;
    private float ComboTimer;
    public float ComboTimeLimit;
    public float ComboTimeLowLimit;
    private bool IsCombo;
    public int MaxComboCount = 3;
    public int SkillCount = 0;

    //������ �� ������ ���� �ð�
    public float KnockbackTime;

    //������
    public int MinDamage { get; set; }
    public int MaxDamage { get; set; }

    //���ݼӵ�
    //public float SkillDelay = 2f;

    //���ݹ���
    public Vector3 AttackRange;

    //���� ���� ��ȯ�� ����
    private Vector2 AimDirection = Vector2.zero;

    private SkeletonAnimation Anim;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _movement = GetComponent<PlayerMovement>();
        Anim = GetComponentInChildren<SkeletonAnimation>();
        _Health = GetComponent<PlayerHealth>();
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        MinDamage = 10;
        MaxDamage = 20;
        _controller.OnAttackEvent += AttackCheck;
        _controller.OnLookEvent += Look;
        //_controller.OnSkillEvent1 += Skill1;
        //_controller.OnSkillEvent2 += Skill2;
        ComboCount = 0;
        ComboTimer = 0f;
        ComboTimeLimit = 0.9f; //0.9�ʾȿ� ����(�ĵ�)
        ComboTimeLowLimit = 0.3f; //�ּ� ���ݽð�(����)
        KnockbackTime = ComboTimeLimit / 2;
        IsAttack = false;
        IsCombo = false;
        IsSkill = false;
    }
    private void Update()
    {
        //�޺�����
        if (IsCombo)
        {
            ComboTimer += Time.deltaTime;

            if (ComboTimer >= ComboTimeLimit)
            {
                // �޺� �ð��� ������ �޺� �ʱ�ȭ
                ComboCount = 0;
                ComboTimer = 0f;
                IsCombo = false;
                IsAttack = false;
            }
        }

        //�ִϸ��̼�
        if (IsAttack && !_Health.IsHit && !IsSkill && !_Health.IsDead)
        {
            if (!_movement.IsRolling && ComboCount == 1)
            {
                if (!Anim.AnimationState.GetCurrent(0).Animation.Name.Equals("sd_attack_01"))
                {
                    Anim.AnimationState.SetAnimation(0, "sd_attack_01", false);
                }
            }
            if (!_movement.IsRolling && ComboCount == 2)
            {
                if (!Anim.AnimationState.GetCurrent(0).Animation.Name.Equals("sd_attack_02"))
                {
                    Anim.AnimationState.SetAnimation(0, "sd_attack_02", false);
                }
            }
            if (!_movement.IsRolling && ComboCount == 3)
            {
                if (!Anim.AnimationState.GetCurrent(0).Animation.Name.Equals("sd_attack_03"))
                {
                    Anim.AnimationState.SetAnimation(0, "sd_attack_03", false);
                }
            }
        }
        /* ��ų ���� ������Ʈ
        if (IsSkill)
        {
            if (!_movement.IsRolling && SkillCount == 1)
            {
                if (!Anim.AnimationState.GetCurrent(0).Animation.Name.Equals("skill_01"))
                {
                    Anim.AnimationState.SetAnimation(0, "skill_01", false);
                }
            }
            else if (!_movement.IsRolling && SkillCount == 2)
            {
                if (!Anim.AnimationState.GetCurrent(0).Animation.Name.Equals("skill_02"))
                {
                    Anim.AnimationState.SetAnimation(0, "skill_02", false);
                }
            }
        }
        */
    }

    private void Look(Vector2 PlayerAim)
    {
        //���� ����
        AimDirection = PlayerAim;
    }

    public void AttackCheck()
    {
        ComboTimer += Time.deltaTime;
        StartCoroutine(StopForce(KnockbackTime));

        if (!IsAttack && ComboCount < MaxComboCount && ComboCount == 0 &&!_movement.IsRolling && !_Health.IsHit && !_Health.IsDead)
        {
            IsAttack = true;
            IsCombo = true;
            ComboCount++;
            //ComboTimer = 0f;
            Attack();
            //Debug.Log("�׳ɰ���");
        }

        if (IsAttack && ComboCount < MaxComboCount && !_movement.IsRolling && IsCombo && !_Health.IsHit && ComboTimer <= ComboTimeLimit && ComboTimer >= ComboTimeLowLimit && !_Health.IsDead)
        {
            ComboCount++;
            ComboTimer = 0f;
            Attack();
            //Debug.Log("�޺�����");
        }
    }
    private void Attack()
    {
        float normalizedX = (Input.mousePosition.x / Screen.width) * 2 - 1;
        //�����ϸ鼭 ��¦ �̵�
        float attackMoveForce = 4f;
        if (normalizedX < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            _rigidbody.AddForce(Vector3.left * attackMoveForce, ForceMode.VelocityChange);
        }
        else if (normalizedX > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            _rigidbody.AddForce(Vector3.right * attackMoveForce, ForceMode.VelocityChange);
        }

        //���� �����ϸ� ����
        RealAttack();
    }
    IEnumerator StopForce(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        //�� ����
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    private void RealAttack()
    {
        //���� ã��
        Collider[] HitColliders = Physics.OverlapBox(AttackStart.position, AttackRange / 2f);
        foreach (Collider collider in HitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                //������ ���
                EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    int PlayerDamage = Random.Range(MinDamage, MaxDamage + 1);
                    enemyHealth.EnemyHit(PlayerDamage);
                }
            }

            if (collider.CompareTag("Resource"))
            {
                //  Debug.Log("�ڿ� ����!");
                Resource targetResource = collider.GetComponent<Resource>();
                targetResource.Hit();
            }
        }
        //Invoke("EndAttack", AttackDelay);
    }

    private void EndAttack()
    {
        //�ٽ� ���� ������ ���·� �����
        IsAttack = false;
    }
    /* ��ų ���� ������Ʈ
    private void Skill1()
    {
        if (SkillCount == 0)
        {
            float normalizedX = (Input.mousePosition.x / Screen.width) * 2 - 1;

            if (normalizedX < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (normalizedX > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            IsSkill = true;
            SkillCount = 1;
            Invoke("RealSkill1", 1.667f);
        }
    }

    private void RealSkill1()
    {

        if (IsSkill && SkillCount == 1)
        {
            ;
            IsSkill = false;
            SkillCount = 0;
        }
        //�ɷ�ġ ��� ����

    }

    private void Skill2()
    {
        if (SkillCount == 0)
        {
            float normalizedX = (Input.mousePosition.x / Screen.width) * 2 - 1;

            if (normalizedX < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (normalizedX > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            IsSkill = true;
            SkillCount = 2;
            Invoke("RealSkill2", 1.667f);
        }
    }

    private void RealSkill2()
    {

        if (IsSkill && SkillCount == 2)
        {
            ;
            IsSkill = false;
            SkillCount = 0;
        }
        //���� ����
    }

    //private void EndSkill()
    //{
    //    IsSkill = false;
    //    SkillCount = 0;
    //}
    */
    private void OnDrawGizmosSelected()
    {
        //ũ��� ��ġ ǥ��
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(AttackStart.position, AttackRange);
    }

    public void ApplyAttackBuff(int buffAmount)
    {
        MinDamage += buffAmount;
        MaxDamage += buffAmount;
    }

    public void RemoveAttackBuff(int buffAmount)
    {
        MinDamage -= buffAmount;
        MaxDamage -= buffAmount;
    }
}