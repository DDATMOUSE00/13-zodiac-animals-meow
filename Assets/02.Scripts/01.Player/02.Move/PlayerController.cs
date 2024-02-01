using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action<Vector3> OnMoveEvent;
    public event Action OnRollEvent;
    public event Action OnAttackEvent;
    public event Action<Vector2> OnLookEvent;
    public void CallMoveEvent(Vector3 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }
    public void CallRollEvent()
    {
        OnRollEvent?.Invoke();
        //Debug.Log("구르기 이벤트");
    }
    public void CallAttackEvent()
    {
        OnAttackEvent?.Invoke();
        //Debug.Log("공격 이벤트");
    }
    public void CallLookEvent(Vector2 PlayerAim)
    {
        OnLookEvent?.Invoke(PlayerAim);
    }
}
