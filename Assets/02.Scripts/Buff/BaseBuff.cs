using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseBuff : MonoBehaviour
{
    public string type;
    public int buff;
    public Sprite icon;

    private void Awake()
    {

    }
    private void Start()
    {
        //Init(this.icon);
        Set();
    }

    //public void Init(Sprite _icon)
    //{
    //    icon = _icon;
    //}

    public void Set()
    {
        gameObject.SetActive(true);
        var _icon = GetComponent<Sprite>();
        _icon = icon;
        //Debug.Log(gameObject.activeInHierarchy);
    }

    public void Clear()
    {
        //gameObject.SetActive(false);
        Destroy(gameObject);
        Debug.Log("Á¦°Å");
    }
}
