using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : PlayerController
{
    public void OnMove(InputValue value)
    {
        Vector3 moveInput = value.Get<Vector3>().normalized;
        CallMoveEvent(moveInput);
    }
    public void OnRoll(InputValue value)
    {
        bool IsRollPressed = value.isPressed;
        CallRollEvent();
    }
    public void OnAttack(InputValue value)
    {
        bool IsAttackPressed = value.isPressed;
        CallAttackEvent();
    }
    public void OnSkill1(InputValue value)
    {
        bool IsSkillPressed1 = value.isPressed;
        CallSkillEvent1();
    }
    public void OnSkill2(InputValue value)
    {
        bool IsSkillPressed2 = value.isPressed;
        CallSkillEvent2();
    }
    public void OnLook(InputValue value)
    {
        Vector2 Aim = value.Get<Vector2>();
        CallLookEvent(Aim);
    }
}
