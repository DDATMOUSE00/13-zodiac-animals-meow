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
    private Vector3 MovementDirection = Vector3.zero;
    private Rigidbody _Rigidbody;

    //������
    private bool IsRolling = false;
    private float RollingTime = 1.0f;
    private float RollSpeed = 10.0f;

    //�÷��̾� ���׹̳�
    public int PlayerMaxSM;
    public int PlayerSM;
    public Slider slider;

    private float SMRecoveryTime = 0.4f; //SM ȸ�� �ð� 0.2��
    private int SMRecoveryAmount = 1; //ȸ���Ǵ� SM ��

    private void Awake()
    {
        _Controller = GetComponent<PlayerController>();
        _Rigidbody = GetComponent<Rigidbody>();
        PlayerMaxSM = 100;

        //�ִϸ��̼� �߰� ����
        //anim = transform.GetChild(0).GetComponent<Animator>();
    }
    private void Start()
    {
        _Controller.OnMoveEvent += Move;
        //������ �߰� ����
        _Controller.OnRollEvent += Roll;
        PlayerSM = PlayerMaxSM;
        StartCoroutine(SMRecoveryCoroutine());
        //���׹̳� UI
        UIMaxSM(PlayerSM);
    }

    private void Update()
    {
        if (slider != null)
        {
            UISM(PlayerSM);
        }
    }
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

    public void UIMaxSM(int PlayerSM)
    {
        //���׹̳� UI
        //Debug.Log(slider);
        slider.maxValue = PlayerSM;
        slider.value = PlayerSM;
    }
    public void UISM(int PlayerSM)
    {
        //���׹̳� UI
        slider.value = PlayerSM;
    }

    private void FixedUpdate()
    {
        if (!IsRolling)
        {
            ApplyMovement(MovementDirection);
        }
    }

    private void Move(Vector3 direction)
    {
        MovementDirection = direction;
    }
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
    }
    private void ApplyMovement(Vector3 direction)
    {
        direction = direction * 5;
        Vector3 currentVelocity = _Rigidbody.velocity;
        currentVelocity.x = direction.x;
        currentVelocity.z = direction.z;
        _Rigidbody.velocity = currentVelocity;
        //var skeleton = GetComponent<SkeletonAnimation>().Skeleton;
        //Debug.Log("�����̴���");

        if (direction.x < 0)
        {
            //skeleton.ScaleX = -1;
            transform.localScale = new Vector3(1, 1, 1);
            //�����̴� �ִϸ��̼� �߰� ����
            //anim.SetBool("IsMove", true);
        }
        else if (direction.x > 0)
        {
            //skeleton.ScaleX = 1;
            transform.localScale = new Vector3(-1, 1, 1);
            //�����̴� �ִϸ��̼� �߰� ����
            //anim.SetBool("IsMove", true);
        }
    }
}
