using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Craft
{
    public string craftName;            // �̸�
    public GameObject go_Prefab;        // ���� ��ġ�� ������
    public GameObject go_PreviewPrefab; // �̸����� ������
}
public class CraftManual : MonoBehaviour
{
    // ���º���
    private Boolean isActivated = false;
    private bool isPreviewActivated = false;

    [SerializeField]
    private GameObject go_BaseUI; // �⺻ ���̽� UI

    [SerializeField]
    private Craft[] craft_building_01;  // ������ ��

    private GameObject go_Preview;  // �̸����� �������� ���� ����
    private GameObject go_Prefab;   // ���� ���� �� �������� ���� ����

    [SerializeField]
    private Transform tf_Player;  // �÷��̾� ��ġ

    // Raycast �ʿ� ���� ����
    private RaycastHit hitInfo;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float range;

    public void SlotClick(int _slotNumber)
    {
        go_Preview = Instantiate(craft_building_01[_slotNumber].go_PreviewPrefab, tf_Player.position + tf_Player.forward, Quaternion.identity);
        go_Prefab = craft_building_01[_slotNumber].go_Prefab;
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
        // 1. ���̸� ���  2.hitInfo�� �����Ѵ�  3. range��ŭ �������� ���  4.layerMask�� �ɸ��°� ������ �ε����� ����
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
