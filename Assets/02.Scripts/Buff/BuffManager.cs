using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public GameObject player;
    public PlayerHealth playerHealth;
    public PlayerAttack playerAttack;
    public PlayerMovement playerMovement;

    public List<GameObject> buffList = new List<GameObject>(); // 버프 리스트
    public List<GameObject> playerBuffList = new List<GameObject>(); // 버프 리스트
    public GameObject newBuff;
    public GameObject buffGroup;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerAttack = player.GetComponent<PlayerAttack>();
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    public void RandomBuff()
    {
        AllDestroyBuff();
        int randomIndex = Random.Range(0, buffList.Count);
        newBuff = buffList[randomIndex];

        GameObject Buff = Instantiate(newBuff, buffGroup.transform);
        playerBuffList.Add(Buff);
        var _selectBuff = Buff.GetComponent<BaseBuff>();
        ApplyBuff(_selectBuff);
    }

    public void AllDestroyBuff()
    {
        for (int i = playerBuffList.Count - 1; i >= 0; i--)
        {
            GameObject buff = playerBuffList[i];
            BaseBuff baseBuffComponent = buff.GetComponent<BaseBuff>();

            if (baseBuffComponent != null)
            {
                RemoveBuff(baseBuffComponent);
                baseBuffComponent.Clear();

                playerBuffList.RemoveAt(i);
            }
        }
    }

    public void ApplyBuff(BaseBuff buff)
    {
        switch (buff.type)
        {
            case "ATK":
                Debug.Log($"B-player Attack min, max{playerAttack.MinDamage}, {playerAttack.MaxDamage}");
                playerAttack.ApplyAttackBuff(buff.buff);
                Debug.Log($"A-player Attack min, max{playerAttack.MinDamage}, {playerAttack.MaxDamage}");
                break;

            case "SM":
                Debug.Log($"A-player PlayerMaxSM, PlayerSM{playerMovement.PlayerMaxSM}, {playerMovement.PlayerSM}");
                playerMovement.ApplyMovementBuff(buff.buff);
                Debug.Log($"A-player PlayerMaxSM, PlayerSM{playerMovement.PlayerMaxSM}, {playerMovement.PlayerSM}");
                break;

            case "HP":
                Debug.Log($"A-player Health cur,max{playerHealth.PlayerHP}, {playerHealth.PlayerMaxHP}");
                playerHealth.ApplyHealthBuff(buff.buff);
                Debug.Log($"A-player Health cur,max{playerHealth.PlayerHP}, {playerHealth.PlayerMaxHP}");
                break;
        }
    }

    public void RemoveBuff(BaseBuff baseBuff)
    {
        //var _selectBuff = selectBuff.GetComponent<BaseBuff>();

        //selectBuff = null;
        switch (baseBuff.type)
        {
            case "ATK":
                Debug.Log($"B-player Attack min, max{playerAttack.MinDamage}, {playerAttack.MaxDamage}");
                playerAttack.RemoveAttackBuff(baseBuff.buff);
                Debug.Log($"A-player Attack min, max{playerAttack.MinDamage}, {playerAttack.MaxDamage}");
                break;

            case "SM":
                Debug.Log($"A-player PlayerMaxSM, PlayerSM{playerMovement.PlayerMaxSM}, {playerMovement.PlayerSM}");
                playerMovement.RemoveMovementBuff(baseBuff.buff);
                Debug.Log($"A-player PlayerMaxSM, PlayerSM{playerMovement.PlayerMaxSM}, {playerMovement.PlayerSM}");
                break;

            case "HP":
                Debug.Log($"A-player Health cur,max{playerHealth.PlayerHP}, {playerHealth.PlayerMaxHP}");
                playerHealth.RemoveHealthBuff(baseBuff.buff);
                Debug.Log($"A-player Health cur,max{playerHealth.PlayerHP}, {playerHealth.PlayerMaxHP}");
                break;
        }
    }

    //public void SetBuffIconUI()
    //{
    //    for (int i = 0; i < playerBuffList.Count; i++)
    //    {
    //        var buff = playerBuffList[i].GetComponent<GameObject>();
    //    }
    //}
}
