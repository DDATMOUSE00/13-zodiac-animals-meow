
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
            if(hit.transform != null && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Interact");
                UIWindowOfBuilding ui = hit.transform.gameObject.GetComponent<UIWindowOfBuilding>();
                
                ui.OpenOrCloseUIWindow();
            }
        }
       // OnDrawRayline();
    }
    /*
   public void OnDrawRayline()
    {
        if(hit.collider != null)
        {
            Debug.DrawLine(tr.position, tr.position + tr.forward * hit.distance , Color.red);
        }
        else
        {
            Debug.DrawLine(tr.position, tr.position + tr.forward * this.distance, Color.white);
        }

    }
    */
}
