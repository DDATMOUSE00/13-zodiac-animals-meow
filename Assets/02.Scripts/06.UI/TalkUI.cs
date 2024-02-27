using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class Talk
{
    [TextArea]
    public string talk;
}

public class TalkUI : MonoBehaviour
{
    [SerializeField] TMP_Text txt_Talk;
    [SerializeField] SpriteRenderer Sprite_TalkBox;
    //[SerializeField] private TutorialManager _TutorialManager;

    private bool IsTalk = false;
    public int ShowTextCount = 1;
    private int count = 0;

    [SerializeField] private Talk[] talk;

    public void ShowTalk1()
    {
        OnOff(true);

        count = 0;
        NextTalk();
    }

    public void ShowTalk2()
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
                    ShowTextCount++;
                    OnOff(false);
                    if (ShowTextCount == 2)
                    {
                        SceneManager.LoadScene("TutorialScene2");
                    }
                    if (ShowTextCount == 3)
                    {
                        SceneManager.LoadScene("TutorialScene3");
                    }
                    if (ShowTextCount == 4)
                    {
                        SceneManager.LoadScene("Village_FINAL");
                        GameManager.Instance.player.transform.position = new Vector3(0, 0, -20);
                    }
                }
            }
        }
    }
}
