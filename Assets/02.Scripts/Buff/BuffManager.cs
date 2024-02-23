using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public GameObject player;
    public PlayerHealth playerHealth;
    public PlayerAttack playerAttack;
    public PlayerMovement playerMovement;

    public List<GameObject> buffList = new List<GameObject>(); // 버프 리스트
    //public GameObject BuffIconUI;
    public GameObject selectBuff;
    private void Start()
    {
        playerHealth = player.GetComponent<PlayerHealth>();
        playerAttack = player.GetComponent<PlayerAttack>();
        playerMovement = player.GetComponent<PlayerMovement>();
    }
    public void ApplyBuff()
    {
        int randomIndex = Random.Range(0, buffList.Count);
        selectBuff = buffList[randomIndex];
        var _selectBuff = selectBuff.GetComponent<BaseBuff>();
        if (selectBuff != null) // 이미 버프가 있다면
        {
            RemoveBuff(_selectBuff); // 현재 버프를 제거
        }
        SetBuffIconUI();

        switch (_selectBuff.type)
        {
            case "ATK":
                Debug.Log($"B-Player Attack min, max{playerAttack.MinDamage}, {playerAttack.MaxDamage}");
                playerAttack.ApplyAttackBuff(_selectBuff.buff);
                Debug.Log($"A-Player Attack min, max{playerAttack.MinDamage}, {playerAttack.MaxDamage}");
                break;

            case "SM":
                Debug.Log($"A-Player PlayerMaxSM, PlayerSM{playerMovement.PlayerMaxSM}, {playerMovement.PlayerSM}");
                playerMovement.ApplyMovementBuff(_selectBuff.buff);
                Debug.Log($"A-Player PlayerMaxSM, PlayerSM{playerMovement.PlayerMaxSM}, {playerMovement.PlayerSM}");
                break;

            case "HP":
                Debug.Log($"A-Player Health cur,max{playerHealth.PlayerHP}, {playerHealth.PlayerMaxHP}");
                playerHealth.ApplyHealthBuff(_selectBuff.buff);
                Debug.Log($"A-Player Health cur,max{playerHealth.PlayerHP}, {playerHealth.PlayerMaxHP}");
                break;
        }
    }

    public void RemoveBuff(BaseBuff baseBuff)
    {
        var _selectBuff = selectBuff.GetComponent<BaseBuff>();

        selectBuff = null;
        switch (baseBuff.type)
        {
            case "ATK":
                Debug.Log($"B-Player Attack min, max{playerAttack.MinDamage}, {playerAttack.MaxDamage}");
                playerAttack.RemoveAttackBuff(baseBuff.buff);
                Debug.Log($"A-Player Attack min, max{playerAttack.MinDamage}, {playerAttack.MaxDamage}");
                break;

            case "SM":
                Debug.Log($"A-Player PlayerMaxSM, PlayerSM{playerMovement.PlayerMaxSM}, {playerMovement.PlayerSM}");
                playerMovement.RemoveMovementBuff(baseBuff.buff);
                Debug.Log($"A-Player PlayerMaxSM, PlayerSM{playerMovement.PlayerMaxSM}, {playerMovement.PlayerSM}");
                break;

            case "HP":
                Debug.Log($"A-Player Health cur,max{playerHealth.PlayerHP}, {playerHealth.PlayerMaxHP}");
                playerHealth.RemoveHealthBuff(baseBuff.buff);
                Debug.Log($"A-Player Health cur,max{playerHealth.PlayerHP}, {playerHealth.PlayerMaxHP}");
                break;
        }
    }

    public void SetBuffIconUI()
    {
        //selectBuff.Set();
        selectBuff.SetActive(true);
        var _selectBuff = selectBuff.GetComponent<BaseBuff>();
        Debug.Log(_selectBuff.type);
        for (int i = 0; i < buffList.Count; i++)
        {
            if (!selectBuff)
            {
                buffList[i].SetActive(false);
            }
        }
    }
}
