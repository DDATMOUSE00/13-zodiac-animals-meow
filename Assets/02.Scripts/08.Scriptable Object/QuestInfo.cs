using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfo", menuName = "08.Scriptable Object/QuestInfoSO")]
public class QuestInfo : ScriptableObject
{
    public int QuestId;
    
    [Header("General")]
    public string QuestName;
    public string QuestDesc;
    public QuestStep[] questPrefabs;
    [Header("Requirements")]
    public int levelRequirement;

    [Header("Rewards")]

    public int goldReward;
    public int experienceReward;
  

}
