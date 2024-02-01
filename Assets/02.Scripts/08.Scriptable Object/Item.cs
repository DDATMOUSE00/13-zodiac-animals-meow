using UnityEngine;

[CreateAssetMenu(menuName = "08.Scriptable Object/ItemSO")]
public class Item :ScriptableObject
{
    public int id;
    public string itemName; //이름 
    public string itemDescription;  // 설명
    public Sprite icon; // 아이콘 
    public ItemType type;
    public int price;

    public bool stackable = true;
}
public enum ItemType
{
    Resource,
    Weapon,
    Consumable
}