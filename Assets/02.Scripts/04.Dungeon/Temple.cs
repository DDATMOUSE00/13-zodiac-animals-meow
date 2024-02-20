using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temple : MonoBehaviour
{
    [SerializeField] private GameObject temple;
    public List<Item> items = new List<Item>();
    [SerializeField] private int receivedCount = 0;
    [SerializeField] private int resourcesNeededCount = 10;
    public Button yesButton;
    public Button noButton;

    private void Start()
    {

    }
    //�κ��丮�� �������� �ִ��� Ȯ��!




    public void YesButton()
    {
        int value = resourcesNeededCount + (resourcesNeededCount * receivedCount);
        //�κ��丮�� �������� �ִ��� Ȯ��!
        //�ʿ��� ��ŭ ����
        receivedCount++;
    }

    public void NoButton()
    {
        temple.SetActive(false);
    }
}
