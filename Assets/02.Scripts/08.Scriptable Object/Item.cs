using UnityEngine;

[CreateAssetMenu(menuName = "08.Scriptable Object/ItemSO")]
public class Item :ScriptableObject
{
    public int id;
    public string itemName; //�̸� 
    public string itemDescription;  // ����
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