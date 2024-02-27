
using UnityEngine;

public class InteractWithBuilding : MonoBehaviour
{
    public Transform tr;
    public RaycastHit hit;
    public float distance = 0.5f;
    public LayerMask layerMask;

     void Start()
    {
        tr = GetComponent<Transform>();
    }

    private void Update()
    {

        Ray ray = new Ray();

        ray.origin = tr.position;
        ray.direction = tr.forward;
        //Physics.Raycast(ray, out hit, distance, layerMask);
        //Debug.Log(hit.transform);
        if (Physics.Raycast(ray, out hit, distance, layerMask)) 
        {
            if (hit.collider != null && Input.GetKeyDown(KeyCode.F))
            {
                //UIWindowOfBuilding ui = hit.transform.gameObject.GetComponent<UIWindowOfBuilding>();
                UIWindowOfBuilding disPlayUI = hit.collider.gameObject.GetComponent<UIWindowOfBuilding>();

                //ui.OpenOrCloseUIWindow();
                disPlayUI.OpenOrCloseUIWindow();
                Debug.Log("키 작동됨");
            }
            Debug.Log("키 작동은 함");
        }
        //Debug.Log("키 작동 안됨");
    }

}
