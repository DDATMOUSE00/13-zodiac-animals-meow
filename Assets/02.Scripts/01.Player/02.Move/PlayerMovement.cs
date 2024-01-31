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


    //구르기
    public bool IsRolling;
    private float RollingTime = 0.5f; //구르는 시간
    private float RollSpeed = 10.0f; //구르는 속도

    //플레이어 스테미너
    public int PlayerMaxSM;
    public int PlayerSM;
    public Slider slider;

    private float SMRecoveryTime = 0.4f; //SM 회복 시간 0.4초
    private int SMRecoveryAmount = 1; //회복되는 SM 양

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
        //구르기
        _Controller.OnRollEvent += Roll;
        PlayerSM = PlayerMaxSM;
        StartCoroutine(SMRecoveryCoroutine());
        //스테미너 UI
        UIMaxSM(PlayerSM);
    }

    private void Update()
    {
        //스테미너 UI
        if (slider != null)
        {
            UISM(PlayerSM);
        }

        //애니메이션
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

    //움직이기
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
                //이동
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

    //구르기
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
            Debug.Log("구르기 이벤트");
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

        //필요없어지면 제거
        //if (IsMoving)
        //{
        //    Anim.AnimationState.AddAnimation(0, "sd_run", true, 0);
        //}
        //else
        //{
        //    Anim.AnimationState.AddAnimation(0, "sd_idle_sword", true, 0);
        //}
    }


    //스테미너 회복
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

    //UI
    public void UIMaxSM(int PlayerSM)
    {
        slider.maxValue = PlayerSM;
        slider.value = PlayerSM;
    }
    public void UISM(int PlayerSM)
    {
        //스테미너 UI
        slider.value = PlayerSM;
    }
}
