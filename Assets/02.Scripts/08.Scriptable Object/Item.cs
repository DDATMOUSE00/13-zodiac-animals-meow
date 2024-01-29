using UnityEngine;

public enum ItemType
{
    Resource,
    Weapon,
    Consumable
}
[System.Serializable]
public class Item
{
    public int id;
    public string name {get; set;} //�̸� 
    public string description { get; set; } // ����
    public Sprite icon; // ������   // id - sprite�� ���� - ��ųʸ��� ���� 
    public int bundle;
    public int quantity;
    public string parentName; //�θ�Ʈ������ �̸� 
    public ItemType type;

}
