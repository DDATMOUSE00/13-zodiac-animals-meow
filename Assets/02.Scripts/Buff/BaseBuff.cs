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
        icon = GetComponent<Sprite>();
    }
    private void Start()
    {
        //Init(this.icon);
        Claer();
    }

    //public void Init(Sprite _icon)
    //{
    //    icon = _icon;
    //}

    public void Set()
    {
        gameObject.SetActive(true);
        Debug.Log(gameObject.activeInHierarchy);
    }

    public void Claer()
    {
        gameObject.SetActive(false);
    }
}
