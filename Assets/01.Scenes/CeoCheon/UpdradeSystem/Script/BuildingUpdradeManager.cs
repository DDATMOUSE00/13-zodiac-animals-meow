using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class BuildingUpgradeManager : MonoBehaviour
{
    [SerializeField] private UpgradeBuilding_Popup _popupUI;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private PlayerAttack _playerAttack;
    [SerializeField] private SpriteRenderer _buildingSprite;

    [SerializeField] TextAsset jsonData;
    public BuildingUpgradeData nextLevelInfo;
    //public JsonLoader jsonLoader; // �ν����Ϳ��� �Ҵ�
    public int currentLevel = 1; // ���� �ǹ� ����

    public List<BuildingUpgradeData> upgrades;

    private static BuildingUpgradeManager _instance;
    public static BuildingUpgradeManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BuildingUpgradeManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<BuildingUpgradeManager>();
                    obj.name = typeof(BuildingUpgradeManager).Name;
                }
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // �̱��� ������Ʈ�� �� ��ȯ �ÿ��� �ı����� �ʵ��� ��
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject); // �ߺ� �ν��Ͻ� ����
            }
        }
        // Json
        LoadUpgradeData();
    }

    private void LoadUpgradeData()
    {
        // JSON ������ �Ľ�
        upgrades = JsonConvert.DeserializeObject<List<BuildingUpgradeData>>(jsonData.text);
        nextLevelInfo = upgrades[currentLevel];
    }

    public void OpenPopup()
    {
        _popupUI.gameObject.SetActive(true);
    }

    public void UpgradeBuilding()
    {
        //BuildingUpgrades upgrades = jsonLoader.GetUpgrades(); // JsonLoader���� ���׷��̵� ���� ��������
        if (currentLevel >= upgrades.Count) return; // �ִ� ���� Ȯ��
        nextLevelInfo = upgrades[currentLevel];

        // ���⼭ �ڿ� Ȯ�� �� ���� ���� ����
        //if(CheckResource(nextLevelInfo))
        if (true)
        {
            #region CHECK
            /*
            // ** �� �ڿ��� �ش��ϴ� KEY�� �Ҵ�
            ItemManager.I.UpdateBundle(0, nextLevelInfo.Wood, "sell");
            ItemManager.I.UpdateBundle(1, nextLevelInfo.Stone, "sell");
            ItemManager.I.UpdateBundle(2, nextLevelInfo.Jelly, "sell");
            ItemManager.I.UpdateBundle(3, nextLevelInfo.Cloud, "sell");

            // ���� Refresh
            ItemManager.I.RefreshInventorySlot();
            */
            #endregion
            // �÷��̾� �ɷ�ġ ���׷��̵� ����
            currentLevel++;
            _playerHealth.PlayerMaxHP += nextLevelInfo.Hp;
            _playerAttack.MaxDamage += nextLevelInfo.Damage;
            _playerAttack.MaxDamage += nextLevelInfo.Damage;
            UpdateBuildingSprite(nextLevelInfo.Sprite);

            Debug.Log(currentLevel);
            Debug.Log("�� ����ü ���� "+ nextLevelInfo.UpgradeCost);

            _popupUI.Refresh();
        }
        // �ڿ��� �����ϸ� false ��ȯ
    }

    private bool CheckResource(BuildingUpgradeData nextLevelInfo)
    {
        if (nextLevelInfo.Wood >= ItemManager.I.itemDic[0] &&
            nextLevelInfo.Stone >= ItemManager.I.itemDic[1] &&
            nextLevelInfo.Jelly >= ItemManager.I.itemDic[2] &&
            nextLevelInfo.Cloud >= ItemManager.I.itemDic[3])
        {
            return true;
        }
        return false;
    }
    void UpdateBuildingSprite(string spritePath)
    {
        Debug.Log("��������Ʈ ����");
        Sprite newSprite = Resources.Load<Sprite>(spritePath);
        if(newSprite == null)
        {
            Debug.LogError("Failed to load sprite at path: " + spritePath);
        }
        else
        {
            _buildingSprite.sprite = newSprite;
        }
    }
}