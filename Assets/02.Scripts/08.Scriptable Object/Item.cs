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
    public string Name {get; set;} //이름 
    public string Description { get; set; } // 설명
    public Sprite Icon; // 아이콘   // id - sprite를 연결 - 딕셔너리로 전달 
    public int Bundle;
    public int Quantity;
    public ItemType type;

}
