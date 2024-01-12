using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //  ItemManager.Instance.MakeSOInstance("item1");
        // ItemManager.Instance.MakeSOInstance("item2");
        // ItemManager.Instance.MakeSOInstance("item3");
        //ItemManager.Instance.MakeSOInstance("Item4");
        ResourceManager.Instance.Test();
        //ItemManager.Instance.MakeSOInstance("Item5");
        //ItemManager.Instance.ListItemData();

    }

}
