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
    public string name {get; set;} //이름 
    public string description { get; set; } // 설명
    public Sprite icon; // 아이콘   // id - sprite를 연결 - 딕셔너리로 전달 
    public int bundle;
    public int quantity;
    public string parentName; //부모트랜스폼 이름 
    public ItemType type;

}
