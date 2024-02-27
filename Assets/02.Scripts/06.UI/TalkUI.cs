using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Talk
{
    [TextArea]
    public string talk;
}

public class TalkUI : MonoBehaviour
{
    [SerializeField] Text txt_Talk;
    [SerializeField] SpriteRenderer Sprite_TalkBox;

    private bool IsTalk = false;

    private int count = 0;

    [SerializeField] private Talk[] talk;

    public void ShowTalk()
    {
        OnOff(true);

        count = 0;
        NextTalk();
    }

    private void OnOff(bool TF)
    {
        Sprite_TalkBox.gameObject.SetActive(TF);
        txt_Talk.gameObject.SetActive(TF);
        IsTalk = TF;
    }

    private void NextTalk()
    {
        txt_Talk.text = talk[count].talk;
        count++;
    }

    private void Update()
    {
        if (IsTalk)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (count < talk.Length)
                {
                    NextTalk();
                }
                else
                {
                    OnOff(false);
                }
            }
        }
    }
}
