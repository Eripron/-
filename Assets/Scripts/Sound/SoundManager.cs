using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BGM
{
    BGM_BOSS,
    BGM_CLEAR,
    BGM_MENU,
    BGM_TOWN,
}

public class SoundManager : PoolManager<SoundManager, SFXObject>
{
    [SerializeField] AudioSource bgmAudio;
    [SerializeField] AudioClip[] bgmClips;
    [SerializeField] AudioClip[] sfxClips;

    [SerializeField] FadeManager fadeManager;

    public float BGMVolume { get; set; }
    public float SFXVolume { get; set; }


    private float bgmVolume;
    private float sfxVolume;

    public void PlayBGM(string bgmName, bool isLoop = false)
    {
        StartCoroutine(WaitUntilFadeOut(bgmName, isLoop));
    }
    public void StopBGM()
    {
        if (bgmAudio.isPlaying)
            bgmAudio.Stop();
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


    IEnumerator WaitUntilFadeOut(string bgmName, bool isLoop)
    {
        Debug.Log("대기중 fade out");
        yield return new WaitUntil(()=> fadeManager.isFading == false);

        if (bgmAudio.isPlaying)
            bgmAudio.Stop();

        bgmAudio.loop = isLoop;
        for (int i = 0; i < bgmClips.Length; i++)
        {
            if (bgmName.Equals(bgmClips[i].name))
            {
                Debug.Log($"BGM Clip 넣기  -->  {bgmName} 찾음 Clip 넣음");
                bgmAudio.clip = bgmClips[i];
                break;
            }
        }

        Debug.Log("BGM PLAY");
        bgmAudio.Play();
    }
}
