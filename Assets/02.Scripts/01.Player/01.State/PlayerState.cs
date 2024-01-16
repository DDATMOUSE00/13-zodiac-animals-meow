using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private PlayerHealth HP;

    private void Start()
    {
        HP = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        int playerHP = HP.PlayerHP;


        //�׽�Ʈ T��ư ������ �ǰ�
        if (Input.GetKeyDown(KeyCode.T))
        {
            HP.Playerhit();
            Debug.Log("Player HP: " + playerHP);
        }
    }
}
