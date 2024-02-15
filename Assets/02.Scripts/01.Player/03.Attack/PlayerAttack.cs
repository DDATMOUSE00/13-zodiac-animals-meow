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
    public bool IsAttack { get; set; }
    private int ComboCount;
    private float ComboTimer;
    public float ComboTimeLimit = 1.0f;
    private bool IsCombo;
    public int MaxComboCount = 3;


    public int MinDamage = 10;
    public int MaxDamage = 20;

    //공격속도
    public float AttackDelay = 1f;

    //공격범위
    public Vector3 AttackRange;

    //공격 방향 전환을 위해
    private Vector2 AimDirection = Vector2.zero;

    private SkeletonAnimation Anim;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _movement = GetComponent<PlayerMovement>();
        Anim = GetComponentInChildren<SkeletonAnimation>();
        _Health = GetComponentInChildren<PlayerHealth>();
    }
    private void Start()
    {
        _controller.OnAttackEvent += AttackCheck;
        _controller.OnLookEvent += Look;
        ComboCount = 0;
        ComboTimer = 0f;
        IsAttack = false;
        IsCombo = false;
    }
    private void Update()
    {
        //콤보어택
        if (IsCombo)
        {
            ComboTimer += Time.deltaTime;

            if (ComboTimer >= ComboTimeLimit)
            {
                // 콤보 시간이 지나면 콤보 초기화
                ComboCount = 0;
                IsCombo = false;
                ComboTimer = 0f;
                IsAttack = false;
            }
        }

        //애니메이션
        if (IsAttack && !_Health.IsInvincible)
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
    }

    private void Look(Vector2 PlayerAim)
    {
        //공격 방향
        AimDirection = PlayerAim;
    }

    public void AttackCheck()
    {
        if (IsAttack && ComboCount < MaxComboCount && !_movement.IsRolling && IsCombo && !_Health.IsInvincible)
        {
            ComboCount++;
            ComboTimer = 0f;
            Attack();
            //Debug.Log("콤보공격");
        }

        if (!IsAttack && ComboCount < MaxComboCount && !_movement.IsRolling && !_Health.IsInvincible)
        {
            IsAttack = true;
            ComboCount++;
            IsCombo = true;
            ComboTimer = 0f;
            Attack();
            //Debug.Log("그냥공격");
        }
    }
    private void Attack()
    {
        //필요없으면 제거 공격방향
        //왼쪽으로 많이 땡겨야하는 버그있음
        Vector2 PlayerAim = AimDirection.normalized;

        float normalizedX = (Input.mousePosition.x / Screen.width) * 2 - 1;
        //Debug.Log(PlayerAim.x);
        //Debug.Log(normalizedX);
        if (normalizedX < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (normalizedX > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //공격 가능하면 공격
        RealAttack();
    }

    private void RealAttack()
    {
        //범위 찾기
        Collider[] HitColliders = Physics.OverlapBox(AttackStart.position, AttackRange / 2f);
        foreach (Collider collider in HitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                //데미지 계산
                EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    int PlayerDamage = Random.Range(MinDamage, MaxDamage + 1);
                    enemyHealth.EnemyHit(PlayerDamage);
                }
            }

            if (collider.CompareTag("Resource"))
            {
              //  Debug.Log("자원 공격!");
                Resource targetResource = collider.GetComponent<Resource>();
                targetResource.Hit();
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
        //크기와 위치 표시
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(AttackStart.position, AttackRange);
    }
}