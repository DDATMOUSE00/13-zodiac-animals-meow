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
    //public JsonLoader jsonLoader; // 인스펙터에서 할당
    public int currentLevel = 1; // 현재 건물 레벨

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
            DontDestroyOnLoad(gameObject); // 싱글톤 오브젝트를 씬 전환 시에도 파괴되지 않도록 함
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject); // 중복 인스턴스 제거
            }
        }
        // Json
        LoadUpgradeData();
    }

    private void LoadUpgradeData()
    {
        // JSON 데이터 파싱
        upgrades = JsonConvert.DeserializeObject<List<BuildingUpgradeData>>(jsonData.text);
        nextLevelInfo = upgrades[currentLevel];
    }

    public void OpenPopup()
    {
        _popupUI.gameObject.SetActive(true);
    }

    public void UpgradeBuilding()
    {
        //BuildingUpgrades upgrades = jsonLoader.GetUpgrades(); // JsonLoader에서 업그레이드 정보 가져오기
        if (currentLevel >= upgrades.Count) return; // 최대 레벨 확인
        nextLevelInfo = upgrades[currentLevel];

        // 여기서 자원 확인 및 차감 로직 구현
        //if(CheckResource(nextLevelInfo))
        if (true)
        {
            #region CHECK
            /*
            // ** 각 자원에 해당하는 KEY값 할당
            ItemManager.I.UpdateBundle(0, nextLevelInfo.Wood, "sell");
            ItemManager.I.UpdateBundle(1, nextLevelInfo.Stone, "sell");
            ItemManager.I.UpdateBundle(2, nextLevelInfo.Jelly, "sell");
            ItemManager.I.UpdateBundle(3, nextLevelInfo.Cloud, "sell");

            // 슬롯 Refresh
            ItemManager.I.RefreshInventorySlot();
            */
            #endregion
            // 플레이어 능력치 업그레이드 로직
            currentLevel++;
            _playerHealth.PlayerMaxHP += nextLevelInfo.Hp;
            _playerAttack.MaxDamage += nextLevelInfo.Damage;
            _playerAttack.MaxDamage += nextLevelInfo.Damage;
            UpdateBuildingSprite(nextLevelInfo.Sprite);

            Debug.Log(currentLevel);
            Debug.Log("하 도대체 뭐임 "+ nextLevelInfo.UpgradeCost);

            _popupUI.Refresh();
        }
        // 자원이 부족하면 false 반환
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
        Debug.Log("스프라이트 변경");
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