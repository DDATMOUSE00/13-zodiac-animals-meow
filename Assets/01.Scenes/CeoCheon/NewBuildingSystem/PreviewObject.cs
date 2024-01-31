using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewObject : MonoBehaviour
{
    // �浹�� ������Ʈ�� �ݶ��̴�
    private List<Collider> colliderList = new List<Collider>();

    [SerializeField]
    private int layerGround; // ���� ���̾�
    private const int IGNORE_RAYCAST_LAYER = 2;

    [SerializeField]
    private Material green;
    [SerializeField]
    private Material red;

    void Start()
    {
        
    }

    void Update()
    {
        Debug.Log("���� �ٲ���");
        ChangeColor();
    }

    private void ChangeColor()
    {
        if(colliderList.Count > 0)
        {
            Debug.Log("RED");
            SetColor(red);
        }
        else
        {
            Debug.Log("GREEN");
            SetColor(green);
        }
    }
    
    private void SetColor(Material mat)
    {
        this.GetComponent<SpriteRenderer>().material = mat;

        /*
        foreach(Transform tf_Child in this.transform)
        {
            var newMaterials = new Material[tf_Child.GetComponent<SpriteRenderer>().materials.Length];

            for(int i = 0; i < newMaterials.Length; i++)
            {
                newMaterials[i] = mat;
            }
            tf_Child.GetComponent<SpriteRenderer>().materials = newMaterials;
        }
        */
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ʈ���� ����");
        if (other.gameObject.layer != layerGround && other.gameObject.layer != IGNORE_RAYCAST_LAYER)
            colliderList.Add(other);
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Ʈ���� ����");
        if (other.gameObject.layer != layerGround && other.gameObject.layer != IGNORE_RAYCAST_LAYER)
            colliderList.Remove(other);
    }
    public bool isBuildable()
    {
        return colliderList.Count == 0;
    }
}
