using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class FadeManager : MonoBehaviour
{
    //���� ȭ�� �ؽ���
    public Texture2D FadeOutTexture;

    //���̵� �ӵ�(���İ�)
    public float FadeSpeed = 0.8f;

    public float fadeTime = 1.0f;
    private int DrawDepth = -1000;
    private float Alpha = 1.0f;
    //���̵� ���� -1 : ���̵� �ƿ�, 1 : ���̵� ��
    private int FadeDirect = -1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(Test());
        }
    }

    IEnumerator Test()
    {
        //if (Alpha == 0)
        //{
        //    BeginFadeIn(1f);

        //}
        //else
        //{
        //    BeginFadeOut(1f);
        //}
        BeginFadeIn(1f);
        yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(3f);
        Debug.Log("test");
        BeginFadeOut(1f);

        yield return null;
    }
    private void Awake()
    {
        FadeOutTexture = new Texture2D(1, 1);
        FadeOutTexture.SetPixel(0, 0, Color.black);
        FadeOutTexture.Apply();
    }

    private void Start()
    {
        //BeginFadeIn(1f);
        BeginFadeOut(1f);
        //Invoke("BeginFadeOut", 0.3f);
    }

    private void OnGUI()
    {
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, Alpha);
        GUI.depth = DrawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), FadeOutTexture);
    }

    private Coroutine fadeCoroutine;  // Fade �ڷ�ƾ�� ������ ����

    IEnumerator Fade()
    {
        float testFadeSpeed = FadeSpeed;
        while (Alpha >= 0 && Alpha <= 1)
        {
            Alpha += FadeDirect * testFadeSpeed * Time.deltaTime / fadeTime;
            Alpha = Mathf.Clamp01(Alpha);
            yield return null;
        }

        if (FadeDirect == -1 && Alpha <= 0)
        {
            //���̵� ������ �ߴ�
            StopCoroutine(Fade());
        }
    }

    public void BeginFadeOut(float value)
    {
        Debug.Log("BeginFadeOut");
        fadeTime = value;
        FadeDirect = -1;

        if (fadeCoroutine != null)  // ������ ���۵� Fade �ڷ�ƾ�� �ִٸ�
        {
            StopCoroutine(fadeCoroutine);  // �ش� �ڷ�ƾ�� ����
        }
        fadeCoroutine = StartCoroutine(Fade());
    }

    public void BeginFadeIn(float value)
    {
        Debug.Log("BeginFadeIn");
        fadeTime = value;
        FadeDirect = 1;

        if (fadeCoroutine != null)  // ������ ���۵� Fade �ڷ�ƾ�� �ִٸ�
        {
            StopCoroutine(fadeCoroutine);  // �ش� �ڷ�ƾ�� ����
        }
        fadeCoroutine = StartCoroutine(Fade());
    }

    public IEnumerator FadeInOut(float fadeTime)
    {
        BeginFadeIn(fadeTime);
        yield return new WaitForSeconds(fadeTime);
        yield return new WaitForSeconds(0.2f);
        BeginFadeOut(fadeTime);
    }

    public IEnumerator CLoadSceneFadeIn(float FadeTime, string LoadSceneName)
    {
        BeginFadeIn(FadeTime);
        yield return YieldInstructionCache.WaitForSeconds(FadeTime);
        yield return YieldInstructionCache.WaitForSeconds(1.5f);
        SceneManager.LoadScene(LoadSceneName);
    }

    public IEnumerator CLoadSceneFadeOut(float FadeTime, string LoadSceneName)
    {
        BeginFadeOut(FadeTime);
        yield return YieldInstructionCache.WaitForSeconds(FadeTime);
        yield return YieldInstructionCache.WaitForSeconds(0.2f);
        SceneManager.LoadScene(LoadSceneName);
    }

}
