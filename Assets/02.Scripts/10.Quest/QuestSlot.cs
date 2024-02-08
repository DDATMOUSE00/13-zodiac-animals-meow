using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestSlot : MonoBehaviour
{

    public Quest quest;
    public TMP_Text QName;
    public TMP_Text QDesc;
    public TMP_Text Lv;
    public TMP_Text Reward;

   
    public void Setting()
    {
        QName.text = quest.q.QuestName;
        QDesc.text = quest.q.QuestDesc;
        Lv.text = $"Lv. {quest.q.levelRequirement}";
        Reward.text = $"{quest.q.goldReward} G";

    }

}
