using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private PlayerController _Controller;
    private PlayerAttack PlayerAttack;
    private Vector3 MovementDirection = Vector3.zero;
    private Rigidbody _Rigidbody;
    private PlayerHealth _Health;
    public bool IsMoving;

    //������
    public bool IsRolling;
    private float RollingTime = 0.667f; //������ �ð�
    private float RollSpeed = 20.0f; //������ �ӵ�

    //�÷��̾� ���׹̳�
    public int PlayerSM { get; set; }

    public int PlayerMaxSM { get; set; }
    public Slider slider;

    private float SMRecoveryTime = 0.4f; //SM ȸ�� �ð� 0.4��
    private int SMRecoveryAmount = 1; //ȸ���Ǵ� SM ��

    private SkeletonAnimation Anim;

    private void Awake()
    {
        _Controller = GetComponent<PlayerController>();
        _Rigidbody = GetComponent<Rigidbody>();
        Anim = GetComponentInChildren<SkeletonAnimation>();
        PlayerAttack = GetComponent<PlayerAttack>();
        _Health = GetComponent<PlayerHealth>();
        PlayerMaxSM = 100;
    }
    private void Start()
    {
        _Controller.OnMoveEvent += Move;
        //������
        _Controller.OnRollEvent += Roll;
        PlayerSM = PlayerMaxSM;
        StartCoroutine(SMRecoveryCoroutine());
        //���׹̳� UI
        UIMaxSM(PlayerSM);

    }

    private void Update()
    {
        //���׹̳� UI
        if (slider != null)
        {
            UISM(PlayerSM);
        }

        //�ִϸ��̼�
        if (!IsRolling && !PlayerAttack.IsAttack && !_Health.IsHit && !PlayerAttack.IsSkill && !_Health.IsDead)
        {
            if (!IsMoving && !PlayerAttack.IsAttack && !_Health.IsHit && !PlayerAttack.IsSkill)
            {
                if (!Anim.AnimationState.GetCurrent(0).Animation.Name.Equals("sd_idle_sword"))
                {
                    Anim.AnimationState.SetAnimation(0, "sd_idle_sword", true);
                }
            }
            else if (IsMoving && !PlayerAttack.IsAttack && !_Health.IsHit && !PlayerAttack.IsSkill)
            {
                if (!Anim.AnimationState.GetCurrent(0).Animation.Name.Equals("sd_run"))
                {
                    Anim.AnimationState.SetAnimation(0, "sd_run", true);
                    AudioManager.Instance.PlaySFX(AudioManager.SFX.Footstep);
                }
            }
        }
        if (_Health.IsHit && !PlayerAttack.IsSkill && !IsRolling && !_Health.IsDead)
        {
            if (!Anim.AnimationState.GetCurrent(0).Animation.Name.Equals("sd_damage"))
            {
                Anim.AnimationState.SetAnimation(0, "sd_damage", false);
                //����
            }
        }
    }

    private void FixedUpdate()
    {
        if (!IsRolling && !_Health.IsDead)
        {
            ApplyMovement(MovementDirection);
        }
    }

    //�����̱�
    private void Move(Vector3 direction)
    {
        MovementDirection = direction;
        IsMoving = (direction != Vector3.zero);
    }

    public void ApplyMovement(Vector3 direction)
    {
        {
            if (!PlayerAttack.IsAttack && !PlayerAttack.IsSkill && !IsRolling && !_Health.IsDead)
            {
                //�̵�
                direction = direction * 10;
                Vector3 currentVelocity = _Rigidbody.velocity;
                currentVelocity.x = direction.x;
                currentVelocity.z = direction.z;
                _Rigidbody.velocity = currentVelocity;

                if (direction.x < 0 && !PlayerAttack.IsAttack && !PlayerAttack.IsSkill && !_Health.IsDead)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else if (direction.x > 0 && !PlayerAttack.IsAttack && !PlayerAttack.IsSkill && !_Health.IsDead)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
            }
        }
    }

    //������
    private void Roll()
    {
        if (!IsRolling && !_Health.IsHit && IsMoving && !_Health.IsDead)
        {
            StartCoroutine(RollCoroutine());
        }
    }
    private IEnumerator RollCoroutine()
    {
        IsRolling = true;
        PlayerAttack.IsAttack = false;
        PlayerAttack.IsSkill = false;
        float elapsedTime = 0f;
        if (PlayerSM >= 20)
        {
            Anim.AnimationState.SetAnimation(0, "sd_rolling", false);
            //Debug.Log("������ �̺�Ʈ");
            PlayerSM -= 20;
            AudioManager.Instance.PlaySFX(AudioManager.SFX.Roll);
            //�̵� ���⿡ ���� ������ ���� ����
            Vector3 RollDirection = Vector3.zero;

            if (MovementDirection != Vector3.zero)
            {
                //������ ���� ����
                RollDirection = MovementDirection.normalized;
            }
            //������ �ð����
            while (elapsedTime < RollingTime)
            {
                // ������ �������� �̵�
                _Rigidbody.velocity = RollDirection * RollSpeed;

                elapsedTime += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }

        IsRolling = false;
    }


    //���׹̳� ȸ��
    private IEnumerator SMRecoveryCoroutine()
    {
        while (true)
        {
            //SM ȸ�� �ð�����
            yield return new WaitForSeconds(SMRecoveryTime);

            //PlayerSM�� PlayerMaxSM���� ���� ���� ȸ��
            if (PlayerSM < PlayerMaxSM)
            {
                PlayerSM = Mathf.Min(PlayerMaxSM, PlayerSM + SMRecoveryAmount);
            }
        }
    }

    //UI
    public void UIMaxSM(int PlayerSM)
    {
        slider.maxValue = PlayerSM;
        slider.value = PlayerSM;
    }
    public void UISM(int PlayerSM)
    {
        //���׹̳� UI
        slider.value = PlayerSM;
    }

    public void ApplyMovementBuff(int buffAmount)
    {
        PlayerMaxSM += buffAmount;
        PlayerSM = PlayerMaxSM;
        SMRecoveryAmount++;
        UIMaxSM(PlayerSM);
    }

    public void RemoveMovementBuff(int buffAmount)
    {
        PlayerMaxSM -= buffAmount;
        PlayerSM = PlayerMaxSM;
        SMRecoveryAmount--;
        UIMaxSM(PlayerSM);
    }
}
