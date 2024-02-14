using System.Collections.Generic;
using UnityEngine;

public class JsonLoader : MonoBehaviour
{
    // �ν����Ϳ��� �Ҵ��� TextAsset
    public TextAsset jsonFile;

    private BuildingUpgrades upgrades;

    void Start()
    {
        if (jsonFile != null)
        {
            // TextAsset�� ������ ����Ͽ� BuildingUpgrades ��ü�� �Ľ�
            upgrades = JsonUtility.FromJson<BuildingUpgrades>(jsonFile.text);
        }
    }

    // �ٸ� ������Ʈ���� ���׷��̵� �����Ϳ� ������ �� �ְ� �ϴ� �޼ҵ�
    public BuildingUpgradeData GetUpgradeData(int level)
    {
        if (level >= 0 && level < upgrades.upgradeList.Count)
        {
            return upgrades.upgradeList[level-1];
        }
        return null; // ��ȿ���� ���� ����
    }

    public BuildingUpgrades GetUpgrades()
    {
        return upgrades;
    }
}