using UnityEngine;

[CreateAssetMenu(menuName = "08.Scriptable Object/ItemSO")]
public class Item :ScriptableObject
{
    public int id;
    public string name {get; set;} //�̸� 
    public string description { get; set; } // ����
    public Sprite icon; // ������ 
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