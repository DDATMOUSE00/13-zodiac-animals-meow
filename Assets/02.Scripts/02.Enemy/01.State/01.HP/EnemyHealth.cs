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

    //무력화
    //public bool IsStagger;

    //데미지 표시 UI
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
        // 자기 자신의 로테이션 값을 (0,0,0)으로 설정
        this.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);

        // 자식 오브젝트의 로테이션 값을 모두 (0,0,0)으로 설정
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
        //몬스터가 맞을때
        if (EnemyHP > 0)
        {
            EnemyHP -= PlayerDamage;
            GameObject HitUI = Instantiate(HitDamageText);
            HitUI.transform.position = HitDamagePoint.position;
            HitUI.GetComponent<HitEnemyDamageUI>().PlayerDamage = PlayerDamage;

            // HP가 0 이하일 경우 Die
            if (EnemyHP <= 0)
            {
                IsDead = true;
                Anim.SetBool("IsDead", true);
                Invoke("Die", 2f);
            }
            else
            {
                //넉백
                Vector3 forceDirection = (transform.position - player.transform.position).normalized;
                forceDirection.y = 0;
                //forceDirection.z = 0;
                _rigidbody.AddForce(forceDirection * KnockbackForce, ForceMode.VelocityChange);
                StartCoroutine(StopForce(KnockbackTime));
            }

            ////강한 공격 맞으면 잠시 무력화 예정
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

        //힘 제거
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    //private void Stagger()
    //{
    //    //무력화 예정
    //    IsStagger = false;
    //}

    private void Die()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
        dropItem.AllDropItems();
    }
}
