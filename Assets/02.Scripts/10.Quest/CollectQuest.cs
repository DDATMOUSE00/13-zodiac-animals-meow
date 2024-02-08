using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectQuest : QuestStep
{

    private int itemIDToCollect;
    private int originalQuantityOfTargetItem;
    private int itemToCollect;

    public void SettingCollectQuest(int id, int number)
    {
        itemIDToCollect = id;
        itemToCollect = number;
        if (ItemManager.I.itemDic.ContainsKey(id))
        {
            originalQuantityOfTargetItem = ItemManager.I.itemDic[itemIDToCollect];
        }
        else
        {
            originalQuantityOfTargetItem = 0;
        }

    }

    private bool IsQuestCompleted()
    {
        if(ItemManager.I.itemDic.ContainsKey(itemIDToCollect) && 
            ItemManager.I.itemDic[itemIDToCollect] - originalQuantityOfTargetItem >= itemToCollect)
        {
            return true;
        }
        return false;
    }
  
    public void CheckProgress()
    {
        if (IsQuestCompleted())
        {
           // Debug.Log("This gameObject is: " + this.gameObject); 
            FinishQuestStep(gameObject);
            
        }
        else
        {
            Debug.Log($"{itemToCollect - ItemManager.I.itemDic[itemIDToCollect]}개 더 모으기");
        }
    }
}
