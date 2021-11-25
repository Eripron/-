using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BGM
{
    BGM_BOSS,


}

public class SoundManager : PoolManager<SoundManager, SFXObject>
{
    [SerializeField] AudioSource bgmAudio;
    [SerializeField] AudioClip[] bgmClips;
    [SerializeField] AudioClip[] sfxClips;

    public void PlayBGM(string bgmName)
    {
        for(int i=0; i<bgmClips.Length; i++)
        {
            if(bgmName.Equals(bgmClips[i].name))
            {
                Debug.Log($"BGM Clip �ֱ�  -->  {bgmName} ã�� Clip ����");
                bgmAudio.clip = bgmClips[i];
                break;
            }
        }

        Debug.Log("BGM PLAY");
        bgmAudio.Play();
    }


    public void PlaySFX(string sfxName)
    {
        for(int i=0; i<sfxClips.Length; i++)
        {
            if(sfxName.Equals(sfxClips[i].name))
            {
                Debug.Log($"SFX Clip �ֱ�  -->  {sfxName} ã�� Clip ����");
                SFXObject sfx = GetPool();
                sfx.OnPlaySFX(sfxClips[i]);
                break;
            }
        }
    }


}
