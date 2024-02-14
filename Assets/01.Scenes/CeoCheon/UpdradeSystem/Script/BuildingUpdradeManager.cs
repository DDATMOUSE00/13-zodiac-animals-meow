using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUpgradeManager : MonoBehaviour
{
    [SerializeField] private GameObject _popupUI;

    public JsonLoader jsonLoader; // �ν����Ϳ��� �Ҵ�
    public int currentLevel = 0; // ���� �ǹ� ����

    void Start()
    {
        // JsonLoader�� ���� ���׷��̵� ���� �ε�
    }

    public void OpenPopup()
    {
        _popupUI.SetActive(true);
    }

    public bool UpgradeBuilding()
    {
        BuildingUpgrades upgrades = jsonLoader.GetUpgrades(); // JsonLoader���� ���׷��̵� ���� ��������
        if (currentLevel >= upgrades.upgradeList.Count) return false; // �ִ� ���� Ȯ��

        BuildingUpgradeData nextLevelInfo = upgrades.upgradeList[currentLevel];
        // ���⼭ �ڿ� Ȯ�� �� ���� ���� ����
        //if(CheckResource(nextLevelInfo))
        if(true)
        {
            // ** �� �ڿ��� �ش��ϴ� KEY�� �Ҵ�
            ItemManager.I.UpdateBundle(0, nextLevelInfo.Wood, "sell");
            ItemManager.I.UpdateBundle(1, nextLevelInfo.Stone, "sell");
            ItemManager.I.UpdateBundle(2, nextLevelInfo.Jelly, "sell");
            ItemManager.I.UpdateBundle(3, nextLevelInfo.Cloud, "sell");

            // ���� Refresh
            ItemManager.I.RefreshInventorySlot();

            // �÷��̾� �ɷ�ġ ���׷��̵� ����
            currentLevel++;
            UpdateBuildingSprite(nextLevelInfo.Sprite);
            return true;
        }
        // �ڿ��� �����ϸ� false ��ȯ
        return false;
    }

    /*
    private bool CheckResource(BuildingUpgradeData nextLevelInfo)
    { 
        nextLevelInfo.Wood = itemDic[id];
        nextLevelInfo.Stone;
        nextLevelInfo.Jelly;
        nextLevelInfo.Cloud;

        nextLevelInfo.UpgradeCost;
    }
    */
    void UpdateBuildingSprite(string spritePath)
    {
        Sprite newSprite = Resources.Load<Sprite>(spritePath);
        GetComponent<SpriteRenderer>().sprite = newSprite;
    }
}