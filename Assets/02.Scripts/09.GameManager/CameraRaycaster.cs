using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    public Transform player;
    // 마지막으로 히트한 오브젝트들을 저장합니다.
    [SerializeField] private List<GameObject> lastHitObjects = new List<GameObject>();

    private void Start()
    {
        player = GameManager.Instance.player.transform;
    }

    private void Update()
    {
        RaycastHit[] hits;
        // "Tree" 레이어의 오브젝트만 대상
        int layerMask = 1 << LayerMask.NameToLayer("Tree");
        // 카메라에서 플레이어를 향하는 방향을 계산
        Vector3 direction = player.position - transform.position;

        // 이전에 히트했던 오브젝트들의 투명도를 원래대로 복구
        foreach (GameObject obj in lastHitObjects)
        {
            // 오브젝트가 파괴되었는지 확인
            if (obj != null)
            {
                MakeChildrenTransparent(obj, 1f);
            }
        }
        lastHitObjects.Clear();

        // 카메라에서 플레이어를 향하는 방향으로 레이를 쏘아 "Tree" 레이어의 오브젝트를 감지
        hits = Physics.RaycastAll(transform.position, direction, direction.magnitude, layerMask);
        foreach (RaycastHit hit in hits)
        {
            GameObject hitObject = hit.transform.gameObject;
            // 투명도를 50%로 설정
            //Debug.Log(hitObject.name);
            MakeChildrenTransparent(hitObject, 0.5f);
            lastHitObjects.Add(hitObject);
        }
    }

    // 오브젝트의 투명도를 설정하는 함수
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
        //    // 재귀적으로 자식의 자식 오브젝트를 검사합니다.
        //    MakeChildrenTransparent(child.gameObject, transparency);
        //}
    }
}
