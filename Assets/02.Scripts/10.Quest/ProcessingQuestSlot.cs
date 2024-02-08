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


    public void Setting()
    {
        QName.text = quest.q.QuestName;
        QDesc.text = quest.q.QuestDesc;
        Lv.text = $"Lv. {quest.q.levelRequirement}";
        Reward.text = $"{quest.q.goldReward} G";
        progress.text = $"0%";

        confirmBtn.enabled = false;
    }

    public void CheckProgress()
    {
        if(quest.state == QuestState.CAN_FINISH)
        {
            confirmBtn.enabled = true;
            //progres 텍스트 바꾸기 
        }
    }
    public void CompleteQuest()
    {
        QuestManager.I.CompleteQuest(quest.q.QuestId);
        Destroy(this.gameObject);
    }


}
