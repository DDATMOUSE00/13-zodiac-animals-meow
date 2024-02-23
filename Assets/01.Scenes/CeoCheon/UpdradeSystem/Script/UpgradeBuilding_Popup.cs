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


    //public JsonLoader jsonLoader; // �ν����Ϳ��� �Ҵ�

    private void Awake()
    {
        //BuildingUpgrades upgrades = jsonLoader.GetUpgrades(); // JsonLoader���� ���׷��̵� ���� ��������
        //jsonData = JsonUtility.ToJson(jsonData);
        //BuildingUpgrades upgrades = JsonUtility.FromJson<BuildingUpgrades>(jsonData);
        //nextLevelInfo = upgrades.upgradeList[BuildingUpgradeManager.Instance.currentLevel];
        //LoadUpgradeData();
    }
    /*
    private void LoadUpgradeData()
    {
        // JSON ������ �Ľ�
        upgrades = JsonUtility.FromJson<BuildingUpgrades>(jsonData.text);
        nextLevelInfo = upgrades.upgradeList[BuildingUpgradeManager.Instance.currentLevel];
    }
    */
    private void Start()
    {
        _UpgradeBuildingName.text = "�Ʒü�";
        _UpgradeBuildingLv.text = "LV"+BuildingUpgradeManager.Instance.currentLevel.ToString();
        _UpgradeCost.text = BuildingUpgradeManager.Instance.nextLevelInfo.UpgradeCost.ToString();
        _UpgradeBuildingImg.sprite = Resources.Load<Sprite>(BuildingUpgradeManager.Instance.nextLevelInfo.Sprite);
    }

    public void Refresh()
    {
        _UpgradeBuildingName.text = "�Ʒü�";
        _UpgradeBuildingLv.text = "LV" + BuildingUpgradeManager.Instance.currentLevel.ToString();
        _UpgradeCost.text = BuildingUpgradeManager.Instance.nextLevelInfo.UpgradeCost.ToString();
        _UpgradeBuildingImg.sprite = Resources.Load<Sprite>(BuildingUpgradeManager.Instance.nextLevelInfo.Sprite);
        Debug.Log("REFRESH? : "+ BuildingUpgradeManager.Instance.nextLevelInfo.UpgradeCost.ToString());
    }
}
