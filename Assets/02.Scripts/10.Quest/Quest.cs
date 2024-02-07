using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest 
{
    public QuestInfo q;

    public QuestState state;

    
    
    public Quest(QuestInfo questInfo)
    {
        this.q = questInfo;
        this.state = QuestState.REQUIREMENTS_NOT_MET;

    }

}
