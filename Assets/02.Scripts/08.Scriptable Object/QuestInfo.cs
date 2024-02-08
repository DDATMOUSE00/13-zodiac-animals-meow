using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfo", menuName = "08.Scriptable Object/QuestInfoSO")]
public class QuestInfo : ScriptableObject
{
    public string QuestId { get; private set; }
    
    [Header("General")]
    public string QuestName;
    public string QuestDesc;
    public QuestStep questPrefabs;

    [Header("Requirements")]
    public int levelRequirement;

    [Header("Rewards")]

    public int goldReward;

    private void OnValidate()
    {
        #if UNITY_EDITOR
        QuestId= Guid.NewGuid().ToString();
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }


}
