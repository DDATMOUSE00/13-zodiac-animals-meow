using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager I;
    private PlayerInfo pInfo;
    public GameObject Player;


    private void Awake()
    {
        I = this;
    }

    public void SavePlayerData()
    {

        int gold = Player.GetComponent<PlayerGold>().Gold;
        int maxD = Player.GetComponent<PlayerAttack>().MaxDamage;

        int minD = Player.GetComponent<PlayerAttack>().MinDamage;
        int HP = Player.GetComponent<PlayerHealth>().PlayerMaxHP;
        int ST = Player.GetComponent<PlayerMovement>().PlayerMaxSM;
        PlayerInfo pInfo = new PlayerInfo(gold, maxD, minD, HP, ST);

        DataManager.I.SaveJsonData(pInfo, "PlayerData");

    }
    public void LoadPlayerData()
    {
        pInfo = DataManager.I.LoadJsonData<PlayerInfo>("PlayerData");

        Player.GetComponent<PlayerGold>().Gold = pInfo.gold;
        Player.GetComponent<PlayerAttack>().MaxDamage = pInfo.maxDamage;
        Player.GetComponent<PlayerAttack>().MinDamage = pInfo.minDamage;
        Player.GetComponent<PlayerHealth>().PlayerMaxHP = pInfo.maxHP;
        Player.GetComponent<PlayerMovement>().PlayerMaxSM = pInfo.maxST;
    }

}


[System.Serializable]
public class PlayerInfo
{

    public int gold;
    public int maxDamage;
    public int minDamage;
    public int maxHP;
    public int maxST;


    public PlayerInfo(int pGold, int pMaxD, int pMinD, int pHP, int pST)
    {
        gold = pGold;
        maxDamage = pMaxD;
        minDamage = pMinD;
        maxHP = pHP;
        maxST = pST;

    }
}