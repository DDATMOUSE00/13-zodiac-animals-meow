using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestQuestScene : MonoBehaviour
{

    CollectQuest test = new CollectQuest();
    CollectQuest test1 = new CollectQuest();
    // Start is called before the first frame update
    void Start()
    {
        test.SettingCollectQuest(2,2);
        test1.SettingCollectQuest(0, 3);
    }


    public void checkQuestProgress()
    {
        test.CheckProgress();
        test1.CheckProgress();
    }





}
