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
        Debug.Log("save player data");
        int Gold = Player.GetComponent<PlayerGold>().Gold;
        int MinD = Player.GetComponent<PlayerAttack>().MinDamage;
        int MaxD = Player.GetComponent<PlayerAttack>().MaxDamage;
        int HP = Player.GetComponent<PlayerHealth>().PlayerHP;
        int MaxHP = Player.GetComponent<PlayerHealth>().PlayerMaxHP;
        int SM = Player.GetComponent<PlayerMovement>().PlayerSM;
        int MaxSM = Player.GetComponent<PlayerMovement>().PlayerMaxSM;
        PlayerInfo pInfo = new PlayerInfo(Gold, MinD, MaxD,  HP, MaxHP, SM, MaxSM);

        DataManager.I.SaveJsonData(pInfo, "PlayerData");

    }
    public void LoadPlayerData()
    {
        Debug.Log("load palyer data");
        pInfo = DataManager.I.LoadJsonData<PlayerInfo>("PlayerData");

        Player.GetComponent<PlayerGold>().Gold = pInfo.Gold;
        Player.GetComponent<PlayerAttack>().MinDamage = pInfo.MinDamage;
        Player.GetComponent<PlayerAttack>().MaxDamage = pInfo.MaxDamage;
        Player.GetComponent<PlayerHealth>().PlayerHP = pInfo.HP;
        Player.GetComponent<PlayerHealth>().PlayerMaxHP = pInfo.MaxHP;
        Player.GetComponent<PlayerMovement>().PlayerSM = pInfo.SM;
        Player.GetComponent<PlayerMovement>().PlayerMaxSM = pInfo.MaxSM;
    }

}


[System.Serializable]
public class PlayerInfo
{
    public int Gold;
    public int MinDamage;
    public int MaxDamage;
    public int HP;
    public int MaxHP;
    public int SM;
    public int MaxSM;

    public PlayerInfo(int pGold, int pMinD, int pMaxD, int pHP, int pMaxHP, int pSM, int pMaxSM)
    {
        Gold = pGold;
        MinDamage = pMinD;
        MaxDamage = pMaxD;
        HP = pHP;
        MaxHP = pMaxHP;
        SM = pSM;
        MaxSM = pMaxSM;
    }
}