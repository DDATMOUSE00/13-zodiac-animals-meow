[System.Serializable]
public class ResourceForBuilding
{
    public string resourceName; // �ڿ��� �̸�
    public int amount; // �ʿ��� �ڿ��� ��

    public ResourceForBuilding(string resourceName, int amount)
    {
        this.resourceName = resourceName;
        this.amount = amount;
    }
}