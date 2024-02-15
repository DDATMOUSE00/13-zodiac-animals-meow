using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProcessingQuestSlot : MonoBehaviour
{
    public Quest quest;
    public TMP_Text QName;
    public TMP_Text QDesc;
    public TMP_Text Lv;
    public TMP_Text Reward;
    public TMP_Text progress;
    public Button confirmBtn;
    private int itemIDToCollect;
    private int originalQuantityOfTargetItem;
    private int itemNumberToComplete;

    public void Setting()
    {
        if (quest.q.QuestType == QuestType.COLLECT)
        {
            itemIDToCollect = quest.q.ItemId;
            itemNumberToComplete = quest.q.ItemQuantityToComplete;
            //originalQuantityOfTargetItem = SettingCollectQuest(quest);
        }
        QName.text = quest.q.QuestName;
        QDesc.text = quest.q.QuestDesc;
        Lv.text = $"Lv. {quest.q.levelRequirement}";
        Reward.text = $"{quest.q.goldReward} G";
        progress.text = "0%";
       confirmBtn.enabled = false;
    }
    public int SettingCollectQuest(Quest q)
    {
        quest = q;
        Debug.Log($"setting {itemIDToCollect}, {q.q.QuestName}");

        if (ItemManager.I.itemDic.ContainsKey(itemIDToCollect))
        {
            return ItemManager.I.itemDic[itemIDToCollect];
        }
        else
        {
            return  0;
        }
    }
    public void UpdateProgress()
    {

        if(quest.state == QuestState.CAN_FINISH)
        {
            progress.text = "100%";
            confirmBtn.enabled = true;
                
        }

        if(quest.q.QuestType == QuestType.COLLECT && ItemManager.I.itemDic.ContainsKey(itemIDToCollect))
        {
            progress.text = $"{((float)(ItemManager.I.itemDic[itemIDToCollect] ) / itemNumberToComplete) * 100 }%";
            if(progress.text == "100%")
            {
                quest.state = QuestState.CAN_FINISH;
                confirmBtn.enabled = true;
            }
        }
    }
    private bool IsQuestCompleted()
    {
        Debug.Log(itemIDToCollect);
        if (ItemManager.I.itemDic.ContainsKey(itemIDToCollect) &&
            ItemManager.I.itemDic[itemIDToCollect] >= itemNumberToComplete)
        {
            return true;
        }
        return false;
    }

    public void CheckProgress()
    {
        Debug.Log($"check progress- {IsQuestCompleted()} ");
        if (IsQuestCompleted())
        {
            quest.state = QuestState.CAN_FINISH;
            CompleteQuest(quest.q.QuestId);

        }

    }
    public void CompleteQuest(string id)
    {
        QuestManager.I.CompleteQuest(id);
        if(quest.q.QuestType== QuestType.COLLECT)
        {
            ItemManager.I.itemDic[itemIDToCollect] -= itemNumberToComplete;
            ItemManager.I.RefreshInventorySlot();
        }
        Destroy(this.gameObject);
    }
}
