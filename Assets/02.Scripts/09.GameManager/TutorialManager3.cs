using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager3 : MonoBehaviour
{
    //검은 화면 텍스쳐
    public Texture2D FadeOutTexture;
    public TalkUI TalkUI;

    //페이드 속도(알파값)
    public float FadeSpeed = 0.8f;

    private int DrawDepth = -1000;
    private float Alpha = 1.0f;
    //페이드 방향 -1 : 페이드 아웃, 1 : 페이드 인
    private int FadeDirect = -1;

    //private int StartTxt = 0;

   
    private void Awake()
    {
        TalkUI = GetComponent<TalkUI>();
        FadeOutTexture = new Texture2D(1, 1);
        FadeOutTexture.SetPixel(0, 0, Color.black);
        FadeOutTexture.Apply();
    }

    private void Start()
    {
        BeginFadeIn();
        GameManager.Instance.Info();

        Invoke("BeginFadeOut", 2f);

//        ResourceManager.Instance.LoadData();
    }

    private void OnGUI()
    {

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, Alpha);
        GUI.depth = DrawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), FadeOutTexture);
    }

    //화면 페이드 코루틴
    IEnumerator Fade()
    {
        while (Alpha >= 0 && Alpha <= 1)
        {
            Alpha += FadeDirect * FadeSpeed * Time.deltaTime;
            Alpha = Mathf.Clamp01(Alpha);
            yield return null;
        }

        if (FadeDirect == -1 && Alpha <= 0)
        {
            //페이드 끝나면 중단
            StopCoroutine("Fade");
        }
    }

    //페이드 아웃
    public void BeginFadeOut()
    {
        FadeDirect = -1;
        StartCoroutine("Fade");
    }

    // 페이드 인
    public void BeginFadeIn()
    {
        FadeDirect = 1;
        TalkUI.ShowTalk1();
        StartCoroutine("Fade");
    }
}
