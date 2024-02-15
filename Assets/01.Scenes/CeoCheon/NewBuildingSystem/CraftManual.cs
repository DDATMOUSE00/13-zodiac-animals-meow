using Spine.Unity;
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
    public Sprite craftImg;             // 이미지
    public int[] craftItemId;           // 필요한 아이템 ID
    public string[] craftNeedItem;      // 필요한 아이템
    public int[] craftNeedItemCount;    // 필요한 아이템의 개수
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
    private GameObject go_PopupUI_noResource;

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

    #region Raycast
    // Raycast 필요 변수 선언
    private RaycastHit hitInfo;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float range;
    #endregion

    #region Slot UI

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
    [SerializeField]
    private TextMeshProUGUI[] slotNeedItem_Tex;

    #endregion


    private void Start()
    {
        CloseWindow();
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
            slotNeedItem_Tex[i].text = "";
            go_Slots[i].SetActive(false);
        }
    }
    public void RightPageSetting()
    {
        if (page < (craft_SelectedTab.Length / go_Slots.Length) + 1)
            page++;
        else
            page = 1;

        TabSlotSetting(craft_SelectedTab);
    }

    public void LeftPageSetting()
    {
        if (page != 1)
            page--;
        else
            page = (craft_SelectedTab.Length / go_Slots.Length) + 1;

        TabSlotSetting(craft_SelectedTab);
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

            for(int j = 0; j < craft_SelectedTab[i].craftNeedItem.Length; j++ )
            {
                slotNeedItem_Tex[i - startSlotNumber].text += craft_SelectedTab[i].craftNeedItem[j];
                slotNeedItem_Tex[i - startSlotNumber].text += "x" + craft_SelectedTab[i].craftNeedItemCount[j] + "\n";
            }
        }
    }
    public void SlotClick(int _slotNumber)
    {
        selectedSlotNumber = _slotNumber + (page - 1) * go_Slots.Length;

        // Resource가 없다면.
        /*
        if (!CheckResource(selectedSlotNumber))
            ShowPopup(go_PopupUI_noResource);
            return;
        */
        go_Preview = Instantiate(craft_SelectedTab[selectedSlotNumber].go_PreviewPrefab, tf_Player.position + tf_Player.forward, Quaternion.identity);
        go_Prefab = craft_SelectedTab[selectedSlotNumber].go_Prefab;
        isPreviewActivated = true;
        go_BaseUI.SetActive(false);
    }

    // [MERGE INVENTORY]
    private bool CheckResource(int _selectedSlotNumber)
    {
        for(int i = 0; i < craft_SelectedTab[_selectedSlotNumber].craftNeedItem.Length; i++)
        {
            int neededItemId = craft_SelectedTab[_selectedSlotNumber].craftItemId[i]; // 필요한 아이템의 ID
            int neededItemCount = craft_SelectedTab[_selectedSlotNumber].craftNeedItemCount[i]; // 필요한 아이템의 개수

            if (ItemManager.I.itemDic[neededItemId] >= neededItemCount)
            {
                return true;
            }
        }
        return true;     
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B) && !isPreviewActivated)
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

    #region Tab window
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
    private void ShowPopup(GameObject popup)
    {
        popup.SetActive(true);
    }
    #endregion

    #region Inventory에 추가할 것
    /* 아이템이 몇개인지 받아오는 함수
    public int GetItemCount(string _itemName)
    {
        int temp = SearchSlotItem(slots, _itemName);

        return temp != 0 ? temp : SearchSlotItem();
    }
    
    private int SearchSlotItem(Slot[] _slots, string _itemName)
    {
        ItemManager.I.itemDic
    }
     */
    #endregion

}

