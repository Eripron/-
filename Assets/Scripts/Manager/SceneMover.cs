using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMover : Singleton<SceneMover>
{
    [SerializeField] DontDestroyManager DDM;
    [SerializeField] FadeManager fadeManager;

    // scene 전환시 ui 창들을 끈다 
    System.Action DelCloseUIWindow;

    string curScene;
    SCENE enumCurScene;

    public void AddCloseWindowFun(System.Action fun)
    {
        // 꺼야하는 창들의 함수를 받아온다.
        DelCloseUIWindow += fun;
    }

    public enum SCENE
    {
        None = -1,

        Start,
        Menu,
        Town,
        Main,

        Scene_Count,
    }

    // load할 scene의 정보 
    SCENE scene = SCENE.None;

    new void Awake()
    {
        base.Awake();

        curScene = SceneManager.GetActiveScene().name;
    }


    public void OnMoveScene(SCENE _scene)
    {
        // scene 정보가 없는 경우 
        if (_scene == SCENE.None || fadeManager == null)
            return;

        if (_scene == SCENE.Menu && curScene == _scene.ToString())
            return;

        curScene = _scene.ToString();
        enumCurScene = _scene;

        //SoundManager.Instance.StopBGM();
        scene = _scene;
        fadeManager.FadeIn(true, MoveScene);
    }

    // Fade경우 중간에 부르기 위해서 
    private void MoveScene()
    {
        DelCloseUIWindow?.Invoke();
        SceneManager.LoadScene(scene.ToString());
        scene = SCENE.None;
    }

    public string CurSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public SCENE CurSceneEnum()
    {
        return enumCurScene;
    }
}
