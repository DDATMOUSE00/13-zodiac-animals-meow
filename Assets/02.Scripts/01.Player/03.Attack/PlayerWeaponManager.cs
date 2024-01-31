//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.InputSystem;
//using UnityEngine.InputSystem.XR;

//public class PlayerWeaponManager : MonoBehaviour
//{
//    public PlayerWeapon EquipWeapon;
//    public Transform EquipTrans;

//    private PlayerController _controller;

//    private void Awake()
//    {
//        _controller = GetComponent<PlayerController>();
//    }

//    private void Start()
//    {
//        _controller.OnAttackEvent += EquipWeapon.AttackCheck;
//        //_controller.OnLookEvent += EquipWeapon.Look;
//    }


//    public void EquipNew()
//    {
//        UnEquip();
//        EquipWeapon = GetComponent<PlayerWeapon>();
//    }

//    public void UnEquip()
//    {
//        if (EquipWeapon != null)
//        {
//            Destroy(EquipWeapon.gameObject);
//            EquipWeapon = null;
//        }
//    }
//}
