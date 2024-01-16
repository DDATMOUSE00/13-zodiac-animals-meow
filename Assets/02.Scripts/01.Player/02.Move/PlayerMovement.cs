using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerMovement : MonoBehaviour
{
    private PlayerController _Controller;
    private Vector3 MovementDirection = Vector3.zero;
    private Rigidbody _Rigidbody;
    

    private void Awake()
    {
        _Controller = GetComponent<PlayerController>();
        _Rigidbody = GetComponent<Rigidbody>();

        //�ִϸ��̼� �߰� ����
        //anim = transform.GetChild(0).GetComponent<Animator>();
    }
    private void Start()
    {
        _Controller.OnMoveEvent += Move;
        //������ �߰� ����
        //Controller.OnRollEvent += Roll;
    }

    private void FixedUpdate()
    {
        ApplyMovment(MovementDirection);
    }

    private void Move(Vector3 direction)
    {
        MovementDirection = direction;
    }
    private void ApplyMovment(Vector3 direction)
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
