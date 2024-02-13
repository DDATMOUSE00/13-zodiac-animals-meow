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
        itemIDToCollect = quest.q.ItemId;
        itemNumberToComplete = quest.q.ItemQuantityToComplete;
        QName.text = quest.q.QuestName;
        QDesc.text = quest.q.QuestDesc;
        Lv.text = $"Lv. {quest.q.levelRequirement}";
        Reward.text = $"{quest.q.goldReward} G";
        progress.text = "0%";
       // confirmBtn.enabled = false;
    }

    public void UpdateProgress()
    {
        if(quest.state == QuestState.CAN_FINISH)
        {
            progress.text = "100%";
            confirmBtn.enabled = true;
        }

        if(quest.q.QuestType == QuestType.COLLECT)
        {

        }
    }
    private bool IsQuestCompleted()
    {
        Debug.Log(itemIDToCollect);
        if (ItemManager.I.itemDic.ContainsKey(itemIDToCollect) &&
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
            Debug.Log("finish");
           // FinishQuestStep(gameObject);

        }
     
    }
    public void CompleteQuest()
    {
        QuestManager.I.CompleteQuest(quest.q.QuestId);
        Destroy(this.gameObject);
    }
}
