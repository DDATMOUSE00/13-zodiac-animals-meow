using UnityEngine;

public class Item :ScriptableObject
{
    public int ID;
    public string Name {get; set;} //�̸� 
    public string Description { get; set; } // ����
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
