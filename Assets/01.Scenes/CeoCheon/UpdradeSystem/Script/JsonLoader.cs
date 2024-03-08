using System.Collections.Generic;
using UnityEngine;

public class JsonLoader : MonoBehaviour
{
    // 인스펙터에서 할당할 TextAsset
    public TextAsset jsonFile;

    private BuildingUpgrades upgrades;

    void Start()
    {
        if (jsonFile != null)
        {
            // TextAsset의 내용을 사용하여 BuildingUpgrades 객체로 파싱
            upgrades = JsonUtility.FromJson<BuildingUpgrades>(jsonFile.text);
        }
    }

    // 다른 컴포넌트에서 업그레이드 데이터에 접근할 수 있게 하는 메소드
    public BuildingUpgradeData GetUpgradeData(int level)
    {
        if (level >= 0 && level < upgrades.upgradeList.Count)
        {
            return upgrades.upgradeList[level-1];
        }
        return null; // 유효하지 않은 레벨
    }

    public BuildingUpgrades GetUpgrades()
    {
        return upgrades;
    }
}