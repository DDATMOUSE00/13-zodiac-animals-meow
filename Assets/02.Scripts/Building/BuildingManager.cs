using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public GameObject[] buildingPrefabs; // 건물 프리팹 배열
    private List<Building> buildings; // 생성된 건물 리스트

    void Start()
    {
        buildings = new List<Building>();
    }

    // 특정 위치에 건물 생성
    public void CreateBuilding(int buildingIndex, Vector3 position)
    {
        if (buildingIndex < 0 || buildingIndex >= buildingPrefabs.Length)
        {
            Debug.LogError("잘못된 건물 인덱스입니다.");
            return;
        }

        GameObject buildingObject = Instantiate(buildingPrefabs[buildingIndex], position, Quaternion.identity);
        Building building = buildingObject.GetComponent<Building>();
        //building.StartConstruction(5.0f); // ex) 건축 시간 5초
        buildings.Add(building);
    }

    // 특정 건물 제거
    public void RemoveBuilding(Building building)
    {
        if (buildings.Contains(building))
        {
            buildings.Remove(building);
            Destroy(building);
        }
        else
        {
            Debug.LogError("존재하지 않는 건물입니다.");
        }
    }
}