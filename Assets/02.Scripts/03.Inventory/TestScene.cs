using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ItemManager.Instance.MakeNewItem("item1", "Item1", ItemType.Consumable);
        ItemManager.Instance.MakeNewItem("item2", "item2 desc", ItemType.Consumable);
       ItemManager.Instance.MakeNewItem("item3", "item3 desc", ItemType.Consumable);
        ItemManager.Instance.MakeNewItem("Item4", "Item4 desc",ItemType.Consumable);
        ItemManager.Instance.MakeNewItem("Item5", "item5 desc", ItemType.Consumable);


     ItemManager.Instance.MakeNewItem("item1", "Item1", ItemType.Consumable);

        ItemManager.Instance.MakeNewItem("Item5", "item5 desc", ItemType.Consumable);
        ItemManager.Instance.MakeNewItem("Item6", "item6desc", ItemType.Consumable);



    }

}
