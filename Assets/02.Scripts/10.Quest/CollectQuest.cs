using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectQuest : QuestStep
{

    private Quest quest;
    public int itemIDToCollect;
    private int originalQuantityOfTargetItem;
    public int itemNumberToComplete;

    public void SettingCollectQuest(Quest q)
    {
        quest = q;
        Debug.Log($"setting {itemIDToCollect}, {q.q.QuestName}");
      
        if (ItemManager.I.itemDic.ContainsKey(itemIDToCollect))
        {
            originalQuantityOfTargetItem = ItemManager.I.itemDic[itemIDToCollect];
        }
        else
        {
            originalQuantityOfTargetItem = 0;
        }
    }
    /*
public float UpdateProcess()
{
    return ItemManager.I.itemDic[itemIDToCollect] / originalQuantityOfTargetItem;
}

private bool IsQuestCompleted()
{
    Debug.Log(itemIDToCollect);
    if(ItemManager.I.itemDic.ContainsKey(itemIDToCollect) && 
        ItemManager.I.itemDic[itemIDToCollect] - originalQuantityOfTargetItem >= itemNumberToComplete)
    {
        return true;
    }
    return false;
}

public void CheckProgress()
{
    Debug.Log($"check progress- {IsQuestCompleted()} ");
    Debug.Log(quest.q.QuestName);
    if (IsQuestCompleted())
    {
        quest.state = QuestState.CAN_FINISH;
        FinishQuestStep(gameObject);

    }
    else
    {
        Debug.Log($"{itemNumberToComplete - ItemManager.I.itemDic[itemIDToCollect]}개 더 모으기");
    }
}
*/
}
