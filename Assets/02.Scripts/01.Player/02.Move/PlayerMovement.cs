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

    //구르기
    private bool IsRolling = false;
    private float RollingTime = 1.0f;
    private float RollSpeed = 10.0f;

    //플레이어 스테미너
    public int PlayerMaxSM;
    public int PlayerSM;
    public Slider slider;

    private float SMRecoveryTime = 0.4f; //SM 회복 시간 0.2초
    private int SMRecoveryAmount = 1; //회복되는 SM 양

    private void Awake()
    {
        _Controller = GetComponent<PlayerController>();
        _Rigidbody = GetComponent<Rigidbody>();
        PlayerMaxSM = 100;

        //애니메이션 추가 예정
        //anim = transform.GetChild(0).GetComponent<Animator>();
    }
    private void Start()
    {
        _Controller.OnMoveEvent += Move;
        //구르기 추가 예정
        _Controller.OnRollEvent += Roll;
        PlayerSM = PlayerMaxSM;
        StartCoroutine(SMRecoveryCoroutine());
        //스테미너 UI
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
            //SM 회복 시간마다
            yield return new WaitForSeconds(SMRecoveryTime);

            //PlayerSM이 PlayerMaxSM보다 작을 때만 회복
            if (PlayerSM < PlayerMaxSM)
            {
                PlayerSM = Mathf.Min(PlayerMaxSM, PlayerSM + SMRecoveryAmount);
            }
        }
    }

    public void UIMaxSM(int PlayerSM)
    {
        //스테미너 UI
        //Debug.Log(slider);
        slider.maxValue = PlayerSM;
        slider.value = PlayerSM;
    }
    public void UISM(int PlayerSM)
    {
        //스테미너 UI
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
            //이동 방향에 따라 구르는 방향 결정
            Vector3 RollDirection = Vector3.zero;

            if (MovementDirection != Vector3.zero)
            {
                //이동 중인 방향을 정규화하여 구르는 방향으로 설정
                RollDirection = MovementDirection.normalized;
            }
            //구르기 시간계산
            while (elapsedTime < RollingTime)
            {
                // 구르는 방향으로 이동
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
        //Debug.Log("움직이는중");

        if (direction.x < 0)
        {
            //skeleton.ScaleX = -1;
            transform.localScale = new Vector3(1, 1, 1);
            //움직이는 애니메이션 추가 예정
            //anim.SetBool("IsMove", true);
        }
        else if (direction.x > 0)
        {
            //skeleton.ScaleX = 1;
            transform.localScale = new Vector3(-1, 1, 1);
            //움직이는 애니메이션 추가 예정
            //anim.SetBool("IsMove", true);
        }
    }
}
