using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    //������
    public int MinDamage = 5;
    public int MaxDamage = 8;
    public float BulletTime = 2f; //����ü ������� �ð�

    private void Start()
    {
        Destroy(gameObject, BulletTime);
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //������ ���
            PlayerHealth PlayerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (PlayerHealth != null)
            {
                int EnemyDamage = Random.Range(MinDamage, MaxDamage + 1);
                PlayerHealth.PlayerHit(EnemyDamage);
                Debug.Log(EnemyDamage);
            }
            Destroy(gameObject);

            //Debug.Log("�÷��̾� ����ü ��Ʈ");
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            //Debug.Log("�� ����ü ��Ʈ");
            Destroy(gameObject);
        }
    }
}
