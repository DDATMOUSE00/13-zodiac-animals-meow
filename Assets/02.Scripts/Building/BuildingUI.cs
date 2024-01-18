using UnityEngine;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour
{
    public Text buildingNameText; // �ǹ� �̸� �ؽ�Ʈ
    public GameObject buildingPanel; // �ǹ� ���� UI �г�

    // UI ������Ʈ
    public void UpdateBuildingUI(Building building)
    {
        buildingNameText.text = building.buildingName;
        buildingPanel.SetActive(true);
    }

    // UI �����
    public void HideBuildingUI()
    {
        buildingPanel.SetActive(false);
    }
}