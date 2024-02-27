using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindowOfBuilding : MonoBehaviour
{ 

    public GameObject UI;
    public void OpenOrCloseUIWindow()
    {
        /*
        Debug.Log(UI.name);
        if (UI.name == "QuestUI")
           QuestManager.I.RefreshAllQuest();
        else if (UI.name == "Shop")
      {
           ShopManager.Instance.OnShopButton();
        }
       else if(UI.name == "CardCanvas")
       {
          Shrine.I.Window();
        }
       else if(UI.name == "LibraryUI")
       {
          LibraryManager.I.OpenUI();
        }
        */
        Toggle();
    }

    public void Toggle()
    {
        if (UI.gameObject.activeInHierarchy)
        {
            UI.gameObject.SetActive(false);
        }
        else
        {
            UI.gameObject.SetActive(true);
        }
    }

}
