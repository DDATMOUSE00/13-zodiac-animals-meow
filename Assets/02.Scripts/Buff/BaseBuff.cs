using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseBuff : MonoBehaviour
{
    public string type;
    public float percentage;
    public float duration;
    public Image icon;

    private void Awake()
    {
        icon = GetComponent<Image>();
    }

    public void Init(string type, float per, float du)
    {
        this.type = type;
        percentage = per;
        duration = du;
    }

    public void Execute()
    {

    }

    public void DeACtivation()
    {
        Destroy(gameObject);
    }
}
