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
    //인벤토리에 아이템이 있는지 확인!




    public void YesButton()
    {
        int value = resourcesNeededCount + (resourcesNeededCount * receivedCount);
        //인벤토리에 아이템이 있는지 확인!
        //필요한 만큼 제거
        receivedCount++;
    }

    public void NoButton()
    {
        temple.SetActive(false);
    }
}
