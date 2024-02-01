using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Craft
{
    public string craftName;            // 이름
    public string craftDes;             // 설명
    public Sprite craftImg;              // 이미지
    public GameObject go_Prefab;        // 실제 설치될 프리팹
    public GameObject go_PreviewPrefab; // 미리보기 프리팹
}
public class CraftManual : MonoBehaviour
{
    // 상태변수
    private Boolean isActivated = false;
    private bool isPreviewActivated = false;

    [SerializeField]
    private GameObject go_BaseUI; // 기본 베이스 UI

    // UI탭
    private int tabNumber = 0;
    private int page = 1;
    private int selectedSlotNumber;
    private Craft[] craft_SelectedTab;

    [SerializeField]
    private Craft[] craft_Building;  // 빌딩용 탭
    [SerializeField]
    private Craft[] craft_stone;

    private GameObject go_Preview;  // 미리보기 프리팹을 담을 변수
    private GameObject go_Prefab;   // 실제 생성 될 프리팹을 담을 변수

    [SerializeField]
    private Transform tf_Player;  // 플레이어 위치

    // Raycast 필요 변수 선언
    private RaycastHit hitInfo;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float range;

    // 필요한 UI Slot 요소
    [SerializeField]
    private GameObject[] go_Slots;

    // 변경되는 부분
    [SerializeField]
    private Image[] slot_Img;
    [SerializeField]
    private TextMeshProUGUI[] slotName_Tex;
    [SerializeField]
    private TextMeshProUGUI[] slotDesc_Tex;

    private void Start()
    {
        tabNumber = 0;
        page = 1;

        // 기본값으로 Building 셋팅
        TabSlotSetting(craft_Building);
    }

    public void TabSetting(int _tabNumber)
    {
        tabNumber = _tabNumber;
        page = 1;

        switch(_tabNumber)
        {
            case 0:
                // 빌딩 셋팅
                TabSlotSetting(craft_Building);
                break;
            case 1:
                // 스톤 셋팅
                TabSlotSetting(craft_stone);
                break;
        }
    }

    private void ClearSlot()
    {
        for(int i = 0; i < go_Slots.Length; i++)
        {
            slot_Img[i].sprite = null;
            slotName_Tex[i].text = "";
            slotDesc_Tex[i].text = "";
            go_Slots[i].SetActive(false);
        }
    }

    private void TabSlotSetting(Craft[] craftTab)
    {
        // 초기화
        ClearSlot();

        craft_SelectedTab = craftTab;

        int startSlotNumber = (page - 1) * go_Slots.Length; // 4의 배수

        for(int i = startSlotNumber; i < craft_SelectedTab.Length; i++)
        {
            if (i == page * go_Slots.Length)
                break;

            go_Slots[i - startSlotNumber].SetActive(true);

            slot_Img[i - startSlotNumber].sprite = craftTab[i].craftImg;
            slotName_Tex[i - startSlotNumber].text = craftTab[i].craftName;
            slotDesc_Tex[i - startSlotNumber].text = craftTab[i].craftDes;
        }
    }
    public void SlotClick(int _slotNumber)
    {
        selectedSlotNumber = _slotNumber + (page - 1) * go_Slots.Length;

        go_Preview = Instantiate(craft_SelectedTab[selectedSlotNumber].go_PreviewPrefab, tf_Player.position + tf_Player.forward, Quaternion.identity);
        go_Prefab = craft_SelectedTab[selectedSlotNumber].go_Prefab;
        isPreviewActivated = true;
        go_BaseUI.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && !isPreviewActivated)
            Window();

        if(isPreviewActivated)
            PreviewPositionUpdate();

        if (Input.GetKeyDown(KeyCode.Escape))
            Cancel();

        if (Input.GetButtonDown("Fire1"))
            Build();
    }
        
    private void Build()
    {
        if(isPreviewActivated && go_Preview.GetComponentInChildren<PreviewObject>().isBuildable())
        {
            Instantiate(go_Prefab, hitInfo.point, Quaternion.identity);
            Destroy(go_Preview);
            isActivated = false;
            isPreviewActivated = false;
            go_Preview = null;
            go_Prefab = null;
        }
    }
    private void PreviewPositionUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction * range, Color.red, 1.0f);
        // 1. 레이를 쏘아  2.hitInfo에 저장한다  3. range만큼 레이저를 쏜다  4.layerMask에 걸리는게 있으면 부딪히게 만듬
        if (Physics.Raycast(ray , out hitInfo, range, layerMask))
        {
            if(hitInfo.transform != null)
            {
                go_Preview.transform.position = hitInfo.point;
                // Debug.Log("Raycast hit at: " + hitInfo.point);
                Debug.DrawRay(ray.origin, ray.direction * range, Color.red, 1.0f);
            }
        }
    }

    private void Window()
    {
        if (!isActivated)
        {
            OpenWindow();
        }
        else
        {
            CloseWindow();
        }
    }

    private void Cancel()
    {
        if(isPreviewActivated)
            Destroy(go_Preview);

        isActivated = false;
        isPreviewActivated= false;
        go_Preview = null;
        go_Prefab = null;

        go_BaseUI.SetActive(false);
    }

    private void OpenWindow()
    {
        isActivated = true;
        go_BaseUI.SetActive(true);
    }

    private void CloseWindow()
    {
        isActivated = false;
        go_BaseUI.SetActive(false);
    }
}
