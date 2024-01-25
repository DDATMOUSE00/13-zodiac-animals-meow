using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private PlayerHealth Player;
    public Slider HpBar;
    private void Start()
    {
        Player = GetComponent<PlayerHealth>();
    }
    private void Update()
    {
        HpBar.value = Player.PlayerHP / Player.PlayerMaxHP;
    }
}
