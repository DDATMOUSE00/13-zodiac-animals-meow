using UnityEngine;

public class Item :ScriptableObject
{
    public int ID;
    public string Name {get; set;} //이름 
    public string Description { get; set; } // 설명
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
