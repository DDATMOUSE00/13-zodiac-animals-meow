using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class HitDamageUI : MonoBehaviour
{
    public float Speed;
    public float AlphaSpeed;
    public float DestroyTime;
    public TextMeshPro text;
    Color alpha;
    public int EnemyDamage;

    private GameObject Player;

    private void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = EnemyDamage.ToString();
        alpha = text.color;
        Invoke("DestroyObject", DestroyTime);
    }

    private void Update()
    {
        transform.Translate(new Vector3(0,Speed * Time.deltaTime,0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * AlphaSpeed);
        text.color = alpha;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
