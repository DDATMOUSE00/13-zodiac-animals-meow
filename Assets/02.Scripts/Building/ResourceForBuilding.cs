[System.Serializable]
public class ResourceForBuilding
{
    public string resourceName; // 자원의 이름
    public int amount; // 필요한 자원의 양

    public ResourceForBuilding(string resourceName, int amount)
    {
        this.resourceName = resourceName;
        this.amount = amount;
    }
}