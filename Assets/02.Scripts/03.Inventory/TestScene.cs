using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ItemManager.Instance.MakeSOInstance("item1","1");
        ItemManager.Instance.MakeSOInstance("item2", "0");
        ItemManager.Instance.MakeSOInstance("item3", "1");
        ItemManager.Instance.MakeSOInstance("Item4", "3");
       // ItemManager.Instance.SetItemData();

    }

}
