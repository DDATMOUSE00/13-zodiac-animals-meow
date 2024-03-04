using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    public Transform player;
    // ���������� ��Ʈ�� ������Ʈ���� �����մϴ�.
    private List<GameObject> lastHitObjects = new List<GameObject>();

    private void Start()
    {
        player = GameManager.Instance.player.transform;
    }

    private void Update()
    {
        RaycastHit[] hits;
        // "Tree" ���̾��� ������Ʈ�� ������� �մϴ�.
        int layerMask = 1 << LayerMask.NameToLayer("Tree");
        // ī�޶󿡼� �÷��̾ ���ϴ� ������ ����մϴ�.
        Vector3 direction = player.position - transform.position;

        // ������ ��Ʈ�ߴ� ������Ʈ���� ������ ������� �����մϴ�.
        foreach (GameObject obj in lastHitObjects)
        {
            MakeChildrenTransparent(obj, 1f);
        }
        lastHitObjects.Clear();

        // ī�޶󿡼� �÷��̾ ���ϴ� �������� ���̸� ��� "Tree" ���̾��� ������Ʈ�� �����մϴ�.
        hits = Physics.RaycastAll(transform.position, direction, direction.magnitude, layerMask);
        foreach (RaycastHit hit in hits)
        {
            GameObject hitObject = hit.transform.gameObject;
            // ������ 50%�� �����մϴ�.
            Debug.Log(hitObject.name);
            MakeChildrenTransparent(hitObject, 0.5f);
            lastHitObjects.Add(hitObject);
        }
    }

    // ������Ʈ�� ������ �����ϴ� �Լ��Դϴ�.
    private void MakeChildrenTransparent(GameObject obj, float transparency)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            Material material = renderer.material;
            if (material != null)
            {
                Color color = material.color;
                color.a = transparency;
                material.color = color;
            }
        }

        //foreach (Transform child in obj.transform)
        //{
        //    Renderer renderer = child.GetComponent<Renderer>();
        //    if (renderer != null)
        //    {
        //        Material material = renderer.material;
        //        if (material != null)
        //        {
        //            Color color = material.color;
        //            color.a = transparency;
        //            material.color = color;
        //        }
        //    }
        //    // ��������� �ڽ��� �ڽ� ������Ʈ�� �˻��մϴ�.
        //    MakeChildrenTransparent(child.gameObject, transparency);
        //}
    }
}
