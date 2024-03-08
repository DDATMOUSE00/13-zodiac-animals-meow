using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("BGM")]
    public AudioClip BGMClip;
    public float BGMVolume;
    AudioSource BGMPlayer;

    [Header("SFX")]
    public AudioClip[] SFXClip;
    public float SFXVolume;
    public int channels;
    AudioSource[] SFXPlayer;
    public int ChannelIndex;

    public enum SFX { Attack, Footstep, Roll }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        Sound();

    }
    private void Start()
    {
        PlayBgm(true);
    }

    private void Sound()
    {
        //배경음 초기화
        GameObject BGMObject = new GameObject("BGMPlayer");
        BGMObject.transform.parent = transform;
        BGMPlayer = BGMObject.AddComponent<AudioSource>();
        BGMPlayer.playOnAwake = false;
        BGMPlayer.loop = true;
        BGMPlayer.volume = BGMVolume;
        BGMPlayer.clip = BGMClip;

        //효과음 초기화
        GameObject SFXObject = new GameObject("SFXPlayer");
        SFXObject.transform.parent = transform;
        SFXPlayer = new AudioSource[channels];

        for (int index = 0; index < SFXPlayer.Length; index++)
        {
            SFXPlayer[index] = SFXObject.AddComponent<AudioSource>();
            SFXPlayer[index].playOnAwake = false;
            SFXPlayer[index].volume = SFXVolume;
            SFXPlayer[index].pitch = 0.83f;
        }
    }

    public void PlayBgm(bool IsPlay)
    {
        if (IsPlay)
        {
            BGMPlayer.Play();
        }
        else
        {
            BGMPlayer.Stop();
        }
    }

    public void PlaySFX(SFX sfx)
    {
        for (int index = 0; index < SFXPlayer.Length; index++)
        {
            int loopIndex = (index + ChannelIndex) % SFXPlayer.Length;

            if (SFXPlayer[loopIndex].isPlaying)
            {
                continue;
            }
            ChannelIndex = loopIndex;
            SFXPlayer[0].clip = SFXClip[(int)sfx];
            SFXPlayer[0].Play();
            break;
        }
    }

    //새로운 BGM을 설정
    //public void ChangeBGM(AudioClip newClip)
    //{
    //    BGMClip = newClip;
    //    BGMPlayer.clip = newClip;
    //    BGMPlayer.Play();
    //}
}
