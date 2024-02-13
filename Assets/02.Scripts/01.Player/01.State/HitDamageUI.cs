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

    void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = EnemyDamage.ToString();
        alpha = text.color;
        Invoke("DestroyObject", DestroyTime);


        //필요없어짐
        ////방향 설정
        //if (Player.transform.localScale.x > 0)
        //{
        //    transform.localScale = new Vector3(1, 1, 1);
        //}
        //else if (Player.transform.localScale.x <= 0)
        //{
        //    transform.localScale = new Vector3(-1, 1, 1);
        //}
    }

    void Update()
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
