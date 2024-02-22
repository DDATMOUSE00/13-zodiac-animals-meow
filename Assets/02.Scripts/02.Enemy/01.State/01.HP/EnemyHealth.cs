using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyHealth : MonoBehaviour
{
    public int EnemyMaxHP;
    public int EnemyHP;
    private PlayerAttack PAttack;
    public Animator Anim;
    public bool IsDead;

    //������ ǥ�� UI
    public GameObject HitDamageText;
    public Transform HitDamagePoint;

    private void Awake()
    {
        PAttack = GetComponent<PlayerAttack>();
        Anim = transform.GetChild(0).GetComponent<Animator>();
    }
    private void Start()
    {
        EnemyHP = EnemyMaxHP;
        IsDead = false;
    }

    private void OnEnable()
    {
        EnemyHP = EnemyMaxHP;
        IsDead = false;
    }
    public void EnemyHit(int PlayerDamage)
    {
        //���Ͱ� ������
        if (EnemyHP > 0)
        {
            // HP ����
            EnemyHP -= PlayerDamage;
            GameObject HitUI = Instantiate(HitDamageText);
            HitUI.transform.position = HitDamagePoint.position;
            HitUI.GetComponent<HitEnemyDamageUI>().PlayerDamage = PlayerDamage;

            // HP�� 0 ������ ��� Die
            if (EnemyHP <= 0)
            {
                IsDead = true;
                Anim.SetBool("IsDead", true);
                Invoke("Die", 2f);
            }
        }
    }
    private void Die()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
