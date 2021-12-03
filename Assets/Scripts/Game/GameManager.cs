using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : Singleton<GameManager>
{
    [SerializeField] MapClearOrFailUI clearOrFailUI;

    [SerializeField] BGM clearBGM;


    new void Awake()
    {
        base.Awake();
    }



    public void OnMapClear()
    {
        // boss ������ ȣ�� 
        SoundManager.Instance.PlayBGM(clearBGM.ToString());
        clearOrFailUI.OnMapClearOrFailUI(true);
        Camera.main.GetComponent<CameraController>().OnMouseAble();
    }

    public void OnMapFail()
    {
        // - ��ư Ŭ���� ȣ���ϴ� �ɷθ� �ϱ�  
        SoundManager.Instance.StopBGM();
        clearOrFailUI.OnMapClearOrFailUI(false);
    }


}

