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
    TextMeshPro text;
    Color alpha;
    public int EnemyDamage;

    public GameObject Player;

    void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = EnemyDamage.ToString();
        alpha = text.color;
        Invoke("DestroyObject", DestroyTime);
    }

    void Update()
    {
        transform.Translate(new Vector3(0,Speed * Time.deltaTime,0));
        //alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * AlphaSpeed);
        //text.color = alpha;

        // 플레이어의 뒤집힘 여부를 기준으로 텍스트의 방향을 설정합니다.
        if (Player.transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (Player.transform.localScale.x <= 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }


}
