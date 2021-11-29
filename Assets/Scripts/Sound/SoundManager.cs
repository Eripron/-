using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BGM
{
    BGM_BOSS,
    BGM_BOSS_CELAR,     // 없음
}

public class SoundManager : PoolManager<SoundManager, SFXObject>
{
    [SerializeField] AudioSource bgmAudio;
    [SerializeField] AudioClip[] bgmClips;
    [SerializeField] AudioClip[] sfxClips;

    public float BGMVolume { get; set; }
    public float SFXVolume { get; set; }


    private float bgmVolume;
    private float sfxVolume;

    public void PlayBGM(string bgmName, bool isLoop = false)
    {
        if (bgmAudio.isPlaying)
            bgmAudio.Stop();

        bgmAudio.loop = isLoop;
        for(int i=0; i<bgmClips.Length; i++)
        {
            if(bgmName.Equals(bgmClips[i].name))
            {
                Debug.Log($"BGM Clip 넣기  -->  {bgmName} 찾음 Clip 넣음");
                bgmAudio.clip = bgmClips[i];
                break;
            }
        }

        Debug.Log("BGM PLAY");
        bgmAudio.Play();
    }
    public void StopBGM()
    {
        //if(bgmAudio.isPlaying)
        //    bgmAudio.Stop();
    }

    public void PlaySFX(string sfxName)
    {
        for(int i=0; i<sfxClips.Length; i++)
        {
            if(sfxName.Equals(sfxClips[i].name))
            {
                SFXObject sfx = GetPool();
                sfx.OnPlaySFX(sfxClips[i], sfxVolume);
                break;
            }
        }
    }

    public void ChangeBGMVolume(float value)
    {
        bgmVolume = Mathf.Clamp(value, 0.0f, 1.0f);
        bgmAudio.volume = bgmVolume;
    }
    public void ChangeSFXVolume(float value)
    {
        sfxVolume = Mathf.Clamp(value, 0.0f, 1.0f);
    }

}
