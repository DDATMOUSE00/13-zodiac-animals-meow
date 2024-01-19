using UnityEngine;
using System.Collections;

public class ConstructionSystem : MonoBehaviour
{
    public IEnumerator Construct(Building building, float constructionTime)
    {
        float currentTime = 0;

        while (currentTime < constructionTime)
        {
            currentTime += Time.deltaTime;
            float progress = currentTime / constructionTime;
            building.constructionSlider.value = progress; // 슬라이더 업데이트
            yield return null;
        }

        building.ConstructBuilding();
    }
}