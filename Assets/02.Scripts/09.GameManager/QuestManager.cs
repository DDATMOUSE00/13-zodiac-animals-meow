using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public static QuestManager I; 
    private Dictionary<int, List<Quest>> questList;
    public Dictionary<string, Quest> allQuests;
    public Transform QuestContainerUI;
    public GameObject questUI;
    public GameObject backBtn;
    public GameObject completeBtn;

    private int PlayerLevel = 2; // 나중에 바깥에서 가져올 정보.
 
    private void Awake()
    {
        I = this;
    //    questList = CreateQuestLIst();
        allQuests = getAllQuests();
    }

    private Dictionary<string, Quest> getAllQuests()
    {
        QuestInfo[] allQuests = Resources.LoadAll<QuestInfo>("Quests");
        Dictionary<string, Quest> AllQuests= new Dictionary<string, Quest>();

        foreach (QuestInfo q in allQuests)
        {
            if (AllQuests.ContainsKey(q.QuestId))
            {
                Debug.Log("key is duplicated");
            }
            else
            {
                Quest newQ = new Quest(q);
                if (PlayerLevel >= newQ.q.levelRequirement)
                    newQ.state = QuestState.CAN_START;  
                AllQuests.Add(newQ.q.QuestId, newQ);
            }
        }
        return AllQuests;
    }
    private Dictionary<int, List<Quest>> CreateQuestLIst()
    {
        QuestInfo[] allQuests = Resources.LoadAll<QuestInfo>("Quests");
      
        Dictionary<int, List<Quest>> LevelToQuestList = new Dictionary<int, List<Quest>>();

        foreach(QuestInfo q in allQuests)
        {
            if (LevelToQuestList.ContainsKey(q.levelRequirement))
            {
                LevelToQuestList[q.levelRequirement].Add(new Quest(q));
            }
            else
            {
                LevelToQuestList.Add(q.levelRequirement, new List<Quest>(new Quest[] { new Quest(q) }) );
            }
        }
        return LevelToQuestList;
    }

    private Quest FindQuestWithId(string id, int level)
    {
        foreach(var quest in questList[level])
        {
            if(quest.q.QuestId == id)
            {
                return quest;
            }
        }
        return null;
    }

    private void ClearAllChildUnderContent()
    {
        foreach(Transform t in QuestContainerUI)
        {
            Destroy(t.gameObject);
        }
    }
    public void RefreshAllQuest()
    {
        if (QuestContainerUI.childCount != 0)
            ClearAllChildUnderContent();


        questUI.SetActive(true);
        backBtn.SetActive(false);
        List<Quest> tmp = allQuests.Values.ToList();
        foreach (var q in tmp)
        {
            if(q.state == QuestState.CAN_START)
            {
                GameObject questPrefab = Resources.Load("QuestSlot") as GameObject;
                GameObject newQuest = Instantiate(questPrefab, QuestContainerUI);
                QuestSlot qslot = newQuest.GetComponent<QuestSlot>();
                qslot.quest = q;
                qslot.Setting();
            }
        }
     
    }

    public void GetQuest(string id)
    {
        Quest quest = allQuests[id];
        quest.state = QuestState.IN_PROGRESS;
    }

    public void CompleteQuest(string id)
    {
        Quest quest = allQuests[id];
        if(quest.state == QuestState.CAN_FINISH)
            quest.state = QuestState.FINISHED;
    }
    public void RefreshProgressingQuest()
    {
        if(QuestContainerUI.childCount != 0)
            ClearAllChildUnderContent();
        backBtn.SetActive(true);
        List<Quest> tmp = allQuests.Values.ToList();
        foreach (var q in tmp)
        {
            if (q.state == QuestState.IN_PROGRESS)
            {
                GameObject questPrefab = Resources.Load("ProcessingQuestSlot") as GameObject;
                GameObject newQuest = Instantiate(questPrefab, QuestContainerUI);
                ProcessingQuestSlot qslot = newQuest.GetComponent<ProcessingQuestSlot>();
                qslot.quest = q;
                qslot.Setting();
            }
        }

    }

}

