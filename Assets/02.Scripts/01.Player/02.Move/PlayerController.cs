using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action<Vector3> OnMoveEvent;
    //구르기 추가
    //public event Action OnRollEvent;
    public event Action OnAttackEvent;
    public void CallMoveEvent(Vector3 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }

    public void CallAttackEvent()
    {
        OnAttackEvent?.Invoke();
        Debug.Log("공격이벤트");
    }

}
