using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingUpgradeData
{
    public int LV;
    public int Wood;
    public int Stone;
    public int Jelly;
    public int Cloud;
    public int UpgradeCost;
    public int Hp;
    public int Damage;
    public string Sprite;
}

[System.Serializable]
public class BuildingUpgrades
{
    public List<BuildingUpgradeData> upgradeList;
}