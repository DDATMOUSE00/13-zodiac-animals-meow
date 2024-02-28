using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int EnemyMaxHP;
    public int EnemyHP;
    private PlayerAttack _PlayerAttack;
    private EnemyMeleeAttack _EnemyMeleeAttack;
    private EnemyLongAttack _EnemyLongAttack;
    private EnemyBossMouse _EnemyBossMouse;
    public Animator Anim;
    public bool IsDead;
    private DropItem dropItem;
    private Rigidbody _rigidbody;
    public GameObject player;
    public float KnockbackForce = 100f;
    public float KnockbackTime = 0.1f;
    public Slider slider;

    //����ȭ
    //public bool IsStagger;

    //������ ǥ�� UI
    public GameObject HitDamageText;
    public Transform HitDamagePoint;

    private void Awake()
    {
        _PlayerAttack = GetComponent<PlayerAttack>();
        Anim = transform.GetChild(0).GetComponent<Animator>();
        dropItem = GetComponent<DropItem>();
        _rigidbody = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        _EnemyMeleeAttack = GetComponent<EnemyMeleeAttack>();
        _EnemyLongAttack = GetComponent<EnemyLongAttack>();
    }
    private void Start()
    {
        EnemyHP = EnemyMaxHP;
        UIMaxHealth(EnemyMaxHP);
        IsDead = false;
    }

    private void OnEnable()
    {
        ReSetting();
    }
    private void Update()
    {
        if (slider != null)
        {
            UIHealth(EnemyHP);
        }

        //if (slider.value <= 0)
        //    transform.Find("Fill Area").gameObject.SetActive(false);
        //else
        //    transform.Find("Fill Area").gameObject.SetActive(true);
    }

    public void ReSetting()
    {
        EnemyHP = EnemyMaxHP;
        IsDead = false;
        Anim.Rebind();
        // �ڱ� �ڽ��� �����̼� ���� (0,0,0)���� ����
        this.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);

        // �ڽ� ������Ʈ�� �����̼� ���� ��� (0,0,0)���� ����
        foreach (Transform child in transform)
        {
            child.eulerAngles = new Vector3(0, 0, 0);
        }
        Anim.Play("Idle");
    }

    public void UIMaxHealth(int EnemyHP)
    {
        //HP UI
        slider.maxValue = EnemyHP;
        slider.value = EnemyHP;
    }
    public void UIHealth(int EnemyHP)
    {
        //HP UI
        slider.value = EnemyHP;
    }

    public void EnemyHit(int PlayerDamage)
    {
        //���Ͱ� ������
        if (EnemyHP > 0)
        {
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
            else
            {
                //�˹�
                Vector3 forceDirection = (transform.position - player.transform.position).normalized;
                forceDirection.y = 0;
                //forceDirection.z = 0;
                _rigidbody.AddForce(forceDirection * KnockbackForce, ForceMode.VelocityChange);
                StartCoroutine(StopForce(KnockbackTime));
            }

            ////���� ���� ������ ��� ����ȭ ����
            //if (PlayerDamage >= EnemyMaxHP / 6)
            //{
            //    IsStagger = true;

            //    Invoke("Stagger", 1f);
            //}
        }
    }

    IEnumerator StopForce(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        //�� ����
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    //private void Stagger()
    //{
    //    //����ȭ ����
    //    IsStagger = false;
    //}

    private void Die()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
        dropItem.AllDropItems();
    }
}
