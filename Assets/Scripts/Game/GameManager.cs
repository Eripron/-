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
        // boss 죽으면 호출 
        //SoundManager.Instance.PlayBGM(BGM.BGM_BOSS_CELAR.ToString());
        SoundManager.Instance.StopBGM();
        clearOrFailUI.OnMapClearOrFailUI(true);
        Camera.main.GetComponent<CameraController>().OnMouseAble();
    }

    public void OnMapFail()
    {
        // - 버튼 클릭시 호출하는 걸로만 하기  
        clearOrFailUI.OnMapClearOrFailUI(false);
    }


}

