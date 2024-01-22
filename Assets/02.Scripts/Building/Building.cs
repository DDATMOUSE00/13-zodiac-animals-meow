using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public string buildingName; // 건물 이름
    public bool isConstructed; // 건물이 완성되었는지 여부
    public SpriteRenderer spriteRenderer; // 스프라이트 렌더러 참조
    public Sprite buildingSprite; // 건물 스프라이트
    public Slider constructionSlider; // 건축 진행 상태 슬라이더
    public ResourceForBuilding[] requiredResources; // 필요한 자원

    void Start()
    {
        spriteRenderer.sprite = buildingSprite;
        constructionSlider.gameObject.SetActive(false);
    }

    public void TryConstruct()
    {
        if (CheckResources())
        {
            StartConstruction(5.0f);
        }
        else
        {
            Debug.Log("필요한 자원이 부족합니다.");
        }
    }

    private bool CheckResources()
    {
        // 자원 검사 로직 구현
        return false; // 일단 false
    }

    private void StartConstruction(float constructionTime)
    {
        constructionSlider.gameObject.SetActive(true);
        StartCoroutine(GetComponent<ConstructionSystem>().Construct(this, constructionTime));
    }

    public void ConstructBuilding()
    {
        isConstructed = true;
        constructionSlider.gameObject.SetActive(false);
    }

    // 플레이어의 자원을 검사하는 함수 (가상 함수 예시)
    private bool PlayerHasEnoughResource(ResourceForBuilding resource)
    {
        // 플레이어의 자원을 검사하는 로직 구현
        return false; // 일단 false로
    }
}