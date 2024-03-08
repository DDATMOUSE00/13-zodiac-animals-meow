using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestSlot : MonoBehaviour
{

    public Quest quest;
    public TextMeshProUGUI QName;
    public TextMeshProUGUI QDesc;
    public TextMeshProUGUI Lv;
    public TextMeshProUGUI Reward;

   
    public void Setting()
    {
        QName.text = quest.q.QuestName;
        QDesc.text = quest.q.QuestDesc;
        Lv.text = $"Lv. {quest.q.levelRequirement}";
 
            Book b = LibraryManager.I.findBookWithId(quest.q.animalId);
            Reward.text = $"{b.title} 의 시련의 증표";
       

    }

    public void GetQuest()
    {
        QuestManager.I.GetQuest(quest.q.QuestId);
        Destroy(this.gameObject);
    }

}
