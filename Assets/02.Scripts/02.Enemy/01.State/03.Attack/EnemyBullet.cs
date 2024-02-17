using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    //데미지
    public int MinDamage = 5;
    public int MaxDamage = 8;
    public float BulletTime = 2f; //투사체 사라지는 시간

    private void Start()
    {
        Destroy(gameObject, BulletTime);
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //데미지 계산
            PlayerHealth PlayerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (PlayerHealth != null)
            {
                int EnemyDamage = Random.Range(MinDamage, MaxDamage + 1);
                PlayerHealth.PlayerHit(EnemyDamage);
                Debug.Log(EnemyDamage);
            }
            Destroy(gameObject);

            //Debug.Log("플레이어 투사체 히트");
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            //Debug.Log("땅 투사체 히트");
            Destroy(gameObject);
        }
    }
}
