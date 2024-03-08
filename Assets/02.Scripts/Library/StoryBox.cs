using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class StoryBox : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text desc;


    public void Setting(string t, string d)
    {

        title.text = t;
        desc.text = d;
    }
}
