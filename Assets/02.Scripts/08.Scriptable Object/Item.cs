using UnityEngine;

public class Item :ScriptableObject
{
    public int ID;
    public string Name; //이름 
    public string Description; // 설명
    public Sprite Icon; // 아이콘 
    public string Bundle;
    public string Quantity;

    public enum ItemType
    {
        Resource,
        Weapon,
        Consumable
    }
}
