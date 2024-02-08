using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public static QuestManager I; 
    private Dictionary<int, List<Quest>> questList;
    public Dictionary<string, Quest> allQuests;
 
    private void Awake()
    {
        I = this;
        questList = CreateQuestLIst();
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
}

