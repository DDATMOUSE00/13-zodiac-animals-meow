using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossEarthquake : MonoBehaviour
{
    //������
    public int MinDamage = 5;
    public int MaxDamage = 8;
    public float BulletTime = 1f; //����ü ������� �ð�

    private void Start()
    {
        Destroy(gameObject, BulletTime);
    }
    public void OnTriggerEnter(Collider Colliders)
    {
        if (Colliders.gameObject.CompareTag("Player"))
        {
            //������ ���
            PlayerHealth PlayerHealth = Colliders.gameObject.GetComponent<PlayerHealth>();
            if (PlayerHealth != null)
            {
                int EnemyDamage = Random.Range(MinDamage, MaxDamage + 1);
                PlayerHealth.PlayerHit(EnemyDamage);
                //Debug.Log(EnemyDamage);
            }
            Destroy(gameObject);

            Debug.Log("�÷��̾� ���� ��Ʈ");
        }
    }
}
