using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ItemManager.Instance.MakeSOInstance("item1", "Item1");
        ItemManager.Instance.MakeSOInstance("item2", "item2 desc");
       ItemManager.Instance.MakeSOInstance("item3", "item3 desc");
        ItemManager.Instance.MakeSOInstance("Item4", "Item4 desc");
        ItemManager.Instance.MakeSOInstance("Item5", "item5 desc");


     ItemManager.Instance.MakeSOInstance("item1", "Item1");

        ItemManager.Instance.MakeSOInstance("Item5", "item5 desc");
        ItemManager.Instance.MakeSOInstance("Item6", "item6desc");



    }

}
