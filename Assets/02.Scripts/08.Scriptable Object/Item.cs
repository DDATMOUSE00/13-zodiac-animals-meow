using UnityEngine;

public enum ItemType
{
    Resource,
    Weapon,
    Consumable
}
public class Item :ScriptableObject
{
    public int ID;
    public string Name {get; set;} //�̸� 
    public string Description { get; set; } // ����
    public Sprite Icon; // ������   // id - sprite�� ���� - ��ųʸ��� ���� 
    public int Bundle;
    public int Quantity;
    public ItemType type;

}
