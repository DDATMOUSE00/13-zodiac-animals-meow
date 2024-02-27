using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("BGM")]
    public AudioClip BGMClip;
    public float BGMVolume;
    AudioSource BGMPlayer;

    [Header("SFX")]
    public AudioClip[] SFXClip;
    public float SFXVolume;
    public int channels;
    AudioSource[] SFXPlayer;
    int ChannelIndex;

    public enum SFX { Attack, Footstep, Roll }

    private void Awake()
    {
        instance = this;
        Sound();
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
}
