using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXObject : PoolObject<SFXObject>
{
    [SerializeField] AudioSource speaker;

    public void OnPlaySFX(AudioClip _clip, float _volume)
    {
        speaker.clip = _clip;
        speaker.volume = _volume;
        speaker.Play();

        StartCoroutine(PlaySFX());
    }

    IEnumerator PlaySFX()
    {
        while(speaker.isPlaying)
            yield return null;

        OnReturnForce();
    }
}
