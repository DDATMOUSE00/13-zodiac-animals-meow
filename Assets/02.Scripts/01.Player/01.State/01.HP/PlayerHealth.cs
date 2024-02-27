using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class PlayerHealth : MonoBehaviour
{
    private SkeletonAnimation Anim;
    private PlayerMovement _PlayerMovement;
    private PlayerAttack _PlayerAttack;
    private PlayerGold _PlayerGold;

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
        _PlayerGold = GetComponent<PlayerGold>();
    }

    private void Start()
    {
        PlayerHP = PlayerMaxHP;
        UIMaxHealth(PlayerMaxHP);
        IsHit = false;
        IsDead = false;
    }

    private void Update()
    {
        //�׽�Ʈ��
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayerHP -= 10;
        }

        if (slider != null)
        {
            UIHealth(PlayerHP);
        }

        // HP�� 0 ������ ��� Die
        if (PlayerHP <= 0)
        {
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
            //�÷��̾ �¾��� ��
            if (PlayerHP > 0)
            {
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
        if (!IsDead)
        {
            IsDead = true;
            if (!Anim.AnimationState.GetCurrent(0).Animation.Name.Equals("sd_die"))
            {
                Anim.AnimationState.SetAnimation(0, "sd_die", false);
                //Invoke("StopAnimation", 0.7f);
                Invoke("RealDead", 5f);
            }
        }
    }
    private void RealDead()
    {
        if (IsDead)
        {
            IsDead = false;
            PlayerHP = PlayerMaxHP;
            _PlayerGold.RemoveGold(500);
            SceneManager.LoadScene("Village_FINAL");
            GameManager.Instance.player.transform.position = new Vector3(0, 0, -20);
        }
    }

    private void StopAnimation()
    {
        Anim.AnimationState.ClearTracks();
    }

    public void ApplyHealthBuff(int buffAmount)
    {
        PlayerMaxHP += buffAmount;
        PlayerHP = PlayerMaxHP;
        UIMaxHealth(PlayerHP);
    }

    public void RemoveHealthBuff(int buffAmount)
    {
        PlayerMaxHP -= buffAmount;
        PlayerHP = PlayerMaxHP;
        UIMaxHealth(PlayerHP);
    }
}
