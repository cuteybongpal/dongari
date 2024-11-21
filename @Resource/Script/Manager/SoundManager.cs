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
    //����� �ҽ� ù��°�� ������ �����
    //����� �ҽ��� �����ϴٸ� Ǯ���� ����� ������
    List<AudioSource> audios = new List<AudioSource>();

    public void Init()
    {
        
    }
    //������� �̹� Ʋ���� �ִٸ� ��ü��
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

        audios[index].clip = null; // Ŭ���� �����
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
