using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }

    private AudioSource bgmPlayer;
    private AudioSource[] sfxPlayers;

    public float bgmVolume;
    public float sfxVolume;
    public int sfxChannelCount;


    private void Awake()
    {
        instance = this;

        GameObject bgmObject = new GameObject("BGM Player");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;

        GameObject sfxObject = new GameObject("SFX Player");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[sfxChannelCount];
        for (int i = 0; i < sfxChannelCount; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].loop = false;
            sfxPlayers[i].volume = sfxVolume;
        }
    }

    public void PlaySFX(AudioClip sfx)
    {
        int i;
        for (i = 0; i < sfxChannelCount && sfxPlayers[i].isPlaying ; i++) ;
        if (i < sfxChannelCount)
        {
            sfxPlayers[i].clip = sfx;
            sfxPlayers[i].Play();
        }
        else
            Debug.Log("효과음 재생기가 모두 재생중이어서 효과음 재생에 실패 했습니다.");
    }

    public void PlayBGM(AudioClip bgm)
    {
        bgmPlayer.clip = bgm;
        bgmPlayer.Play();
    }
}
