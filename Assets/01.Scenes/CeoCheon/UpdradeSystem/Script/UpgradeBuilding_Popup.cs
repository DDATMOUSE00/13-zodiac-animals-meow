using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBuilding_Popup : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _UpgradeBuildingName;
    [SerializeField] TextMeshProUGUI _UpgradeBuildingLv;
    [SerializeField] TextMeshProUGUI _UpgradeCost;
    [SerializeField] Image _UpgradeBuildingImg;
    [SerializeField] TextAsset jsonData;
    public BuildingUpgrades upgrades;


    //public JsonLoader jsonLoader; // 인스펙터에서 할당

    private void Awake()
    {
        //BuildingUpgrades upgrades = jsonLoader.GetUpgrades(); // JsonLoader에서 업그레이드 정보 가져오기
        //jsonData = JsonUtility.ToJson(jsonData);
        //BuildingUpgrades upgrades = JsonUtility.FromJson<BuildingUpgrades>(jsonData);
        //nextLevelInfo = upgrades.upgradeList[BuildingUpgradeManager.Instance.currentLevel];
        //LoadUpgradeData();
    }
    /*
    private void LoadUpgradeData()
    {
        // JSON 데이터 파싱
        upgrades = JsonUtility.FromJson<BuildingUpgrades>(jsonData.text);
        nextLevelInfo = upgrades.upgradeList[BuildingUpgradeManager.Instance.currentLevel];
    }
    */
    private void Start()
    {
        _UpgradeBuildingName.text = "훈련소";
        _UpgradeBuildingLv.text = "LV"+BuildingUpgradeManager.Instance.currentLevel.ToString();
        _UpgradeCost.text = BuildingUpgradeManager.Instance.nextLevelInfo.UpgradeCost.ToString();
        _UpgradeBuildingImg.sprite = Resources.Load<Sprite>(BuildingUpgradeManager.Instance.nextLevelInfo.Sprite);
    }

    public void Refresh()
    {
        _UpgradeBuildingName.text = "훈련소";
        _UpgradeBuildingLv.text = "LV" + BuildingUpgradeManager.Instance.currentLevel.ToString();
        _UpgradeCost.text = BuildingUpgradeManager.Instance.nextLevelInfo.UpgradeCost.ToString();
        _UpgradeBuildingImg.sprite = Resources.Load<Sprite>(BuildingUpgradeManager.Instance.nextLevelInfo.Sprite);
        Debug.Log("REFRESH? : "+ BuildingUpgradeManager.Instance.nextLevelInfo.UpgradeCost.ToString());
    }
}
