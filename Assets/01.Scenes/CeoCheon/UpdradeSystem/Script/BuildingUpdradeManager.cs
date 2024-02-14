using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUpgradeManager : MonoBehaviour
{
    [SerializeField] private GameObject _popupUI;

    public JsonLoader jsonLoader; // 인스펙터에서 할당
    public int currentLevel = 0; // 현재 건물 레벨

    void Start()
    {
        // JsonLoader를 통해 업그레이드 정보 로드
    }

    public void OpenPopup()
    {
        _popupUI.SetActive(true);
    }

    public bool UpgradeBuilding()
    {
        BuildingUpgrades upgrades = jsonLoader.GetUpgrades(); // JsonLoader에서 업그레이드 정보 가져오기
        if (currentLevel >= upgrades.upgradeList.Count) return false; // 최대 레벨 확인

        BuildingUpgradeData nextLevelInfo = upgrades.upgradeList[currentLevel];
        // 여기서 자원 확인 및 차감 로직 구현
        //if(CheckResource(nextLevelInfo))
        if(true)
        {
            // ** 각 자원에 해당하는 KEY값 할당
            ItemManager.I.UpdateBundle(0, nextLevelInfo.Wood, "sell");
            ItemManager.I.UpdateBundle(1, nextLevelInfo.Stone, "sell");
            ItemManager.I.UpdateBundle(2, nextLevelInfo.Jelly, "sell");
            ItemManager.I.UpdateBundle(3, nextLevelInfo.Cloud, "sell");

            // 슬롯 Refresh
            ItemManager.I.RefreshInventorySlot();

            // 플레이어 능력치 업그레이드 로직
            currentLevel++;
            UpdateBuildingSprite(nextLevelInfo.Sprite);
            return true;
        }
        // 자원이 부족하면 false 반환
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