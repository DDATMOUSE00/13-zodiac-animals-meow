using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonScrollView : MonoBehaviour
{
    public static DungeonScrollView Instance;
    private ScrollRect scrollRect;
    public List<GameObject> DungeonSlots;
    public DenguonSlot selectDenguonSlot;
    private void Awake()
    {
        Instance = this;
        scrollRect = GetComponentInChildren<ScrollRect>();
    }

    private void Start()
    {
        NewDungeonSlot();
    }
    public void NewDungeonSlot()
    {
        //리스트의 있는 던전 생성
        if (DungeonSlots != null)
        {
            for (int i = 0; i<DungeonSlots.Count; i++)
            {
                var newDungeonSlot = Instantiate(DungeonSlots[i], scrollRect.content);
                //DungeonSlots.Add(newDungeonSlot);

                float y = 100f;

                scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, (y + 10) * DungeonSlots.Count);
            }
        }
    }

    public void SealectDenguonSlot(DenguonSlot denguonSlot)
    {
        selectDenguonSlot = denguonSlot;
        Debug.Log(selectDenguonSlot.sceneName);
    }

    
}
