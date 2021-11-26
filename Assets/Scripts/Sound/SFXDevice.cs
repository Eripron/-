using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXDevice : MonoBehaviour
{
    [SerializeField] AudioClip clip;

    void Start()
    {
        Button button = GetComponent<Button>();
        if (button == null)
        {
            Debug.Log("버튼 없음");
            enabled = false;
        }

        button.onClick.AddListener(PlaySFXSound);
    }

    public void PlaySFXSound()
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlaySFX(clip.name);
    }

}
