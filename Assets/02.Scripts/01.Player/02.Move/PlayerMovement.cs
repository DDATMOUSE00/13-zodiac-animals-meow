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
    public bool IsMoving;


    //������
    public bool IsRolling;
    private float RollingTime = 0.5f; //������ �ð�
    private float RollSpeed = 10.0f; //������ �ӵ�

    //�÷��̾� ���׹̳�
    public int PlayerMaxSM;
    public int PlayerSM;
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
        if (!IsRolling && !PlayerAttack.IsAttack)
        {
            if (IsMoving && !PlayerAttack.IsAttack)
            {
                if (!Anim.AnimationState.GetCurrent(0).Animation.Name.Equals("sd_run"))
                {
                    Anim.AnimationState.SetAnimation(0, "sd_run", true);
                }
            }
            else
            {
                if (!Anim.AnimationState.GetCurrent(0).Animation.Name.Equals("sd_idle_sword"))
                {
                    Anim.AnimationState.SetAnimation(0, "sd_idle_sword", true);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (!IsRolling)
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

    private void ApplyMovement(Vector3 direction)
    {
        {
            if (!PlayerAttack.IsAttack)
            {
                //�̵�
                direction = direction * 7;
                Vector3 currentVelocity = _Rigidbody.velocity;
                currentVelocity.x = direction.x;
                currentVelocity.z = direction.z;
                _Rigidbody.velocity = currentVelocity;

                if (direction.x < 0 && !PlayerAttack.IsAttack)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else if (direction.x > 0 && !PlayerAttack.IsAttack)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
            }
        }
    }

    //������
    private void Roll()
    {
        if (!IsRolling)
        {
            StartCoroutine(RollCoroutine());
        }
    }
    private IEnumerator RollCoroutine()
    {
        IsRolling = true;
        float elapsedTime = 0f;
        if (PlayerSM >= 20)
        {
            Anim.AnimationState.SetAnimation(0, "sd_rolling", false);
            Debug.Log("������ �̺�Ʈ");
            PlayerSM -= 20;
            //�̵� ���⿡ ���� ������ ���� ����
            Vector3 RollDirection = Vector3.zero;

            if (MovementDirection != Vector3.zero)
            {
                //�̵� ���� ������ ����ȭ�Ͽ� ������ �������� ����
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

        //�ʿ�������� ����
        //if (IsMoving)
        //{
        //    Anim.AnimationState.AddAnimation(0, "sd_run", true, 0);
        //}
        //else
        //{
        //    Anim.AnimationState.AddAnimation(0, "sd_idle_sword", true, 0);
        //}
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
}
