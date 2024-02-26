
using UnityEngine;

public class InteractWithBuilding : MonoBehaviour
{
    public Transform tr;
    public RaycastHit hit;
    public float distance = 0.001f;
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

        if (Physics.Raycast(ray, out hit, distance, layerMask)) 
        {
            Debug.Log(hit.transform);
            if (hit.transform != null && Input.GetKeyDown(KeyCode.E))
            {
                UIWindowOfBuilding ui = hit.transform.gameObject.GetComponent<UIWindowOfBuilding>();
                
                ui.OpenOrCloseUIWindow();
            }
        }
    }

}
