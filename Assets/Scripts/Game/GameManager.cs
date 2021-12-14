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
        // boss 죽으면 호출 
        SoundManager.Instance.PlayBGM(clearBGM.ToString());
        Camera.main.GetComponent<CameraController>().OnMouseAble();
    }

    public void MapClearUI()
    {
        clearOrFailUI.OnMapClearOrFailUI(true);
    }

    public void OnMapFail()
    {
        // - 버튼 클릭시 호출하는 걸로만 하기  
        SoundManager.Instance.StopBGM();
        clearOrFailUI.OnMapClearOrFailUI(false);
    }


}

