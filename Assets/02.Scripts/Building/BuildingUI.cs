using UnityEngine;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour
{
    public Text buildingNameText; // 건물 이름 텍스트
    public GameObject buildingPanel; // 건물 관련 UI 패널

    // UI 업데이트
    public void UpdateBuildingUI(Building building)
    {
        buildingNameText.text = building.buildingName;
        buildingPanel.SetActive(true);
    }

    // UI 숨기기
    public void HideBuildingUI()
    {
        buildingPanel.SetActive(false);
    }
}