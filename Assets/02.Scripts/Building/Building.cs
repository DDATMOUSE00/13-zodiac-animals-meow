using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public string buildingName; // �ǹ� �̸�
    public bool isConstructed; // �ǹ��� �ϼ��Ǿ����� ����
    public SpriteRenderer spriteRenderer; // ��������Ʈ ������ ����
    public Sprite buildingSprite; // �ǹ� ��������Ʈ
    public Slider constructionSlider; // ���� ���� ���� �����̴�
    public ResourceForBuilding[] requiredResources; // �ʿ��� �ڿ�

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
            Debug.Log("�ʿ��� �ڿ��� �����մϴ�.");
        }
    }

    private bool CheckResources()
    {
        // �ڿ� �˻� ���� ����
        return false; // �ϴ� false
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

    // �÷��̾��� �ڿ��� �˻��ϴ� �Լ� (���� �Լ� ����)
    private bool PlayerHasEnoughResource(ResourceForBuilding resource)
    {
        // �÷��̾��� �ڿ��� �˻��ϴ� ���� ����
        return false; // �ϴ� false��
    }
}