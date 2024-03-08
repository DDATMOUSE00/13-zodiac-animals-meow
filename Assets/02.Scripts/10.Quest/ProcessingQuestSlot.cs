using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProcessingQuestSlot : MonoBehaviour
{
    public Quest quest;
    public TextMeshProUGUI QName;
    public TextMeshProUGUI QDesc;
    public TextMeshProUGUI Lv;
    public TextMeshProUGUI Reward;
    public TextMeshProUGUI progress;
    public Button confirmBtn;
    private int itemIDToCollect;
    private int originalQuantityOfTargetItem;
    private int itemNumberToComplete;
    private int animalId;


    public void Setting()
    {
    
            itemIDToCollect = quest.q.ItemId;
            itemNumberToComplete = quest.q.ItemQuantityToComplete;
            //Reward.text = $"{quest.q.goldReward} G";
            //originalQuantityOfTargetItem = SettingCollectQuest(quest);

            animalId = quest.q.animalId;
            Book b = LibraryManager.I.findBookWithId(quest.q.animalId);
            Reward.text = $"{b.title}�� �÷��� ��ǥ";
       
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
    public void UpdateProgress() ///����Ŭ���� ����Ʈ ���� ��� --> ����Ʈ�� CAN_FINISH���·� �ٲ������.
    {

        if(quest.state == QuestState.CAN_FINISH) 
        {
            progress.text = "100%";
            confirmBtn.enabled = true;
                
        }

        if(quest.q.QuestType == QuestType.COLLECT && ItemManager.I.itemDic.ContainsKey(itemIDToCollect)) //collect�ٲ� ��� 
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
       
            ItemManager.I.itemDic[itemIDToCollect] -= itemNumberToComplete;
            ItemManager.I.RefreshInventorySlot();

        BookSlot b = LibraryManager.I.findBookSlotWithId(animalId);
        b.AddStroyBook();
        LibraryManager.I.AddBooks(animalId);
          
          
      
   
        Destroy(this.gameObject);
    }
}
