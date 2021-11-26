using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] MapClearOrFailUI clearOrFailUI; 

    new void Awake()
    {
        base.Awake();
    }


    public void OnMapClear()
    {
        // boss ������ ȣ�� 
        //SoundManager.Instance.PlayBGM(BGM.BGM_BOSS_CELAR.ToString());
        SoundManager.Instance.StopBGM();
        clearOrFailUI.OnMapClearOrFailUI(true);
        Camera.main.GetComponent<CameraController>().OnMouseAble();
    }

    public void OnMapFail()
    {
        // - ��ư Ŭ���� ȣ���ϴ� �ɷθ� �ϱ�  
        clearOrFailUI.OnMapClearOrFailUI(false);
    }


}

