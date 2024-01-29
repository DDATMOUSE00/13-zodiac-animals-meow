using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action<Vector3> OnMoveEvent;
    public event Action OnRollEvent;
    public event Action OnAttackEvent;
    public void CallMoveEvent(Vector3 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }
    public void CallRollEvent()
    {
        OnRollEvent?.Invoke();
        Debug.Log("������ �̺�Ʈ");
    }
    public void CallAttackEvent()
    {
        OnAttackEvent?.Invoke();
        Debug.Log("���� �̺�Ʈ");
    }
}
