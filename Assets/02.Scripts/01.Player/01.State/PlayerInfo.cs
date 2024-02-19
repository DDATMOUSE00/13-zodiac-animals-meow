using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private PlayerMovement _PlayerMovement;
    private PlayerGold _PlayerGold;
    private PlayerHealth _PlayerHealth;
    private PlayerAttack _PlayerAttack;

    private void Awake()
    {
        _PlayerMovement = GetComponent<PlayerMovement>();
        _PlayerGold = GetComponent<PlayerGold>();
        _PlayerHealth = GetComponent<PlayerHealth>();
        _PlayerAttack = GetComponent<PlayerAttack>();
    }
}