using UnityEngine;

public class Item :ScriptableObject
{
    public int ID;
    public string Name; //�̸� 
    public string Description; // ����
    public Sprite Icon; // ������ 
    public string Bundle;
    public string Quantity;

    public enum ItemType
    {
        Resource,
        Weapon,
        Consumable
    }
}
