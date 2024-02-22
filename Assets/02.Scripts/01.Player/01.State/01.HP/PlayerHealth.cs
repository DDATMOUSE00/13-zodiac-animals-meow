using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class PlayerHealth : MonoBehaviour
{
    private SkeletonAnimation Anim;
    private PlayerMovement _PlayerMovement;
    private PlayerAttack _PlayerAttack;

    //ü��UI
    //public TextMeshProUGUI MinHPTEXT;
    //public TextMeshProUGUI MaxHPTEXT;

    //�ǰ� ������UI
    public GameObject HitDamageText;
    public Transform HitDamagePoint;

    //ü��
    public int PlayerHP { get; set; }

    public int PlayerMaxHP { get; set; }

    public Slider slider;

    //����
    public bool IsHit;

    //����
    public bool IsDead;

    private void Awake()
    {
        PlayerMaxHP = 100;
        Anim = GetComponentInChildren<SkeletonAnimation>();
        _PlayerMovement = GetComponent<PlayerMovement>();
        _PlayerAttack = GetComponent<PlayerAttack>();
    }

    private void Start()
    {
        PlayerHP = PlayerMaxHP;
        UIMaxHealth(PlayerMaxHP);
        IsHit = false;
        IsDead = false;
        //MinHPTEXT.text = PlayerHP.ToString();
        //MaxHPTEXT.text = PlayerMaxHP.ToString();
    }

    private void Update()
    {
        if (slider != null)
        {
            UIHealth(PlayerHP);
        }

        // HP�� 0 ������ ��� Die
        if (PlayerHP <= 0)
        {
            IsDead = true;
            Die();
        }
    }

    public void UIMaxHealth(int PlayerHP)
    {
        //HP UI
        slider.maxValue = PlayerHP;
        slider.value = PlayerHP;
    }
    public void UIHealth(int PlayerHP)
    {
        //HP UI
        slider.value = PlayerHP;
    }


    public void PlayerHit(int EnemyDamage)
    {
        if (!IsHit && !_PlayerMovement.IsRolling)
        {
            GameObject HitUI = Instantiate(HitDamageText);
            HitUI.transform.position = HitDamagePoint.position;
            HitUI.GetComponent<HitDamageUI>().EnemyDamage = EnemyDamage;
            //�÷��̾ �¾��� �� HP ��� ��� ����ó��
            if (PlayerHP > 0)
            {
                // HP ����
                PlayerHP -= EnemyDamage;

                //����
                Invisible();
            }
        }
    }

    public void Invisible()
    {
        //����
        //Debug.Log("����");
        IsHit = true;
        _PlayerAttack.ComboCount = 0;
        Invoke("DisInvisible", 0.3f);
    }
    public void DisInvisible()
    {
        //���� ����
        //Debug.Log("���� ����");
        IsHit = false;
    }

    private void Die()
    {
        // �÷��̾� ���
        Debug.Log("�÷��̾� ���");
    }
}
