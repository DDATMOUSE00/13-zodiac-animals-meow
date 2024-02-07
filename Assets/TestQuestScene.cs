using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestQuestScene : MonoBehaviour
{

    CollectQuest test = new CollectQuest();
    // Start is called before the first frame update
    void Start()
    {
        test.SettingCollectQuest(2,1);
    }


    public void checkQuestProgress()
    {
        test.CheckProgress();
    }





}
