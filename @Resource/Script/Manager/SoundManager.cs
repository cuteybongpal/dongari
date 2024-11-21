using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting.FullSerializer;
using System;
using Unity.VisualScripting;

public class SoundManager : MonoBehaviour
{
    //오디오 소스 첫번째는 무조건 배경음
    //오디오 소스가 부족하다면 풀링을 사용할 예정임
    List<AudioSource> audios = new List<AudioSource>();

    public void Init()
    {
        
    }
    //배경음이 이미 틀어져 있다면 교체함
    public void TurnOnBGM(AudioClip clip, bool isLoop)
    {
        if (audios.Count == 0)
        {
            audios.Add(GetComponent<AudioSource>());
            audios[0].loop = true;
        }
        audios[0].clip = clip;
        audios[0].Play();
        if (!isLoop)
            WaitClipEndAndDelClip(clip, 0).Forget();
    }
    public void TurnOnEff(AudioClip _clip)
    {
        if (audios.Count >= 1)
            AddAudioSource(false);
        bool isAllFilled = true;
        for (int i = 1; i < audios.Count; i++)
        {
            if (!audios[i].isPlaying)
            {
                isAllFilled &= true;
                continue;
            }
            else
            {
                audios[i].clip= _clip;
                audios[i].Play();
                WaitClipEndAndDelClip(_clip, i).Forget();
                isAllFilled = false;
                break;
            }
        }
        if (isAllFilled)
        {
            AudioSource _source = AddAudioSource(false);
            _source.clip = _clip;
            _source.Play();
            WaitClipEndAndDelClip (_clip, audios.Count - 1).Forget();
        }
    }
    async UniTaskVoid WaitClipEndAndDelClip(AudioClip _clip, int index)
    {
        if (audios[index] == null)
            return;
        if (audios[index].clip == null)
            return;
        await UniTask.Delay(TimeSpan.FromSeconds(_clip.length));

        audios[index].clip = null; // 클립을 비워줌
    }
    private AudioSource AddAudioSource(bool isLoop)
    {
        AudioSource audioSource = new GameObject() { name = "sound01" }.AddComponent<AudioSource>();
        audioSource.loop = isLoop;
        audioSource.playOnAwake = false;
        audios.Add(audioSource);
        return audioSource;
    }
}
