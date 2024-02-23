using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public static QuestManager I; 
    //private Dictionary<int, List<Quest>> questList;
    public Dictionary<string, Quest> allQuests;
    public Transform QuestContainerUI;
    public GameObject questUI;
    public GameObject backBtn;
    public GameObject completeBtn;
    public Dictionary<string, QuestInformation> SavedQuestInfo;

    private int PlayerLevel = 2; // 나중에 바깥에서 가져올 정보.
 
    private void Awake()
    {
        I = this;
        //    questList = CreateQuestLIst();
        SavedQuestInfo = new Dictionary<string, QuestInformation>();
        allQuests = getAllQuests();
    }
    public void SaveQuestData()
    {
        List<string> keyList = new List<string>(allQuests.Keys);
        for (int i = 0; i < keyList.Count; i++)
        {
            if (!SavedQuestInfo.ContainsKey(keyList[i]))
            {
                QuestInformation qinfo = new();
                qinfo.state = allQuests[keyList[i]].state;
                qinfo.type = allQuests[keyList[i]].q.QuestType;

                if (qinfo.type == QuestType.COLLECT)
                {
                    
                    qinfo.SOPath = $"Quests/CollectQuest/{allQuests[keyList[i]].q.QInfoName}.asset";
                }
                else
                {
                    qinfo.SOPath = $"Quests/DungeonClearQuest/{allQuests[keyList[i]].q.QInfoName}.asset";
                }
                SavedQuestInfo.Add(allQuests[keyList[i]].q.QuestId, qinfo);
            }
            else
            {
                SavedQuestInfo[keyList[i]].state = allQuests[keyList[i]].state;
            }
        }
      
        DataManager.I.SaveJsonData(SavedQuestInfo, "QuestData");

    }
    public void LoadQuestData()
    {
        SavedQuestInfo = DataManager.I.LoadJsonData<Dictionary<string, QuestInformation>>("QuestData");
        List<string> keyList = new List<string>(SavedQuestInfo.Keys);
        for (int i = 0; i < keyList.Count; i++)
        {
            Quest q = allQuests[keyList[i]];
            
            if (q != null)
            {
                q.state = SavedQuestInfo[keyList[i]].state;
                q.q.QuestType = SavedQuestInfo[keyList[i]].type;
            }
        }
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
    /*
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
    */
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

    public void CheckCollectQuestProcess(string id)
    {
        Debug.Log($"Check collect Quest -{id}");
        Quest quest = allQuests[id];
       

        quest.state = QuestState.CAN_FINISH;
    }


    public void GetQuest(string id)
    {
        Quest quest = allQuests[id];
        quest.state = QuestState.IN_PROGRESS;
       // QuestStep obj = Instantiate(quest.q.questPrefabs);
      /*  if (quest.q.QuestType == QuestType.COLLECT)
        {
            CollectQuest cQuest = obj.GetComponent<CollectQuest>();
            cQuest.SettingCollectQuest(quest);
        }
      */
    }

    public void CompleteQuest(string id)
    {
        Quest quest = allQuests[id];
        if (quest.state == QuestState.CAN_FINISH)
        {
            quest.state = QuestState.FINISHED;
           
        }


    }
    public void RefreshProgressingQuest()
    {
        if(QuestContainerUI.childCount != 0)
            ClearAllChildUnderContent();
        backBtn.SetActive(true);
        List<Quest> tmp = allQuests.Values.ToList();
        foreach (var q in tmp)
        {
            if (q.state == QuestState.IN_PROGRESS || q.state == QuestState.CAN_FINISH)
            {
                GameObject questPrefab = Resources.Load("ProcessingQuestSlot") as GameObject;
                GameObject newQuest = Instantiate(questPrefab, QuestContainerUI);
                ProcessingQuestSlot qslot = newQuest.GetComponent<ProcessingQuestSlot>();
                qslot.quest = q;
                qslot.Setting();
                qslot.UpdateProgress();
            }
        }

    }

}

