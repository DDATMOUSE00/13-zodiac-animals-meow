using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;
    protected void FinishQuestStep(GameObject obj)
    {
        if (!isFinished)
        {
            isFinished = true;
      
            Destroy(obj);
        }
    }
}
