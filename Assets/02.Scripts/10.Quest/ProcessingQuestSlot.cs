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
    public GameObject Player;
    public TMP_Text Lv;
    public TMP_Text Reward;
    public TMP_Text progress;
    public Button confirmBtn;
    private int itemIDToCollect;
    private int originalQuantityOfTargetItem;
    private int itemNumberToComplete;
    private int animalId;


    public void Setting()
    {
        if (quest.q.QuestType == QuestType.COLLECT)
        {
            itemIDToCollect = quest.q.ItemId;
            itemNumberToComplete = quest.q.ItemQuantityToComplete;
            Reward.text = $"{quest.q.goldReward} G";
            //originalQuantityOfTargetItem = SettingCollectQuest(quest);
        }
        else
        {
            animalId = quest.q.animalId;
            Book b = LibraryManager.I.findBookWithId(quest.q.animalId);
            Reward.text = $"{b.title} 스토리 북";
        }
        QName.text = quest.q.QuestName;
        QDesc.text = quest.q.QuestDesc;
        Lv.text = $"Lv. {quest.q.levelRequirement}";
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
    public void UpdateProgress() ///던전클리어 퀘스트 같은 경우 --> 퀘스트를 CAN_FINISH상태로 바꿔줘야함.
    {

        if(quest.state == QuestState.CAN_FINISH) 
        {
            progress.text = "100%";
            confirmBtn.enabled = true;
                
        }

        if(quest.q.QuestType == QuestType.COLLECT && ItemManager.I.itemDic.ContainsKey(itemIDToCollect)) //collect바께 없어서 
        {
            float progressFloat = ((float)(ItemManager.I.itemDic[itemIDToCollect]) / itemNumberToComplete) * 100;
            if(progressFloat >= 100)
            {
                progressFloat = 100;
            }
            progress.text = $"{progressFloat}%";

            if(progress.text == "100%")
            {
                quest.state = QuestState.CAN_FINISH;
                confirmBtn.enabled = true;
            }
        }

    }
    private bool IsQuestCompleted()
    {
        if (quest.q.QuestType == QuestType.COLLECT)
        {
            if (ItemManager.I.itemDic.ContainsKey(itemIDToCollect) &&
                ItemManager.I.itemDic[itemIDToCollect] < itemNumberToComplete)
            {
                return false;
            }
        }
        return true;
    }

    public void CheckProgress()
    {
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
            GameManager.Instance.player.GetComponent<PlayerGold>().AddGold(quest.q.goldReward);
        }
        else
        {
            LibraryManager.I.AddBooks(animalId);
          
        }
   
        Destroy(this.gameObject);
    }
}
