using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public GameObject[] buildingPrefabs; // �ǹ� ������ �迭
    private List<Building> buildings; // ������ �ǹ� ����Ʈ

    void Start()
    {
        buildings = new List<Building>();
    }

    // Ư�� ��ġ�� �ǹ� ����
    public void CreateBuilding(int buildingIndex, Vector3 position)
    {
        if (buildingIndex < 0 || buildingIndex >= buildingPrefabs.Length)
        {
            Debug.LogError("�߸��� �ǹ� �ε����Դϴ�.");
            return;
        }

        GameObject buildingObject = Instantiate(buildingPrefabs[buildingIndex], position, Quaternion.identity);
        Building building = buildingObject.GetComponent<Building>();
        //building.StartConstruction(5.0f); // ex) ���� �ð� 5��
        buildings.Add(building);
    }

    // Ư�� �ǹ� ����
    public void RemoveBuilding(Building building)
    {
        if (buildings.Contains(building))
        {
            buildings.Remove(building);
            Destroy(building);
        }
        else
        {
            Debug.LogError("�������� �ʴ� �ǹ��Դϴ�.");
        }
    }
}