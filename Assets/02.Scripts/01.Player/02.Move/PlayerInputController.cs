using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : PlayerController
{
    public void Awake()
    {
        ;
    }
    public void OnMove(InputValue value)
    {
        Vector3 moveInput = value.Get<Vector3>().normalized;
        CallMoveEvent(moveInput);
        //Debug.Log(moveInput);
    }
    public void OnAttack(InputValue value)
    {
        bool IsAttackPressed = value.isPressed;
        CallAttackEvent();
    }
}
