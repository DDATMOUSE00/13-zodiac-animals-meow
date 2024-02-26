
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
            Debug.Log("확인");

            if (hit.collider != null && Input.GetKeyDown(KeyCode.E))
            {
                //UIWindowOfBuilding ui = hit.transform.gameObject.GetComponent<UIWindowOfBuilding>();
                UIWindowOfBuilding disPlayUI = hit.collider.gameObject.GetComponent<UIWindowOfBuilding>();
                Debug.Log("확인");
                //ui.OpenOrCloseUIWindow();
                disPlayUI.OpenOrCloseUIWindow();
                Debug.Log("확인");
            }
        }
    }

}
